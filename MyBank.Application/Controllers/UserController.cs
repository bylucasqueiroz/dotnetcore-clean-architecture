using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBank.Domain.Entities.User;
using MyBank.Domain.Users.Entities;
using MyBank.Infrastructure.Context;
using MyBank.Service.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyBank.Application.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyBankContext _myBankContext;
        public UserController(MyBankContext myBankContext)
        {
            _myBankContext = myBankContext;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody]Auth request)
        {
            try
            {
                // Recupera o usuário
                var user = _myBankContext.Users.Where(x => x.Account == request.Account && x.Agency == request.Agency).FirstOrDefault();

                // Verifica se o usuário existe
                if (user == null)
                    return NotFound(new { message = "Account and Agency invalid" });

                if (!user.Password.ToLower().Equals(request.Password.ToLower()))
                    return NotFound(new { message = "Password invalid" });

                // Gera o Token
                var token = TokenService.GenerateToken(user);

                // Oculta a senha
                user.Password = "";

                // Retorna os dados
                return new
                {
                    token = token
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpPost()]
        [Route("Create")]
        public async Task<ActionResult<string>> Create([FromBody]User user)
        {
            try
            {
                var newUser = new User()
                {
                    Agency = user.Agency,
                    Account = user.Account,
                    Password = user.Password,
                    IdData = user.IdData
                };

                _myBankContext.Users.Add(newUser);

                await _myBankContext.SaveChangesAsync();

                return Ok("Usuário cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("Search")]
        public async Task<ActionResult<User>> GetId(int id)
        {
            try
            {
                var user = await _myBankContext.Users.FindAsync(id);

                return Ok(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("List")]
        public ActionResult<IQueryable<User>> GetAll()
        {
            try
            {
                var lista = _myBankContext.Users.AsQueryable();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }
    }
}
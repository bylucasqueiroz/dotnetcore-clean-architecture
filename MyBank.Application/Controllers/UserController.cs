using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBank.Domain.Entities.User;
using MyBank.Domain.Interfaces;
using MyBank.Domain.Users.Entities;
using MyBank.Infrastructure.Context;
using MyBank.Service.Services;
using MyBank.Service.Validators;
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

        private IService<User> service = new BaseService<User>();
        public UserController(MyBankContext myBankContext)
        {
            _myBankContext = myBankContext;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
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

        [HttpPost]
        [Route("Create")]
        [Authorize]
        public IActionResult Post([FromBody] User item)
        {
            try
            {
                service.Post<UserValidator>(item);

                return new ObjectResult(item.Id);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("Search/{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            try
            {
                return new ObjectResult(service.Get(id));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("List")]
        [Authorize]
        public IActionResult Get()
        {
            try
            {
                return new ObjectResult(service.Get());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("delete")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                service.Delete(id);

                return new NoContentResult();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        public IActionResult Put([FromBody] User item)
        {
            try
            {
                service.Put<UserValidator>(item);

                return new ObjectResult(item);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
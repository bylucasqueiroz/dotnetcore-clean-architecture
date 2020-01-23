using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBank.Domain.Entities;
using MyBank.Infrastructure.Context;
using MyBank.Service.Services;

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
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody]User request)
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
                user = user,
                token = token
            };
        }
    }
}
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyBank.Domain.Entities.User;
using MyBank.Domain.Interfaces;
using MyBank.Domain.Users.Entities;
using MyBank.Infrastructure.Context;
using MyBank.Service.Enuns;
using MyBank.Service.Services;
using MyBank.Service.Validators;
using System;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyBank.Application.Controllers
{
    [Route("v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private BaseService<User> service = new BaseService<User>();

        private MyBankContext context = new MyBankContext();

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody]Auth request)
        {
            try
            {
                var user = context.Users.Where(x => x.Account == request.Account && x.Agency == request.Agency).FirstOrDefault();

                if (user == null)
                    return NotFound(new { message = "Account and Agency invalid" });

                if (!user.Password.ToLower().Equals(request.Password.ToLower()))
                    return NotFound(new { message = "Password invalid" });

                var token = TokenService.GenerateToken(user);

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
        [Route("post")]
        [Authorize("Manager")]
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
        [Route("get/{id}")]
        [Authorize("Manager")]
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
        [Route("get")]
        [Authorize("Manager")]
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
        [Authorize("Manager")]
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

        [HttpPut]
        [Route("put")]
        [Authorize("Manager")]
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
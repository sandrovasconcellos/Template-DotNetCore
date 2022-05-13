using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Template.Application.Interfaces;
using Template.Application.ViewModels;
using Template.Auth.Services;

namespace Template.Controllers
{
    //para chamar https://localhost:44386/api/users
    //Authorize indica que é privado - o correto é privar tudo e ir liberando com a necessidade

    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class UsersController : ControllerBase
    {

        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Get()
        {
            return Ok(this.userService.Get());
        }

        [HttpPost, AllowAnonymous]
        public IActionResult Post(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(this.userService.Post(userViewModel));
        }

        //para diferenciar devemos indicar o parametro a ser recebido
        //passando parametro pela rota
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            return Ok(this.userService.GetById(id));
        }

        [HttpPut]
        public IActionResult Put(UserViewModel userViewModel)
        {
            return Ok(this.userService.Put(userViewModel));
        }

        [HttpDelete()]
        public IActionResult Delete()
        {
            //garante que so terá acesso o id logado.
            string _userId = TokenService.GetValueFromClaim(HttpContext.User.Identity, ClaimTypes.NameIdentifier);

            return Ok(this.userService.Delete(_userId));
        }

        //nome da API
        //AllowAnonymous: libera para ser publica
        [HttpPost("authenticate"), AllowAnonymous]
        public IActionResult Authenticate(UserAuthenticateRequestViewModel  userViewModel)
        {
            ////para estudo
            //UserAuthenticateResponseViewModel userAuthenticateResponseViewModel;
            //userAuthenticateResponseViewModel = this.userService.Authenticate(userViewModel);
            //return Ok(userAuthenticateResponseViewModel);

            return Ok(this.userService.Authenticate(userViewModel));
        }
    }
}

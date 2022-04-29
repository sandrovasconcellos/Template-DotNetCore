using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Application.Interfaces;
using Template.Application.ViewModels;

namespace Template.Controllers
{
    //para chamar https://localhost:44386/api/users

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.userService.Get());
        }

        [HttpPost]
        public IActionResult Post(UserViewModel userViewModel)
        {
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

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return Ok(this.userService.Delete(id));
        }

        //nome da API
        [HttpPost("authenticate")]
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

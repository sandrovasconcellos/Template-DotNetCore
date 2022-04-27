using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Application.Interfaces;
using Template.Application.ViewModels;
using Template.Domain.Entities;

namespace Template.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public List<UserViewModel> Get()
        {
            List<UserViewModel> _userViewModels = new List<UserViewModel>();

            IEnumerable<User> _users = this.userRepository.GetAll();

            //o mapper permite preencher lista
            _userViewModels = mapper.Map<List<UserViewModel>>(_users);           

            return _userViewModels;
        }

        public bool Post (UserViewModel userViewModel)
        {         
            //destino(User) <- origem(userViewModel)
            User _user = mapper.Map<User>(userViewModel);

            this.userRepository.Create(_user);

            return true;
        }

        public UserViewModel GetById(string id)
        {
            //se não consegui tranformar em guid - o id vai ser inserido no userId
            if(!Guid.TryParse(id, out Guid userId))
            {
                throw new Exception("UserId is not valid");
            }

            //recupera o usuario não deletado
            User _user = this.userRepository.Find(x => x.Id == userId && !x.IsDeleted);

            if(_user == null)
                throw new Exception("User not found");

            return mapper.Map<UserViewModel>(_user);
        }

        public bool Put(UserViewModel  userViewModel)
        {
            //recupera um objeto realiza o tracked(monitorado) e altera
            User _user = this.userRepository.Find(x => x.Id == userViewModel.Id && !x.IsDeleted);

            if (_user == null)
                throw new Exception("User not found");

            _user = mapper.Map<User>(userViewModel);

            //informa que esse objeto esta tracked, por isso precisamos informar no metodo FIND inclui o AsNoTracking().
            this.userRepository.Update(_user);

            //ALTERAÇÃO DE TESTE NO GIT

            return true;
        }

        public bool Delete (string id)
        {
            //se não consegui tranformar em guid - o id vai ser inserido no userId
            if (!Guid.TryParse(id, out Guid userId))
            {
                throw new Exception("UserId is not valid");
            }

            //recupera o usuario não deletado
            User _user = this.userRepository.Find(x => x.Id == userId && !x.IsDeleted);
            if (_user == null)
                throw new Exception("User not found");
                        
            return this.userRepository.Delete(_user);
        }
    }
}

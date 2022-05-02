using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Application.Interfaces;
using Template.Application.ViewModels;
using Template.Auth.Services;
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
            if (userViewModel.Id != Guid.Empty)
                throw new Exception("UserID must be empty");

            //destino(User) <- origem(userViewModel)
            User _user = mapper.Map<User>(userViewModel);

            this.userRepository.Create(_user);

            return true;
        }

        public UserViewModel GetById(string id)
        {
            //se não consegui tranformar em guid - o id vai ser inserido no userId
            if (!Guid.TryParse(id, out Guid userId))
                throw new Exception("UserID is not valid");

            //recupera o usuario não deletado
            User _user = this.userRepository.Find(x => x.Id == userId && !x.IsDeleted);

            if(_user == null)
                throw new Exception("User not found");

            return mapper.Map<UserViewModel>(_user);
        }

        public bool Put(UserViewModel  userViewModel)
        {
            if (userViewModel.Id == Guid.Empty)
                throw new Exception("ID is invalid");

            //recupera um objeto realiza o tracked(monitorado) e altera
            User _user = this.userRepository.Find(x => x.Id == userViewModel.Id && !x.IsDeleted);

            if (_user == null)
                throw new Exception("User not found");

            _user = mapper.Map<User>(userViewModel);

            //informa que esse objeto esta tracked, por isso precisamos informar no metodo FIND inclui o AsNoTracking().
            this.userRepository.Update(_user);

            return true;
        }

        public bool Delete (string id)
        {
            //se não consegui tranformar em guid - o id vai ser inserido no userId            
            if (!Guid.TryParse(id, out Guid userId))
                throw new Exception("UserID is not valid");

            //recupera o usuario não deletado
            User _user = this.userRepository.Find(x => x.Id == userId && !x.IsDeleted);
            if (_user == null)
                throw new Exception("User not found");
                        
            return this.userRepository.Delete(_user);
        }

        public UserAuthenticateResponseViewModel Authenticate(UserAuthenticateRequestViewModel user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                throw new Exception("Email/Password are required.");

            //recupera o usuario do banco
            User _user = this.userRepository.Find(x => !x.IsDeleted && x.Email.ToLower() == user.Email.ToLower());
            if (_user == null)
                throw new Exception("User not found");

            //preenche o objeto UserViewModel com o objeto _user e cria o token, chama o construtor UserAuthenticateResponseViewModel
            //devolve o objeto UserAuthenticateResponseViewModel
            return new UserAuthenticateResponseViewModel(mapper.Map<UserViewModel>(_user), TokenService.GenerateToken(_user));
        }
    }
}

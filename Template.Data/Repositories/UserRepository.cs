using System;
using System.Collections.Generic;
using System.Text;
using Template.Application.Interfaces;
using Template.Data.Context;
using Template.Domain.Entities;

namespace Template.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(TemplateContext context)
            : base(context)
        {

        }

        public IEnumerable<User> GetAll()
        {
            return Query(x => x.IsDeleted == false);
        }
    }
}

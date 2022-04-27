using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Domain.Entities;
using Template.Domain.Models;

namespace Template.Data.Extensions
{
    //static - não precia instanciar para chamar
    public static class ModelBuilderExtension
    {
        public static ModelBuilder ApplyGlobalConfiguration(this ModelBuilder builder)
        {
            //recupera a lista das entidades 
            foreach (IMutableEntityType entityType  in builder.Model.GetEntityTypes())
            {
                foreach(IMutableProperty property  in entityType.GetProperties())
                {
                    //recupera o nome da propriedade
                    switch(property.Name)
                    {
                        //caso o nome recuperado for Id
                        case nameof(Entity.Id):
                            //informa que ele é um primery key
                            property.IsKey();
                            break;
                        case nameof(Entity.DateUpdated):
                            //setada com nulo
                            property.IsNullable = true;
                            break;
                        case nameof(Entity.DateCreated):
                            //não pode ser nulo
                            property.IsNullable = false;
                            property.SetDefaultValue(DateTime.Now);
                            break;
                        case nameof(Entity.IsDeleted):
                            //não pode ser nulo
                            property.IsNullable = false;
                            property.SetDefaultValue(false);
                            break;
                        default:
                            break;
                    }
                }
            }

            return builder;
        }

        public static ModelBuilder SeedData(this ModelBuilder builder)
        {
            //toda vez que rodar o migration incluir um usuario padrão
            builder.Entity<User>()
                .HasData(
                    new User { Id = Guid.Parse("fc072692-d322-448b-9b1b-ba3443943579"), Name = "User Default", Email = "userdefault@template.com", 
                               DateCreated = new DateTime(2022,4,24), IsDeleted = false, DateUpdated = null}
                );
            return builder;
        }
    }
}

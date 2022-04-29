using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Template.Auth.Models;
using Template.Domain.Entities;

namespace Template.Auth.Services
{
    public static class TokenService
    {
        //recebe o usuario que já foi reconhecido pelo metodo de service
        public static string GenerateToken(User user)
        {
            //cria o objeto
            var tokenHandler = new JwtSecurityTokenHandler();
            
            //Secret é a propriedade da tabela Settings que criamos
            //gera um array de bites da chave secreta
            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            //cria um objeto do tipo SecurityTokenDescriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    //depois de autenticar essas informações estaram a disposição sem precisar ir ao banco
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    //o usuario logado, quando acessa uma pagina, ele não envia o id via api (front/back) e sim o token,
                    //esse é o modo de segurança, com o token eu recupero os dados disponiveis
                }),
                //duração
                Expires = DateTime.UtcNow.AddHours(3),
                //assinatura da credencial
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                
            };
            //cria o token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //escreve ele para retorno
            return tokenHandler.WriteToken(token);
        }
    }
}

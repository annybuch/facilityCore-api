using FacilityCore.API.Autenticacao.JWT.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FacilityCore.API.Autenticacao.JWT
{
    public class GerarTokenJwt
    {
        private readonly ConfiguracoesJwt _configuracoesJwt;

        public GerarTokenJwt(ConfiguracoesJwt configuracoesJwt)
        {
            _configuracoesJwt = configuracoesJwt;
        }

        public string GerarToken(string usuarioId, string nomeUsuario)
        {
            var chaveSeguranca = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuracoesJwt.Secret!));
            var credenciais = new SigningCredentials(chaveSeguranca, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, usuarioId),
            new Claim(JwtRegisteredClaimNames.UniqueName, nomeUsuario),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
               issuer: _configuracoesJwt.Issuer,
               audience: _configuracoesJwt.Audience,
               claims: claims,
               expires: DateTime.Now.AddMinutes(_configuracoesJwt.Expiracao),
               signingCredentials: credenciais);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

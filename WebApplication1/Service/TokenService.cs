using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1
{
    public class TokenService
    {
        private const string TesteChave = "LabraSoft_Security_Key_2026_@_Secret_System_v1.0!";

        public static string GerarToken(int UsuarioId, string Email, string Cargo)
        {
            var Chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TesteChave));

            var Credenciais = new SigningCredentials(Chave, SecurityAlgorithms.HmacSha256);

            var Claims = new[]
            {
                    new Claim("UsuarioId", UsuarioId.ToString()),
                    new Claim("Email", Email),
                    new Claim ("Cargo", Cargo)
            };

            var token = new JwtSecurityToken(claims: Claims, expires: DateTime.UtcNow.AddMinutes(30), signingCredentials: Credenciais);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static ClaimsPrincipal ValidarToken(string Token)
        {
            try
            {
                var Chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TesteChave));

                var Parametros = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = Chave,

                    ValidateIssuer = false,
                    ValidateAudience = false,

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var handler = new JwtSecurityTokenHandler();

                return handler.ValidateToken(Token, Parametros, out SecurityToken TokenValidado);
            }
            catch
            {
                return null;
            }
        }
    }
}
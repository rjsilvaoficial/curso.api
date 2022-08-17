using curso.api.Filters;
using curso.api.Models;
using curso.api.Models.Usuarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace curso.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        /// <summary>
        /// teste
        /// </summary>
        /// <param name="loginViewModelInput"></param>
        /// <returns></returns>
        //SwaggerResponse gera a informação de referência para o swagger, contendo status code e o json do conteúdo do objeto retornado
        [SwaggerResponse(statusCode: 200, Description = "Logado com sucesso!",Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, Description = "Campos obrigatório!", Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, Description = "Erro interno!", Type = typeof(ErroGenericoViewModel))]
        [ValidacaoModelStateCustomizado]
        [HttpPost]
        [Route("Logar")]
        public IActionResult Logar(LoginViewModelInput loginViewModelInput)
        {

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(new ValidaCampoViewModelOutput(ModelState.SelectMany(e => e.Value.Errors)
            //        .Select(e => e.ErrorMessage)));
            //}
            var usuarioViewModelOutput = new UsuarioViewModelOutput
            {
                Codigo = 1,
                Email = "email",
                Login = "login"
            };

            var secret = Encoding.ASCII.GetBytes("94b92de3-e371-48bb-b2d5-e866b51becd1");
            var symmetricSecurityKey = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuarioViewModelOutput.Codigo.ToString()),
                    new Claim(ClaimTypes.Name, usuarioViewModelOutput.Login.ToString()),
                    new Claim(ClaimTypes.Email, usuarioViewModelOutput.Email.ToString())

                }),
                Expires = DateTime.UtcNow.AddDays(1), //recomenda-se 1 à 2 horas
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            var token = jwtSecurityTokenHandler.WriteToken(tokenGenerated);


            return Ok(new
            {
                Token = token,
                Usuario = usuarioViewModelOutput
            
            });
        }

        [HttpPost]
        [Route("Registrar")]
        public IActionResult Registrar(RegistrarViewModelInput registrarViewModelInput)
        {
            return Created("",registrarViewModelInput);
        }
    }
}

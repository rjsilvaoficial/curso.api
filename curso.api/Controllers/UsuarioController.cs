using Business.Entities;
using Business.Repositories;
using Configurations;
using curso.api.Filters;
using curso.api.Models;
using curso.api.Models.Usuarios;
using Infraestructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace curso.api.Controllers
{
    [Route("api/v1/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;
        private readonly IAuthenticationService _authenticationService;

        public UsuarioController(
            IUsuarioRepository usuarioRepository, 
            IConfiguration configuration,
            IAuthenticationService authenticationService)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
            _authenticationService = authenticationService;
        }

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
            var usuario = _usuarioRepository.ObterUsuario(loginViewModelInput.Login);

            if(usuario == null)
            {
                return BadRequest("Erro ao tentar acessar!");
            }
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(new ValidaCampoViewModelOutput(ModelState.SelectMany(e => e.Value.Errors)
            //        .Select(e => e.ErrorMessage)));
            //}
            var usuarioViewModelOutput = new UsuarioViewModelOutput
            {
                Codigo = usuario.Codigo,
                Login = loginViewModelInput.Login,
                Email = usuario.Email
            };

            var token = _authenticationService.GerarToken(usuarioViewModelOutput);

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
            //Através de um obj contexto, conseguimos checar pendência de migrações
            //var migracoesPendentes = contexto.Database.GetPendingMigrations();

            //Com isso podemos também efetivar as migrações pendentes
            //if(migracoesPendentes.Count() > 0)
            //{
            //    contexto.Database.Migrate();
            //}

            var usuario = new Usuario();
            usuario.Login = registrarViewModelInput.Login;
            usuario.Email = registrarViewModelInput.Email;
            usuario.Senha = registrarViewModelInput.Senha;

            _usuarioRepository.Adicionar(usuario);
            _usuarioRepository.Commit();



            return Created("",usuario);
        }
    }
}

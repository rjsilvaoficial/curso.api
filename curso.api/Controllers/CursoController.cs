using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Cursos;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Controllers
{
    [Route("api/v1/cursos")]
    [ApiController]
    [Authorize]
    public class CursoController : ControllerBase
    {
        /// <summary>
        /// Inclusão de cursos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao cadastrar curso")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        public async Task<IActionResult> Post(CursoViewModelInput cursoViewModelInput)
        {
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            return Created("", cursoViewModelInput);
        }

        /// <summary>
        /// Pesquisa de cursos
        /// </summary>
        /// <returns></returns>

        [SwaggerResponse(statusCode: 200, description: "Sucesso ao obter cursos")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var cursos = new List<CursoViewModelOutput>();
            //var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            //for(int i = 0; i < 3; i++)
            //{
            //    cursos.Add(new CursoViewModelOutput
            //    {
            //        Login = $"",
            //        Descricao = $"Descricao{ i }",
            //        Nome = $"Nome{ i }"
            //    });
            //}

            cursos.Add(new CursoViewModelOutput { Login = "", Nome = "teste", Descricao = "teste" });


            return Ok(cursos);
        }
    }
}

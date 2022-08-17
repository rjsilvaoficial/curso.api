using curso.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace curso.api.Filters
{
    //Está classe se dedica a criação de um filtro e padronização de um modelo de retorno em caso de erros
    public class ValidacaoModelStateCustomizado : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //Verificando se modelo está inválido
            if (!context.ModelState.IsValid)
            {
                //Criando um objeto que conterá uma relação com todos os erros identificados
                var validaCampoViewModel = new ValidaCampoViewModelOutput(context.ModelState
                    .SelectMany(e => e.Value.Errors)
                    .Select(e => e.ErrorMessage));
                //Atribuindo a action em execução no momento o retorno de uma BadRequest contendo os erros (pois foi essa a implementação neste cenário)
                context.Result = new BadRequestObjectResult(validaCampoViewModel);
            }
        }
    }
}

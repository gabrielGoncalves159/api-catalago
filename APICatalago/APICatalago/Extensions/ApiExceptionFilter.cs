using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalago.Extensions
{
    // Esse filtro é responsavel por apresentar uma mensagem quando uma exceção não tratada é identificada, assim mantendo um código mais limpo
    // evitando repetições de código
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Ocorreu uma execeção não tratada.");

            context.Result = new ObjectResult("Ocorreu um problema ao tratar a sua solicitação.")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}

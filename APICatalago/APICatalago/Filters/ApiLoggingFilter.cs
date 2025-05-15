using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalago.Filters
{
    // Essa classe é um filtro personalizado que realiza o log de registros nos métodos Action dos nossos controladores
    // A aplicação do filtro sera no controller adicionando o atributo [ServiceFilter(typeof(ApiLoggingFilter))] a uma rota
    public class ApiLoggingFilter : IActionFilter
    {
        private readonly ILogger<ApiLoggingFilter> _logger;
        public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
        { 
            _logger = logger;
        }
        // Executa antes da Action
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //Exmplo de utilização do log
            _logger.LogInformation("### Executando -> OnActionExecuted");
            _logger.LogInformation("############################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"ModelState: {context.ModelState.IsValid}");
            _logger.LogInformation("############################################");
        }

        //Executa depois da Action
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("### Executando -> OnActionExecuting");
            _logger.LogInformation("############################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"ModelState: {context.HttpContext.Response.StatusCode}");
            _logger.LogInformation("############################################");
        }
    }
}

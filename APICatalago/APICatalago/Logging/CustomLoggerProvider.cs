using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Collections.Concurrent;

namespace APICatalago.Logging
{
    // Essa classe é responsavel por criar instancia de logs personalizados utilizando o ILoggerProvider, 
    public class CustomLoggerProvider : ILoggerProvider
    {
        readonly CustomLoggerProviderConfiguration loggerConfig;

        //Dicionario de loggers onde a chave é o nome de uma categoria e e uma instancia de CustomerLogger
        readonly ConcurrentDictionary<string, CustomerLogger> loggers = new ConcurrentDictionary<string, CustomerLogger>();

        //Define a configuração de logger para o provedor
        public CustomLoggerProvider(CustomLoggerProviderConfiguration loggerConfig)
        {
            this.loggerConfig = loggerConfig;
        }

        // Cria um logger para uma categoria especifica e retorna uma categoria do dicionario, se não existir, ira cria-la
        public ILogger CreateLogger(string categoryName)
        {
            return loggers.GetOrAdd(categoryName, name => new CustomerLogger(name, loggerConfig));
        }

        // Liberar os recursos quando o provedor for inutilizado
        public void Dispose()
        {
            loggers.Clear();
        }
    }
}

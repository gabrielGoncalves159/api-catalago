
namespace APICatalago.Logging
{
    // Essa classe define os metodos necessarios para a implementação dos logs
    public class CustomerLogger : ILogger
    {
        readonly string loggerName;
        private CustomLoggerProviderConfiguration loggerConfig;

        public CustomerLogger(string name, CustomLoggerProviderConfiguration loggerConfig)
        {
            this.loggerName = name;
            this.loggerConfig = loggerConfig;
        }

        //Cria um escopo para mensagens de log, mas, não sera utilizado, por isso retorna null
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        // Verifica se o nível de log desejado esta habilitado bom base na configuração, se não estiver, as mensagens desse nível não serão registradas
        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == loggerConfig.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            string mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";
            EscreverTextoArquivo(mensagem);
        }

        private void EscreverTextoArquivo(string mensagem)
        {
            string caminhoArquivoLog = @"C:/dados/log/log_produtoCategoria.txt";

            using (StreamWriter streamWriter = new StreamWriter(caminhoArquivoLog, true))
            {
                try
                {
                    streamWriter.WriteLine(mensagem);
                    streamWriter.Close();
                } catch(Exception)
                {
                    throw;
                }
            }
        }
    }
}

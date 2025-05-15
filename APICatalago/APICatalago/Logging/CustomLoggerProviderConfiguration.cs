namespace APICatalago.Logging
{
    // Classe responsavel por definir a configuração do provedor do log personalizado 
    // LogLevel - Defini o nível mínimo de log a ser registrado, com o padrão LogLevel.Warning
    // EventId - Define o ID do evento de log, com o padrão zero
    public class CustomLoggerProviderConfiguration
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Warning;
        public int EventId { get; set; } = 0;
    }
}

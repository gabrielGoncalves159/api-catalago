using System.Text.Json;

namespace APICatalago.Models
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Trace { get; set; }
        public override string ToString()
        {
            // convertendo as informações da classe no formato json
            return JsonSerializer.Serialize(this);
        }
    }
}

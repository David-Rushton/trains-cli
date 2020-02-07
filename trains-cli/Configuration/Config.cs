using System;
using static System.Environment;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace trains_cli.Configuration
{
    public class Config
    {
        [JsonPropertyName("appId")]
        public string? AppId { get; set; }

        [JsonPropertyName("appKey")]
        public string? AppKey { get; set; }
    }
}

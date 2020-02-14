using System;
using static System.Environment;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Dr.TrainsCli.Configuration
{
    public class Config
    {
        // Config file path is set when the object is created by the factory
        // Value applied here to reduce null checks elsewhere
        internal Config()
            => (ConfigFilePath) = ("");


        [JsonIgnore]
        public string ConfigFilePath { get; set; }

        [JsonPropertyName("appId")]
        public string? AppId { get; set; }

        [JsonPropertyName("appKey")]
        public string? AppKey { get; set; }
    }
}

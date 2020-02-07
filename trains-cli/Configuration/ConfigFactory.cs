using System;
using static System.Environment;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;


namespace Dr.TrainsCli.Configuration
{
    internal class ConfigFactory
    {
        private ConfigFactory()
        { }


        public static string ConfigFilePath => Path.Combine
        (
            Environment.GetFolderPath(SpecialFolder.LocalApplicationData),
            "OldLeaf",
            "trains-cli",
            "config.json"
        );


        public static async Task<Config> GetConfigAsync()
        {
            try
            {
                await createConfigFileIfNotExists();
                return await GetConfig();
            }
            catch (Exception e)
            {
                throw new Exception($"Cannot read config: {ConfigFilePath}", e);
            }


            async Task createConfigFileIfNotExists()
            {
                if( ! File.Exists(ConfigFilePath) )
                {
                    Directory.CreateDirectory
                    (
                        Path.GetDirectoryName(ConfigFilePath)
                    );
                    await File.WriteAllTextAsync
                    (
                        ConfigFilePath,
                        JsonSerializer.Serialize<Config>(new Config())
                    );
                }
            }

            async Task<Config> GetConfig()
            {
                using(FileStream fs = File.OpenRead(ConfigFilePath))
                {
                    return await JsonSerializer.DeserializeAsync<Config>(fs);
                }
            }
        }
    }
}

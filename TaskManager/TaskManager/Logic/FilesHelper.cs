using System.IO;
using Newtonsoft.Json.Linq;

namespace TaskManager
{
    public class FilesHelper
    {
        private readonly string _apiKey;
        private readonly string _apiToken;

        public FilesHelper()
        {
            // Read the configuration from the JSON file
            JObject config = JObject.Parse(File.ReadAllText("settings.json"));
            _apiKey = (string)config["apiKey"];
            _apiToken = (string)config["apiToken"];
        }
        public string GetApiKey()
        {
            return _apiKey;
        }

        public string GetApiToken()
        {
            return _apiToken;
        }
    }
}
using Newtonsoft.Json;
using System;
using System.IO;

namespace TTSTool
{
    public class Config
    {
        private const string CONFIG_FILENAME = "config.json";

        public string Key { get; set; }
        public string Region { get; set; }
        public string OutputFolder { get; set; }
        public decimal Pitch { get; set; }
        public decimal Speed { get; set; }
        public string Lang { get; set; }
        public string Voice { get; set; }
        public string Style { get; set; }
        public string Role { get; set; }
        public bool MP3 { get; set; }
        public bool KeepWAV { get; set; }
        public bool Stero { get; set; }
        private string Json { get; set; }

        internal static Config LoadConfig()
        {
            if (File.Exists(CONFIG_FILENAME))
            {
                var json = File.ReadAllText(CONFIG_FILENAME);
                var config = JsonConvert.DeserializeObject<Config>(json);
                config.Json = json;
                return config;
            }
            else
            {
                return new Config();
            }
        }

        internal void SaveConfig()
        {
            var json = JsonConvert.SerializeObject(this);
            if(this.Json != json)
            {                
                File.WriteAllText(CONFIG_FILENAME, json);
                this.Json = json;
            }            
        }
    }
}
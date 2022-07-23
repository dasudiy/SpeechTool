using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace SpeechTool
{
    public class Config : INotifyPropertyChanged
    {
        private const string CONFIG_FILENAME = "config.json";
        public event PropertyChangedEventHandler PropertyChanged;

        public Config()
        {
            this.Speed = 1;
            this.Pitch = 1;
        }

        private string f_Key;
        public string Key
        {
            get => f_Key;
            set => SetField(ref f_Key, value);
        }
        private string f_Region;
        public string Region
        {
            get => f_Region;
            set => SetField(ref f_Region, value);
        }
        private string f_OutputFolder;
        public string OutputFolder
        {
            get => f_OutputFolder;
            set => SetField(ref f_OutputFolder, value);
        }
        private decimal f_Pitch;
        public decimal Pitch
        {
            get => f_Pitch;
            set => SetField(ref f_Pitch, value);
        }
        private decimal f_Speed;
        public decimal Speed
        {
            get => f_Speed;
            set => SetField(ref f_Speed, value);
        }
        private string f_Lang;
        public string Lang
        {
            get => f_Lang;
            set => SetField(ref f_Lang, value);
        }
        private string f_Voice;
        public string Voice
        {
            get => f_Voice;
            set => SetField(ref f_Voice, value);
        }
        private string f_Style;
        public string Style
        {
            get => f_Style;
            set => SetField(ref f_Style, value);
        }
        private string f_Role;
        public string Role
        {
            get => f_Role;
            set => SetField(ref f_Role, value);
        }
        private bool f_MP3;
        public bool MP3
        {
            get => f_MP3;
            set => SetField(ref f_MP3, value);
        }
        private bool f_Stero;
        public bool Stero
        {
            get => f_Stero;
            set => SetField(ref f_Stero, value);
        }

        private bool f_SRT;
        public bool SRT
        {
            get => f_SRT;
            set => SetField(ref f_SRT, value);
        }

        private string Json { get; set; }

        internal static Config LoadConfig()
        {
            if (File.Exists(CONFIG_FILENAME))
            {
                var json = File.ReadAllText(CONFIG_FILENAME);
                var config = JsonConvert.DeserializeObject<Config>(json);

                config.PropertyChanged += (sender, e) =>
                {
                    config.SaveConfig();
                };

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
            if (this.Json != json)
            {
                File.WriteAllText(CONFIG_FILENAME, json);
                this.Json = json;
            }
        }


        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
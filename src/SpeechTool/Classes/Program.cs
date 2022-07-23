using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpeechTool.Classes;

namespace SpeechTool
{
    static class Program
    {
        const string CLI_HELP_TEXT = @"this is help text";
        public static SpeechApp App { get; set; }
        public static Config Config { get; set; }


        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) //use async Task will cause BrowserFolderDialog cannot display
        {
            WindowsApi.ShowConsoleWindow();
            Console.WriteLine(@"
   _______ _______ _______ _______ _______ __   __   _______ _______ _______ ___     
  |       |       |       |       |       |  | |  | |       |       |       |   |    
  |  _____|    _  |    ___|    ___|       |  |_|  | |_     _|   _   |   _   |   |    
  | |_____|   |_| |   |___|   |___|       |       |   |   | |  | |  |  | |  |   |    
  |_____  |    ___|    ___|    ___|      _|       |   |   | |  |_|  |  |_|  |   |___ 
   _____| |   |   |   |___|   |___|     |_|   _   |   |   | |       |       |       |
  |_______|___|   |_______|_______|_______|__| |__|   |___| |_______|_______|_______|
   _______ __   __   ______  _______ _______ __   __   ______  ___ __   __           
  |  _    |  | |  | |      ||   _   |       |  | |  | |      ||   |  | |  |          
  | |_|   |  |_|  | |  _    |  |_|  |  _____|  | |  | |  _    |   |  |_|  |          
  |       |       | | | |   |       | |_____|  |_|  | | | |   |   |       |          
  |  _   ||_     _| | |_|   |       |_____  |       | | |_|   |   |_     _|          
  | |_|   | |   |   |       |   _   |_____| |       | |       |   | |   |            
  |_______| |___|   |______||__| |__|_______|_______| |______||___| |___|            
");
            if (args.ArgumentExist("--help"))
            {
                WindowsApi.ShowConsoleWindow();
                Console.WriteLine(CLI_HELP_TEXT);
                Console.Read();
                return;
            }

            Config = Config.LoadConfig();
            Config.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(Config.Key) || e.PropertyName == nameof(Config.Region))
                {
                    App = new SpeechApp(Config.Key, Config.Region);
                }
            };
            App = new SpeechApp(Config.Key, Config.Region);
            
            switch (args.ArgumentRead("--action", RunType.ui))
            {
                case RunType.ui:
                    //WindowsApi.HideConsoleWindow();
                    Application.SetHighDpiMode(HighDpiMode.SystemAware);
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Mainform());
                    break;
                case RunType.tts:
                    _ = App.TextToSpeech(
                        args.ArgumentRead<string>("--sourceText"),
                        args.ArgumentRead<string>("--input"),
                        args.ArgumentExist("--ssml"),
                        args.ArgumentRead<string>("--lang", "zh-CN"),
                        args.ArgumentRead<string>("--voice", "zh-CN-YunxiNeural"),
                        args.ArgumentRead<string>("--output"),
                        args.ArgumentExist("--stereo"),
                        args.ArgumentExist("--mp3"),
                        args.ArgumentExist("--srt"));
                    break;
                case RunType.stt:
                    var output = App.SpeechToText(
                        args.ArgumentRead<string>("--input"),
                        args.ArgumentRead<string>("--lang", null),
                        args.ArgumentRead<string[]>("--phrase", null),
                        args.ArgumentExist("--srt")).Result;
                    Console.WriteLine(output);
                    break;
                default:
                    break;
            }
        }
    }
}

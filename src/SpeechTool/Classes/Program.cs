using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpeechTool.Classes;

namespace SpeechTool
{
    static class Program
    {
        const string CLI_HELP_TEXT = @"帮助
  --help                            显示这个信息
操作
  --action tts|stt                  语音生成|语音识别
语音生成
  --sourceText 需转语音的文字       直接输入文字
  --input 文本文件                  从文件输入
  --ssml                            输入内容为ssml格式，默认为文本格式
  --lang 语言                       输入的语言，如zh-CN
  --voice 声音                      使用的声音，如zh-CN-YunxiNeural
  --stereo                          将输出音频转为立体声，默认为单声道
  --mp3                             将输出音频转码为MP3格式，默认为WAV
  --srt                             同时生成SRT字幕文件
语音识别
  --input 媒体文件                  用语语音识别的音频或视频文件
  --lang 语言                       输入的语言，如zh-CN
  --phrase 短语1,短语2...           用语提高精确度的短语
  --srt                             生成SRT字幕文件
";
        public static SpeechApp App { get; set; }
        public static Config Config { get; set; }


        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) //use async Task will cause BrowserFolderDialog cannot display
        {
            if (args.ArgumentExist("--help"))
            {
                WindowsApi.ShowConsoleWindow();
                Console.WriteLine(CLI_HELP_TEXT);                
                return;
            }

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
                    if (!Config.ShowConsole)
                    {
                        WindowsApi.HideConsoleWindow();
                    }                    
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

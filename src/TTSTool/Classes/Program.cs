using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTSTool.Classes;

namespace TTSTool
{
    static class Program
    {
        const string CLI_HELP_TEXT = @"";
        static SpeechHelper helper;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main(string[] args)
        {
            if (args.ArgumentExist("--help"))
            {
                Console.WriteLine(CLI_HELP_TEXT);
                return;
            }

            var config = Config.LoadConfig();
            helper = new SpeechHelper(config.Key, config.Region);
            switch (args.ArgumentRead("--action", RunType.ui))
            {
                case RunType.ui:
                    Application.SetHighDpiMode(HighDpiMode.SystemAware);
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Mainform());
                    break;
                case RunType.tts:
                    await helper.TextToSpeech(
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
                    var output = await helper.SpeechToText(
                        args.ArgumentRead<string>("--input"),
                        args.ArgumentRead<string>("--lang", null),
                        args.ArgumentRead<string[]>("--phrase", null),
                        args.ArgumentExist("--srt"));
                    Console.WriteLine(output);
                    break;
                default:
                    break;
            }
        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace TTSTool.Classes
//{
//    class SpeechApp
//    {
//        const string CLI_HELP_TEXT = @"";
//        static SpeechHelper helper;

//        public static async Task Run(string[] args)
//        {
//            if (args.ArgumentExist("--help"))
//            {
//                Console.WriteLine(CLI_HELP_TEXT);
//                return;
//            }

//            switch (args.ArgumentRead("--action", RunType.ui))
//            {
//                case RunType.ui:
//                    Application.SetHighDpiMode(HighDpiMode.SystemAware);
//                    Application.EnableVisualStyles();
//                    Application.SetCompatibleTextRenderingDefault(false);
//                    Application.Run(new Mainform());
//                    break;
//                case RunType.tts:
//                    await helper.TextToSpeech(
//                        args.ArgumentRead<string>("--sourceText"),
//                        args.ArgumentRead<string>("--input"),
//                        args.ArgumentRead<string>("--lang", "zh-CN"),
//                        args.ArgumentRead<string>("--voice", "zh-CN-YunxiNeural"),
//                        args.ArgumentRead<string>("--output"),
//                        args.ArgumentExist("--stereo"),
//                        args.ArgumentExist("--mp3"));
//                    break;
//                case RunType.stt:
//                    await helper.SpeechToText(
//                        args.ArgumentRead<string>("--input"),
//                        args.ArgumentRead<string>("--lang", null),
//                        args.ArgumentRead<string>("--phrase", null).Split(','),
//                        args.ArgumentRead<string>("--srt", null));
//                    break;
//                default:
//                    break;
//            }
//        }
//    }
//}

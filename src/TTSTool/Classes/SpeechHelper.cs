using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using NAudio.Lame;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TTSTool.Classes
{
    public class SpeechHelper
    {
        private IReadOnlyCollection<VoiceInfo> voiceInfos;
        public string Key { get; set; }
        public string Region { get; set; }

        public SpeechHelper(string key, string region)
        {
            this.Key = key;
            this.Region = region;
        }

        public async Task<string> SpeechToText(string sourceMediaFile,
            string lang = null,
            string[] phraseList = null,
            bool generateSRTFile = false,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            Action<SpeechRecognitionEventArgs> action = null;
            using (var srtBuilder = new SRTBuilder(sourceMediaFile + ".srt"))
            {
                if (generateSRTFile)
                {

                    action = new Action<SpeechRecognitionEventArgs>(e =>
                    {
                        srtBuilder.Write(e.Result.OffsetInTicks, e.Result.Duration, e.Result.Text);
                    });

                }
                return await SpeechToText(sourceMediaFile, lang, phraseList, action, cancellationToken);
            }
        }

        public async Task<string> SpeechToText(string sourceMediaFile,
            string lang = null,
            string[] phraseList = null,
            Action<SpeechRecognitionEventArgs> recognizedFn = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var fs = new FileStream(sourceMediaFile, System.IO.FileMode.Open, FileAccess.Read))
            {
                using (var mediaFoundationReader = new StreamMediaFoundationReader(fs))
                {
                    var sampleprovider = mediaFoundationReader.ToSampleProvider();
                    var monoStream = sampleprovider.ToMono().ToWaveProvider16();

                    var format = AudioStreamFormat.GetWaveFormatPCM(
                        Convert.ToUInt32(monoStream.WaveFormat.SampleRate),
                        Convert.ToByte(monoStream.WaveFormat.BitsPerSample),
                        (byte)1);
                    using (var config = AudioConfig.FromStreamInput(
                        new PullAudioInputStreamAdapter((buffer, size) => monoStream.Read(buffer, 0, Convert.ToInt32(size))),
                        format))
                    {
                        using (var recognizer = CreateSpeechRecognizer(config, c => c.SpeechRecognitionLanguage = lang))
                        {
                            var tcs = new TaskCompletionSource<int>();
                            var sb = new StringBuilder();
                            recognizer.Recognized += (sender, e) =>
                            {
                                if (e.Result.Reason == ResultReason.RecognizedSpeech)
                                {
                                    sb.AppendLine(e.Result.Text);
                                    recognizedFn.Invoke(e);
                                }
                            };

                            recognizer.Canceled += (sender, e) =>
                            {
                                tcs.TrySetResult(0);
                            };
                            recognizer.SessionStopped += (sender, e) =>
                            {
                                tcs.TrySetResult(0);
                            };

                            if (phraseList != null)
                            {
                                var phraseListGrammar = PhraseListGrammar.FromRecognizer(recognizer);
                                foreach (var item in phraseList)
                                {
                                    phraseListGrammar.AddPhrase(item);
                                }
                            }

                            await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);
                            tcs.Task.Wait(cancellationToken);
                            await recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);

                            return sb.ToString();
                        }

                    }
                }
            }
        }

        public async Task TextToSpeech(string sourceText,
            string inputFile,
            bool sourceIsSSML,
            string lang,
            string voice,
            string outputFile,
            bool toStereo,
            bool encodeToMP3,
            bool generateSRTFile)
        {
            if (string.IsNullOrWhiteSpace(sourceText) && File.Exists(inputFile))
            {
                sourceText = await File.ReadAllTextAsync(inputFile, Encoding.UTF8);
            }
            if (string.IsNullOrWhiteSpace(outputFile))
            {
                outputFile = sourceText + (encodeToMP3 ? ".mp3" : ".wav");
            }

            byte[] binary;
            using (var ms = new MemoryStream())
            {
                using (var waveWriter = new WaveFileWriter(ms, new WaveFormat(16000, 16, 1)))
                {
                    using (var audioConfig = AudioConfig.FromStreamOutput(new PushAudioOutputStreamAdapter(b =>
                    {
                        waveWriter.Write(b, 0, b.Length);
                        return Convert.ToUInt32(b.Length);
                    })))
                    {
                        using (var synth = CreateSpeechSynthesizer(audioConfig, c =>
                        {
                            c.SpeechSynthesisLanguage = lang;
                            c.SpeechSynthesisVoiceName = voice;
                        }))
                        {
                            if (generateSRTFile)
                            {
                                var tcs = new TaskCompletionSource<int>();

                                if (!sourceIsSSML)
                                {
                                    sourceText = GetSSMLWithAutoAddBookmark(sourceText, voice, "。", "\r\n", "\n", "，");
                                    sourceIsSSML = true;
                                    await File.WriteAllTextAsync(outputFile + ".xml", sourceText, Encoding.UTF8);
                                }
                                var srtBuilder = new SRTBuilder(outputFile + ".srt");
                                synth.BookmarkReached += (sender, e) =>
                                {
                                    srtBuilder.Write(e.AudioOffset.To<long>() * 3, e.Text);
                                };
                                _ = tcs.Task.ContinueWith(t => srtBuilder.Dispose());
                                synth.SynthesisCompleted += (sender, e) => tcs.SetResult(0);
                                synth.SynthesisCanceled += (sender, e) => tcs.SetResult(1);
                            }

                            using (var result = await (sourceIsSSML ? synth.SpeakSsmlAsync(sourceText) : synth.SpeakTextAsync(sourceText)))
                            {
                                if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                                {
                                    Console.WriteLine($"Speech synthesized to speaker for text [{sourceText}]");
                                }
                                else if (result.Reason == ResultReason.Canceled)
                                {
                                    var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                                    Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                                    if (cancellation.Reason == CancellationReason.Error)
                                    {
                                        Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                                        Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                                        Console.WriteLine($"CANCELED: Did you update the subscription info?");
                                    }
                                }
                            }
                        }
                    }
                }
                binary = ms.ToArray();
            }

            var ms2 = new MemoryStream(binary);
            try
            {
                if (toStereo)
                {
                    using (var originalStream = ms2)
                    {
                        originalStream.Position = 0;
                        ms2 = new MemoryStream();

                        using (var inputReader = new WaveFileReader(originalStream))
                        {
                            WaveFileWriter.WriteWavFileToStream(ms2, inputReader.ToSampleProvider().ToStereo().ToWaveProvider16());
                        }
                    }
                }

                if (encodeToMP3)
                {
                    using (var originalStream = ms2)
                    {
                        originalStream.Position = 0;
                        ms2 = new MemoryStream();

                        using (var inputReader = new WaveFileReader(originalStream))
                        {
                            using (var wtr = new LameMP3FileWriter(ms2, inputReader.WaveFormat, 192))
                            {
                                await inputReader.CopyToAsync(wtr);
                                await wtr.FlushAsync();
                            }
                        }
                    }
                }

                using (var fs = new FileStream(outputFile, FileMode.Create))
                {
                    ms2.Position = 0;
                    await ms2.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
            }
            finally
            {
                ms2.Dispose();
            }

        }

        public string GetSSMLWithAutoAddBookmark(string sourceText, string voiceName, params string[] bookmarkSymbols)
        {
            XElement voice;
            var doc = new XDocument(SSMLHelper.BuildSpeak(voice = SSMLHelper.BuildVoice(voiceName)));

            foreach (var item in sourceText.Split(bookmarkSymbols, StringSplitOptions.RemoveEmptyEntries))
            {
                voice.Add(item);
                voice.Add("，");
                voice.Add(SSMLHelper.BuildBookmark(item));
            }
            return doc.ToString();
        }


        public async Task<IEnumerable<VoiceInfo>> GetAllVoice()
        {
            if (voiceInfos == null)
            {
                using (var synth = CreateSpeechSynthesizer())
                {
                    var result = await synth.GetVoicesAsync();
                    //TODO: check success
                    voiceInfos = result.Voices;
                }
            }
            return this.voiceInfos;
        }

        private SpeechSynthesizer CreateSpeechSynthesizer(AudioConfig audioConfig = null, Action<SpeechConfig> configFn = null)
        {
            var config = SpeechConfig.FromSubscription(Key, Region);
            configFn?.Invoke(config);
            if (audioConfig != null)
            {
                return new SpeechSynthesizer(config, audioConfig);
            }
            else
            {
                return new SpeechSynthesizer(config);
            }

        }

        private SpeechRecognizer CreateSpeechRecognizer(AudioConfig audioConfig, Action<SpeechConfig> configFn = null)
        {
            var config = SpeechConfig.FromSubscription(Key, Region);
            configFn?.Invoke(config);
            return new SpeechRecognizer(config, audioConfig);
        }
    }
}

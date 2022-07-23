using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechTool.Classes
{
    public class SRTBuilder : IDisposable
    {
        public int SEQ { get; set; }
        private long lastEndOffset = 0;
        private Lazy<StreamWriter> writer;
        public StreamWriter Writer => writer.Value;

        private List<SubtitleCacheItem> cache = new List<SubtitleCacheItem>();

        public SRTBuilder(string srtFileName)
        {
            SEQ = 1;
            writer = new Lazy<StreamWriter>(() => new StreamWriter(srtFileName, false, Encoding.UTF8));
        }

        public void Write(long offsetInTicks, TimeSpan duration, string text)
        {
            var startTime = new DateTime(offsetInTicks);
            var endTime = startTime.Add(duration);
            Writer.WriteLine($"{SEQ++}");
            Writer.WriteLine($"{startTime:HH:mm:ss,fff} --> {endTime:HH:mm:ss,fff}");
            Writer.WriteLine($"{text}");
            Writer.WriteLine();
        }

        public void Write(long offsetInTicks, string text)
        {
            cache.Add(new SubtitleCacheItem(lastEndOffset, offsetInTicks, text));
            lastEndOffset = offsetInTicks;
        }

        public void FixLength(long correctAudioLength)
        {
            //bookmark offset时间错但相对长度正确，按最终wave文件时长修复，可能与https://github.com/Azure-Samples/cognitive-services-speech-sdk/issues/1245 关联，使用sdk 1.22.0
            if (cache.Count > 0)
            {
                foreach (var item in cache)
                {
                    item.StartOffset = ConvertExtension.MapValue(item.StartOffset, 0, cache.Last().EndOffset, 0, correctAudioLength).To<long>();
                    item.EndOffset = ConvertExtension.MapValue(item.EndOffset, 0, cache.Last().EndOffset, 0, correctAudioLength).To<long>();
                }
            }
        }

        public void Dispose()
        {
            if (cache.Count > 0)
            {
                foreach (var item in cache)
                {
                    var startTime = new DateTime(item.StartOffset);
                    var endTime = new DateTime(item.EndOffset);

                    Writer.WriteLine($"{SEQ++}");
                    Writer.WriteLine($"{startTime:HH:mm:ss,fff} --> {endTime:HH:mm:ss,fff}");
                    Writer.WriteLine($"{item.Text}");
                    Writer.WriteLine();
                }
            }
            Writer?.Dispose();
        }

        private class SubtitleCacheItem
        {
            public SubtitleCacheItem(long startOffset, long endOffset, string text)
            {
                StartOffset = startOffset;
                EndOffset = endOffset;
                Text = text;
            }

            public long StartOffset { get; set; }
            public long EndOffset { get; set; }
            public string Text { get; set; }
        }
    }
}

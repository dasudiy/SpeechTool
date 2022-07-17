using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TTSTool.Classes
{
    public class SRTBuilder : IDisposable
    {
        public int SEQ { get; set; }
        private long lastEndOffset = 0;
        private Lazy<StreamWriter> writer;
        public StreamWriter Writer => writer.Value;

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
            var startTime = new DateTime(lastEndOffset);
            var endTime = new DateTime(offsetInTicks);
            Writer.WriteLine($"{SEQ++}");
            Writer.WriteLine($"{startTime:HH:mm:ss,fff} --> {endTime:HH:mm:ss,fff}");
            Writer.WriteLine($"{text}");
            Writer.WriteLine();
            lastEndOffset = offsetInTicks;
        }

        public void Dispose()
        {
            Writer?.Dispose();
        }
    }
}

using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechTool.Classes
{
    public class PullAudioInputStreamAdapter : PullAudioInputStreamCallback
    {
        public PullAudioInputStreamAdapter(Func<byte[], uint, int> readFn)
        {
            this.ReadFn = readFn;
        }

        public Func<byte[], uint, int> ReadFn { get; }

        public override int Read(byte[] dataBuffer, uint size) => ReadFn(dataBuffer, size);
    }

    public class PushAudioOutputStreamAdapter : PushAudioOutputStreamCallback
    {
        public PushAudioOutputStreamAdapter(Func<byte[], uint> writeFn)
        {
            this.WriteFn = writeFn;
        }

        public Func<byte[], uint> WriteFn { get; }


        public override uint Write(byte[] dataBuffer)
        {
            return WriteFn(dataBuffer);
        }
    }

}

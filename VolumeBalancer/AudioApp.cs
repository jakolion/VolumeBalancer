using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;

namespace VolumeBalancer
{
    class AudioApp
    {
        public AudioSessionControl session { get; }
        public string path { get; }

        public AudioApp(AudioSessionControl session, string path)
        {
            this.session = session;
            this.path = path;
        }

        public override string ToString()
        {
            return path;
        }
    }
}

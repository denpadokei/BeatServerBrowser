using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BeatServerBrowser.Core.Interfaces
{
    public interface ISoundPlayerService : IDisposable
    {
        public void Play(FileInfo soundFileInfo);

        public void Stop();
    }
}

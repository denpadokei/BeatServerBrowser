using System;
using System.IO;

namespace BeatServerBrowser.Core.Interfaces
{
    public interface ISoundPlayerService : IDisposable
    {
        public void Play(FileInfo soundFileInfo);

        public void Stop();
    }
}

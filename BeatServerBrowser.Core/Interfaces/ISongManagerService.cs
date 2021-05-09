using BeatServerBrowser.Core.Models;
using System;

namespace BeatServerBrowser.Core.Interfaces
{
    public interface ISongManagerService
    {
        public void Serch();

        void Delete(LocalBeatmapInfo beatmap);

        event Action SongDeleted;
    }
}

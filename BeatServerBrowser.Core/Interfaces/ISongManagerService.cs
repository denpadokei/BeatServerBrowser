using BeatServerBrowser.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeatServerBrowser.Core.Interfaces
{
    public interface ISongManagerService
    {
        public void Serch();

        void Delete(LocalBeatmapInfo beatmap);

        Action SongDeleted { get; set; }
    }
}

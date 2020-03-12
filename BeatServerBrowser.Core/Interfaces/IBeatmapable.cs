using BeatSaverSharp;
using BeatServerBrowser.Core.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeatServerBrowser.Core.Interfaces
{
    public interface IBeatmapable
    {
        MTObservableCollection<IBeatmapEntityable> Beatmaps { get; set; }
        public void Serch();
    }
}

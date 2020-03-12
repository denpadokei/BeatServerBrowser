using BeatSaverSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Media;

namespace BeatServerBrowser.Core.Interfaces
{
    public interface IBeatmapEntityable : INotifyPropertyChanged
    {
        Beatmap Beatmap { get; set; }
        ImageSource Cover { get; set; }
        string SongTitle { get; }
    }
}

using System;
using System.ComponentModel;

namespace BeatServerBrowser.Core.Interfaces
{
    public interface IWindowPanel : INotifyPropertyChanged, IInitializable
    {
        string Title { get; set; }
        Object CurrentListViewContext { get; set; }
    }
}

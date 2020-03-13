using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BeatServerBrowser.Core.Interfaces
{
    public interface IWindowPanel : INotifyPropertyChanged, IInitializable
    {
        string Title { get; set; }
        Object CurrentListViewContext { get; set; }
    }
}

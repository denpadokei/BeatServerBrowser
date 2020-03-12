using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BeatServerBrowser.Core.Interfaces
{
    public interface IWindowPanel : INotifyPropertyChanged
    {
        string Title { get; set; }
        Object CurrentListViewContext { get; set; }
    }
}

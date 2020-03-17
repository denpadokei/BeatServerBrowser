using System;
using System.Collections.Generic;
using System.Text;

namespace BeatServerBrowser.Core.Interfaces
{
    public interface ILoadingService
    {
        bool IsLoading { get; set; }
        void Load(Action action);
    }
}

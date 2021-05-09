using System;

namespace BeatServerBrowser.Core.Interfaces
{
    public interface ILoadingService
    {
        bool IsLoading { get; set; }
        void Load(Action action);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BeatServerBrowser.Core.Interfaces
{
    public interface ILoadingService
    {
        public bool IsLoading { get; set; }
        public void Load(Action action);
    }
}

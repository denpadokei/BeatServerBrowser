using BeatServerBrowser.Core.Models;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;

namespace BeatServerBrowser.Core.Interfaces
{
    public interface ICustomDialogService
    {
        void Show(string name, IDialogParameters parameters, Action<IDialogResult> callback);
        void ShowDialog(string name, IDialogParameters parameters, Action<IDialogResult> callback);
        void ShowOpenFileDialog(OpenFileDialogParameters parameters, out IEnumerable<string> fileNames);
    }
}

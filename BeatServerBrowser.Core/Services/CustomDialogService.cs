using BeatServerBrowser.Core.Interfaces;
using BeatServerBrowser.Core.Models;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Shell;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;

namespace BeatServerBrowser.Core.Services
{
    public class CustomDialogService : ICustomDialogService
    {
        [Dependency]
        public IDialogService dialogService_;

        public void Show(string name, IDialogParameters parameters, Action<IDialogResult> callback)
        {
            this.dialogService_?.Show(name, parameters, callback);
        }

        public void ShowDialog(string name, IDialogParameters parameters, Action<IDialogResult> callback)
        {
            this.dialogService_?.ShowDialog(name, parameters, callback);
        }

        public void ShowOpenFileDialog(OpenFileDialogParameters parameters, out IEnumerable<string> fileNames)
        {
            var window = new CommonOpenFileDialog()
            {
                EnsureFileExists = parameters.EnsureFileExists,
                EnsureReadOnly = parameters.EnsureReadOnly,
                EnsurePathExists = parameters.EnsurePathExists,
                EnsureValidNames = parameters.EnsureValidNames,
                AllowPropertyEditing = parameters.AllowPropertyEditing,
                AddToMostRecentlyUsedList = parameters.AddToMostRecentlyUsedList,
                AllowNonFileSystemItems = parameters.AllowNonFileSystemItems,
                IsFolderPicker = parameters.IsFolderPicker,
                Multiselect = parameters.Multiselect,
                RestoreDirectory = parameters.RestoreDirectory,
                ShowPlacesList = parameters.ShowPlacesList,
                ShowHiddenItems = parameters.ShowHiddenItems,
                NavigateToShortcut = parameters.NavigateToShortcut,
                InitialDirectory = parameters.InitialDirectory,
                DefaultDirectory = parameters.DefaultDirectory,
                DefaultDirectoryShellContainer = parameters.DefaultDirectoryShellContainer,
                CookieIdentifier = parameters.CookieIdentifier,
                DefaultFileName = parameters.DefaultFileName,
                DefaultExtension = parameters.DefaultExtension,
                Title = parameters.Title
            };
            if (parameters.FilterList != null) {
                foreach (var filter in parameters.FilterList) {
                    window.Filters.Add(new CommonFileDialogFilter(filter.Key, filter.Value));
                }
            }
            var result = window.ShowDialog();

            if (result == CommonFileDialogResult.Ok) {
                var list = new List<string>();
                foreach (var item in window.FileNames) {
                    list.Add(item);
                }
                fileNames = list;
            }
            else {
                fileNames = null;
            }
        }

        public CustomDialogService()
        {

        }
    }
}

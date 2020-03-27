using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeatServerBrowser.Core.Models
{
    public struct OpenFileDialogParameters
    {

        //
        // 概要:
        //     Gets or sets a value that specifies whether the returned file must be in an existing
        //     folder.
        //
        // 例外:
        //   T:System.InvalidOperationException:
        //     This property cannot be set when the dialog is visible.
        public bool EnsurePathExists { get; set; }
        //
        // 概要:
        //     Gets or sets a value that determines whether to validate file names.
        //
        // 例外:
        //   T:System.InvalidOperationException:
        //     This property cannot be set when the dialog is visible.
        public bool EnsureValidNames { get; set; }
        //
        // 概要:
        //     Gets or sets a value that determines whether read-only items are returned. Default
        //     value for CommonOpenFileDialog is true (allow read-only files) and CommonSaveFileDialog
        //     is false (don't allow read-only files).
        //
        // 例外:
        //   T:System.InvalidOperationException:
        //     This property cannot be set when the dialog is visible.
        public bool EnsureReadOnly { get; set; }
        //
        // 概要:
        //     Gets or sets a value that determines the restore directory.
        //
        // 例外:
        //   T:System.InvalidOperationException:
        //     This property cannot be set when the dialog is visible.
        public bool RestoreDirectory { get; set; }
        //
        // 概要:
        //     Gets or sets a value that controls whether to show or hide the list of pinned
        //     places that the user can choose.
        //
        // 例外:
        //   T:System.InvalidOperationException:
        //     This property cannot be set when the dialog is visible.
        public bool ShowPlacesList { get; set; }
        //
        // 概要:
        //     Gets or sets a value that controls whether to show or hide the list of places
        //     where the user has recently opened or saved items.
        //
        // 例外:
        //   T:System.InvalidOperationException:
        //     This property cannot be set when the dialog is visible.
        public bool AddToMostRecentlyUsedList { get; set; }
        //
        // 概要:
        //     Gets or sets a value that controls whether to show hidden items.
        //
        // 例外:
        //   T:System.InvalidOperationException:
        //     This property cannot be set when the dialog is visible.
        public bool ShowHiddenItems { get; set; }
        //
        // 概要:
        //     Gets or sets a value that controls whether properties can be edited.
        public bool AllowPropertyEditing { get; set; }
        //
        // 概要:
        //     Gets or sets a value that controls whether shortcuts should be treated as their
        //     target items, allowing an application to open a .lnk file.
        //
        // 例外:
        //   T:System.InvalidOperationException:
        //     This property cannot be set when the dialog is visible.
        public bool NavigateToShortcut { get; set; }
        //
        // 概要:
        //     Gets or sets a value that determines whether the file must exist beforehand.
        //
        // 例外:
        //   T:System.InvalidOperationException:
        //     This property cannot be set when the dialog is visible.
        public bool EnsureFileExists { get; set; }
        //
        // 概要:
        //     Gets or sets the initial directory displayed when the dialog is shown. A null
        //     or empty string indicates that the dialog is using the default directory.
        public string InitialDirectory { get; set; }
        //
        // 概要:
        //     Gets or sets a location that is always selected when the dialog is opened, regardless
        //     of previous user action. A null value implies that the dialog is using the default
        //     location.
        public ShellContainer InitialDirectoryShellContainer { get; set; }
        //
        // 概要:
        //     Sets the folder and path used as a default if there is not a recently used folder
        //     value available.
        public string DefaultDirectory { get; set; }
        //
        // 概要:
        //     Sets the location (ShellContainer used as a default if there is not a recently
        //     used folder value available.
        public ShellContainer DefaultDirectoryShellContainer { get; set; }
        //
        // 概要:
        //     Gets or sets a value that enables a calling application to associate a GUID with
        //     a dialog's persisted state.
        public Guid CookieIdentifier { get; set; }
        //
        // 概要:
        //     Default file name.
        public string DefaultFileName { get; set; }
        //
        // 概要:
        //     Gets or sets the default file extension to be added to file names. If the value
        //     is null or string.Empty, the extension is not added to the file names.
        public string DefaultExtension { get; set; }
        //
        // 概要:
        //     Gets or sets the dialog title.
        public string Title { get; set; }

        //
        // 概要:
        //     Gets or sets a value that determines whether the user can select more than one
        //     file.
        public bool Multiselect { get; set; }
        //
        // 概要:
        //     Gets or sets a value that determines whether the user can select folders or files.
        //     Default value is false.
        public bool IsFolderPicker { get; set; }
        //
        // 概要:
        //     Gets or sets a value that determines whether the user can select non-filesystem
        //     items, such as Library, Search Connectors, or Known Folders.
        public bool AllowNonFileSystemItems { get; set; }

        public Dictionary<string, string> FilterList { get; set; }
    }
}

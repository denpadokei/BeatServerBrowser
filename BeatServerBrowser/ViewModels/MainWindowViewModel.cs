using BeatServerBrowser.Core.Bases;
using BeatServerBrowser.Core.Models;
using MaterialDesignThemes.Wpf;
using Prism.Mvvm;
using System.ComponentModel;
using System.Configuration;
using System.Windows;

namespace BeatServerBrowser.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public string Title {get; set;}
        public MainWindowViewModel()
        {
            this.Title = "BeatSaverBrowser";
        }
    }
}

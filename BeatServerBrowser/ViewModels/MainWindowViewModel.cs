using BeatServerBrowser.Core.Bases;
using BeatServerBrowser.Core.Models;
using MaterialDesignThemes.Wpf;
using Prism.Mvvm;
using System.ComponentModel;
using System.Configuration;
using System.Windows;

namespace BeatServerBrowser.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _title = "BeatSaverBrowser";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            ConfigMaster.Current.IsDark = Core.Properties.Settings.Default.IsDark;

            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();

            var baseTheme = ConfigMaster.Current.IsDark ? new MaterialDesignDarkTheme() : (IBaseTheme)new MaterialDesignLightTheme();

            theme.SetBaseTheme(baseTheme);
            paletteHelper.SetTheme(theme);

            WeakEventManager<INotifyPropertyChanged, PropertyChangedEventArgs>.AddHandler(
                ConfigMaster.Current, nameof(INotifyPropertyChanged.PropertyChanged), this.OnPropertyChangeConfigMaster);
        }

        private void OnPropertyChangeConfigMaster(object sender, PropertyChangedEventArgs e)
        {
            if (sender is ConfigMaster && e.PropertyName == nameof(ConfigMaster.IsDark)) {
                var paletteHelper = new PaletteHelper();
                var theme = paletteHelper.GetTheme();

                var baseTheme = ConfigMaster.Current.IsDark ? new MaterialDesignDarkTheme() : (IBaseTheme)new MaterialDesignLightTheme();

                theme.SetBaseTheme(baseTheme);
                paletteHelper.SetTheme(theme);

                Core.Properties.Settings.Default.IsDark = this.Config.IsDark;
                Core.Properties.Settings.Default.Save();
            }
        }

        public MainWindowViewModel()
        {

        }
    }
}

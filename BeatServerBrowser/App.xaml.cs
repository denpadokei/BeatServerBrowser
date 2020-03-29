using Prism.Ioc;
using BeatServerBrowser.Views;
using System.Windows;
using Prism.Regions;
using Prism.Modularity;
using BeatServerBrowser.Home;
using NLog;
using NLog.Targets;
using NLog.Config;
using System.Diagnostics;
using System;
using MaterialDesignThemes.Wpf;
using System.Configuration;
using System.IO;
using BeatServerBrowser.Core.Models;
using BeatServerBrowser.Setting;
using BeatServerBrowser.Serch;
using BeatServerBrowser.List;
using BeatServerBrowser.Core;
using BeatServerBrowser.Local;
using BeatServerBrowser.PlayList;
using BeatServerBrowser.Core.Services;

namespace BeatServerBrowser
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            var logger = LogManager.GetCurrentClassLogger();
            logger.Info("BeatServerBrowserを起動します。");
            Debug.WriteLine("BeatServerBrowserを起動します。");

            try {
                this.Container.Resolve<IRegionManager>()?.RequestNavigate("ContentRegion", "HomeView");
            }
            catch (Exception e) {
                Debug.WriteLine(e);
                logger.Error(e);
            }
            
        }

        public override void Initialize()
        {
            base.Initialize();
            var config = new LoggingConfiguration();

            var file = new FileTarget("logfile") { FileName = "log.txt", ConcurrentWrites = true };
            var consol = new ConsoleTarget("logconsole");

            config.AddRule(LogLevel.Trace, LogLevel.Fatal, file);
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, consol);

            LogManager.Configuration = config;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<HomeModule>();
            moduleCatalog.AddModule<ListModule>();
            moduleCatalog.AddModule<CoreModule>();
            moduleCatalog.AddModule<LocalModule>();
            moduleCatalog.AddModule<PlayListModule>();
            moduleCatalog.AddModule<SerchModule>();
            moduleCatalog.AddModule<SettingModule>();
            base.ConfigureModuleCatalog(moduleCatalog);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            SoundPlayerService.CurrentPlayer.Stop();
        }
    }
}

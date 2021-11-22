using BeatServerBrowser.Core;
using BeatServerBrowser.Core.Models;
using BeatServerBrowser.Core.Services;
using BeatServerBrowser.Home;
using BeatServerBrowser.List;
using BeatServerBrowser.Local;
using BeatServerBrowser.PlayList;
using BeatServerBrowser.Serch;
using BeatServerBrowser.Setting;
using BeatServerBrowser.Views;
using NLog;
using NLog.Config;
using NLog.Targets;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace BeatServerBrowser
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return this.Container.Resolve<MainWindow>();
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

        protected override void Initialize()
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

        protected override void OnExit(ExitEventArgs args)
        {
            SoundPlayerService.CurrentPlayer.Stop();
            if (!Directory.Exists(ConfigMaster.TempralyDirectory)) {
                Directory.CreateDirectory(ConfigMaster.TempralyDirectory);
            }
            var info = new DirectoryInfo(ConfigMaster.TempralyDirectory);
            foreach (var folder in info.EnumerateDirectories()) {
                try {
                    foreach (var item in folder.EnumerateFiles()) {
                        item.Delete();
                    }
                    folder.Delete();
                }
                catch (Exception e) {
                    Debug.WriteLine(e);
                }
            }
            base.OnExit(args);
        }
    }
}

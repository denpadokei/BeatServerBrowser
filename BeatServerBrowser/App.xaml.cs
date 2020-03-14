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
using Microsoft.Extensions.Configuration;
using MaterialDesignThemes.Wpf;
using System.Configuration;
using System.IO;
using BeatServerBrowser.Core.Classes;
using BeatServerBrowser.Setting;
using BeatServerBrowser.Serch;

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
            var reagionManager = this.Container.Resolve<IRegionManager>();
            reagionManager.RequestNavigate("ContentRegion", "HomeRegion");

            var logger = LogManager.GetCurrentClassLogger();
            logger.Info("BeatServerBrowserを起動します。");
            Debug.WriteLine("BeatServerBrowserを起動します。");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<HomeModule>();
            moduleCatalog.AddModule<SerchModule>();
            moduleCatalog.AddModule<SettingModule>();
        }
    }
}

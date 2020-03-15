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
using BeatServerBrowser.Core.Models;
using BeatServerBrowser.Setting;
using BeatServerBrowser.Serch;
using BeatServerBrowser.List;

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

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<HomeModule>();
            moduleCatalog.AddModule<ListModule>();
            moduleCatalog.AddModule<SerchModule>();
            moduleCatalog.AddModule<SettingModule>();
            base.ConfigureModuleCatalog(moduleCatalog);
        }
    }
}

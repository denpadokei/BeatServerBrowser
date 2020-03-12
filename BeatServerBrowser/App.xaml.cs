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

namespace BeatServerBrowser
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //var logconfig = new LoggingConfiguration();
            
            //var file = new FileTarget("log.txt") { FileName = "log.txt", ConcurrentWrites = true };
            //var console = new ConsoleTarget("logconsole");

            //logconfig.AddRule(LogLevel.Trace, LogLevel.Fatal, file);
            //logconfig.AddRule(LogLevel.Trace, LogLevel.Fatal, console);

            ////logconfig.AddTarget(console);
            ////logconfig.AddTarget(file);

            //LogManager.Configuration = logconfig;

            base.OnStartup(e);
        }

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
            //containerRegistry.RegisterForNavigation<Main>("MainView");
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<HomeModule>();
        }
    }
}

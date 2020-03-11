using Prism.Ioc;
using BeatServerBrowser.Views;
using System.Windows;
using Prism.Regions;
using Prism.Modularity;
using BeatServerBrowser.Home;

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

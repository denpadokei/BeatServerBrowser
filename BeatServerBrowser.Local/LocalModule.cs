using BeatServerBrowser.Local.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace BeatServerBrowser.Local
{
    public class LocalModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("LocalListRegion", typeof(LocalList));
            regionManager.RegisterViewWithRegion("LocalPanelRegion", typeof(LocalPanel));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation(typeof(LocalMain), "LocalMain");
        }
    }
}
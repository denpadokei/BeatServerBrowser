using BeatServerBrowser.Serch.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace BeatServerBrowser.Serch
{
    public class SerchModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("SerchListRegion", typeof(ListView));
            regionManager.RegisterViewWithRegion("SerchPanelRegion", typeof(PanelView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry) => containerRegistry.RegisterForNavigation(typeof(SerchMain), "SerchMain");
    }
}
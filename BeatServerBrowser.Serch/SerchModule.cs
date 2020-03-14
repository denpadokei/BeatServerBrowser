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
            var regionManger = containerProvider.Resolve<IRegionManager>();
            regionManger.RegisterViewWithRegion("SerchListRegion", typeof(ListView));
            regionManger.RegisterViewWithRegion("SerchPanelRegion", typeof(PanelView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation(typeof(SerchMain), "SerchMain");
        }
    }
}
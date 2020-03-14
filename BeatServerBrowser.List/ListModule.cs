using BeatServerBrowser.List.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace BeatServerBrowser.List
{
    public class ListModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ListListRegion", typeof(ListView));
            regionManager.RegisterViewWithRegion("ListPanelRegion", typeof(PanelView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation(typeof(ListMain), nameof(ListMain));
        }
    }
}
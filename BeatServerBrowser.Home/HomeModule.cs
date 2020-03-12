using BeatServerBrowser.Home.UserControls;
using BeatServerBrowser.Home.ViewModels;
using BeatServerBrowser.Home.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace BeatServerBrowser.Home
{
    public class HomeModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("PanelList", typeof(PanelList));
            regionManager.RegisterViewWithRegion("GridList", typeof(GridList));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<HomeView>("HomeRegion");
            containerRegistry.RegisterForNavigation<PanelList>("PanelList");
            containerRegistry.RegisterForNavigation<GridList>("GridList");
        }
    }
}
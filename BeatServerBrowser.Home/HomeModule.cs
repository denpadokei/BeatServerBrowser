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

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<HomeView>("HomeRegion");
        }
    }
}
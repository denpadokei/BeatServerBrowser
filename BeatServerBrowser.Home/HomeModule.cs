using BeatServerBrowser.Home.ViewModels;
using BeatServerBrowser.Home.Views;
using NLog;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Diagnostics;

namespace BeatServerBrowser.Home
{
    public class HomeModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            try {
                containerProvider.Resolve<IRegionManager>().RegisterViewWithRegion("ContentRegion", typeof(HomeView));
            }
            catch (Exception e) {
                Debug.WriteLine(e);
                LogManager.GetCurrentClassLogger().Error(e);
            }
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation(typeof(HomeView), "HomeView");
        }
    }
}
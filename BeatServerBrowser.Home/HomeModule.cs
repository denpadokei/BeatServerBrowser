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
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation(typeof(HomeView), "HomeView");
        }
    }
}
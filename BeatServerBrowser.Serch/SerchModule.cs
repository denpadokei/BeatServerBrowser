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

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<SerchMain>("SerchMainView");
            containerRegistry.Register<ListView>("ListView");
            containerRegistry.Register<PanelView>("PanelView");
        }
    }
}
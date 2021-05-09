using BeatServerBrowser.Setting.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace BeatServerBrowser.Setting
{
    public class SettingModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var reagionManager = containerProvider.Resolve<IRegionManager>();
            reagionManager.RegisterViewWithRegion("SettingRegion", typeof(SettingView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry) => containerRegistry.RegisterDialog<SettingView>();
    }
}
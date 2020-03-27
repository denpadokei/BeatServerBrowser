using BeatServerBrowser.Core.Bases;
using BeatServerBrowser.Core.Interfaces;
using BeatServerBrowser.Core.Services;
using BeatServerBrowser.Core.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace BeatServerBrowser.Core
{
    public class CoreModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ILoadingService, LoadingService>();
            containerRegistry.Register<ICustomDialogService, CustomDialogService>();
            containerRegistry.RegisterDialog<ConfimationDialog>(nameof(ConfimationDialog));
        }
    }
}
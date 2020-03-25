using BeatServerBrowser.PlayList.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace BeatServerBrowser.PlayList
{
    public class PlayListModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var rm = containerProvider.Resolve<IRegionManager>();
            rm.RegisterViewWithRegion("PlaylistRegion", typeof(CreatePlaylist));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation(typeof(PlaylistMain), "PlaylistMain");
            containerRegistry.RegisterDialog<PlaylistSongs>(nameof(PlaylistSongs));
        }
    }
}
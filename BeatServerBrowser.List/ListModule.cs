using BeatServerBrowser.List.Views;
using NLog;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Diagnostics;

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
            var logger = LogManager.GetCurrentClassLogger();

            try {
                containerRegistry.RegisterForNavigation(typeof(ListMain), "ListMain");
            }
            catch (ViewRegistrationException e) {
                Debug.WriteLine(e);
                logger.Error(e);
            }
            catch (Exception e) {
                Debug.WriteLine(e);
                logger.Error(e);
            }
        }
    }
}
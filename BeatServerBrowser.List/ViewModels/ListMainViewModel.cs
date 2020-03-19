using BeatServerBrowser.Core.Models;
using BeatServerBrowser.List.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace BeatServerBrowser.List.ViewModels
{
    public class ListMainViewModel : BindableBase, INavigationAware
    {
        
        #region // 構築・破棄
        public ListMainViewModel()
        {
            
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }
        #endregion
    }
}

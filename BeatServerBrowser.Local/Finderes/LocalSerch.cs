using BeatServerBrowser.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeatServerBrowser.Local.Finderes
{
    public static class LocalSerch
    {
        public static IEnumerable<LocalBeatmapInfo> LocalSerching(int page, IList<LocalBeatmapInfo> mapList = null)
        {
            if (page < 0) {
                return null;
            }
            var list = new List<LocalBeatmapInfo>();
            page++;
            if (mapList == null) {
                for (int i = (page - 1) * 10; i < page * 10; i++) {
                    if (ConfigMaster.Current.SortedLocalBeatmaps.Count <= i) {
                        continue;
                    }
                    list.Add(ConfigMaster.Current.SortedLocalBeatmaps[i]);
                }
            }
            else {
                for (int i = (page - 1) * 10; i < page * 10; i++) {
                   if (!mapList.Any() || mapList.Count <= i) {
                        continue;
                    }
                    list.Add(mapList[i]);
                }
            }
            
            return list;
        }
    }
}

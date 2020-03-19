using BeatServerBrowser.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeatServerBrowser.Local.Finderes
{
    public static class LocalSerch
    {
        public static IEnumerable<LocalBeatmapInfo> LocalSerching(int page)
        {
            if (page < 0) {
                return null;
            }
            var list = new List<LocalBeatmapInfo>();
            page++;
            for (int i = (page - 1) * 10 ; i < page * 10; i++) {
                if (ConfigMaster.Current.LocalBeatmaps.Count < i) {
                    continue;
                }
                list.Add(ConfigMaster.Current.LocalBeatmaps[i]);
            }
            return list;
        }
    }
}

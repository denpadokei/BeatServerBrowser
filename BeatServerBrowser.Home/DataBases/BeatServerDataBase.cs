using BeatSaverSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeatServerBrowser.Home.DataBases
{
    public static class BeatServerDataBase
    {
        public static IEnumerable<Page> GetPage(IEnumerable<uint> pages)
        {
            var beatserver = new BeatSaver();
            foreach (var pagenum in pages) {
                yield return beatserver.Latest(pagenum).Result;
            }
        }
    }
}

using BeatSaverSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeatServerBrowser.List.DataBases
{
    public static class ListDataBase
    {
        public static IEnumerable<Page> GetPage(BeatSaver beatSaver, IEnumerable<uint> pages)
        {
            foreach (var pagenum in pages) {
                var page = beatSaver.Latest(pagenum).Result;
                yield return page;
            }
        }
    }
}

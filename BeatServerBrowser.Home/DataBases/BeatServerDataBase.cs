using BeatSaverSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeatServerBrowser.Home.DataBases
{
    public static class BeatServerDataBase
    {
        public static  IEnumerable<Page> GetPage(BeatSaver beatSaver, IEnumerable<uint> pages)
        {
            foreach (var pagenum in pages) {
                var page = beatSaver.Latest(pagenum).Result;
                yield return page;
            }
        }

        public static Page Serch(BeatSaver beatSaver, string quary, uint pagenum)
        {
            if (string.IsNullOrWhiteSpace(quary)) {
                return new Page();
            }
            return beatSaver.Search(quary, pagenum).Result;
        }
    }
}

using BeatSaverSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeatServerBrowser.Home.DataBases
{
    public static class BeatServerDataBase
    {
        public static Page GetPage(uint page)
        {
            var beatserver = new BeatSaver();
            return beatserver.Latest(page).Result;
        }
    }
}

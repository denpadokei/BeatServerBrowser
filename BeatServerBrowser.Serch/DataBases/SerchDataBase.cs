using BeatSaverSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeatServerBrowser.Serch.DataBases
{
    public static class SerchDataBase
    {
        public static Page Serch(BeatSaver beatSaver, string quary, uint pagenum)
        {
            return beatSaver.Search(quary, pagenum).Result;
        }
    }
}

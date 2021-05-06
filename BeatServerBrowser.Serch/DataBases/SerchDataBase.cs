using BeatSaverSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeatServerBrowser.Serch.DataBases
{
    public static class SerchDataBase
    {
        public static Task<Page<SearchRequestOptions>> Serch(BeatSaver beatSaver, string quary, uint pagenum)
        {
            var op = new SearchRequestOptions(quary);
            op.Page = pagenum;
            return beatSaver.Search(op);
        }
    }
}

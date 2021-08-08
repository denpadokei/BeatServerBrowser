using BeatSaverSharp;
using BeatSaverSharp.Models.Pages;
using System.Threading.Tasks;

namespace BeatServerBrowser.Serch.DataBases
{
    public static class SerchDataBase
    {
        public static Task<Page> Serch(BeatSaver beatSaver, string quary, uint pagenum)
        {
            var op = new SearchTextFilterOption(quary);
            return beatSaver.SearchBeatmaps(op, (int)pagenum);
        }
    }
}

using BeatSaverSharp;
using BeatSaverSharp.Models.Pages;
using BeatServerBrowser.Core.ScoreSaberSherp;
using BeatServerBrowser.Core.ScoreSaberSherp.Types;
using System;
using System.Threading.Tasks;

namespace BeatServerBrowser.List.DataBases
{
    public static class ListDataBase
    {
        public static async Task<Page> GetLatestPage(BeatSaver beatSaver, uint count)
        {
            var op = new SearchTextFilterOption()
            {
                SortOrder = SortingOptions.Latest,
            };
            var page = await beatSaver.SearchBeatmaps(op, (int)count);
            return page;
        }

        //public static async Task<Page> GetHotPage(BeatSaver beatSaver, uint count)
        //{
        //    var op = PagedRequestOptions.Default;
        //    op.Page = count;
        //    var page = await beatSaver.Hot(op);
        //    return page;
        //}

        //public static async Task<Page> GetDownloadsPage(BeatSaver beatSaver, uint count)
        //{
        //    var op = PagedRequestOptions.Default;
        //    op.Page = count;
        //    var page = await beatSaver.Downloads(op);
        //    return page;
        //}

        public static async Task<Page> GetRatingPage(BeatSaver beatSaver, uint count)
        {
            var op = new SearchTextFilterOption()
            {
                SortOrder = SortingOptions.Rating
            };
            var page = await beatSaver.SearchBeatmaps(op, (int)count);
            return page;
        }
        public static async Task<Page> GetRankPage(BeatSaver beatSaver, uint count)
        {
            var op = new SearchTextFilterOption()
            {
                Ranked = true,
                SortOrder = SortingOptions.Latest,
            };
            var page = await beatSaver.SearchBeatmaps(op, (int)count);
            return page;
        }
    }
}

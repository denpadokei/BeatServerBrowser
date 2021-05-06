using BeatSaverSharp;
using BeatServerBrowser.Core.ScoreSaberSherp;
using BeatServerBrowser.Core.ScoreSaberSherp.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeatServerBrowser.List.DataBases
{
    public static class ListDataBase
    {
        public static async Task<Page<PagedRequestOptions>> GetLatestPage(BeatSaver beatSaver, uint count)
        {
            var op = PagedRequestOptions.Default;
            op.Page = count;
            var page = await beatSaver.Latest(op);
            return page;
        }

        public static async Task<Page<PagedRequestOptions>> GetHotPage(BeatSaver beatSaver, uint count)
        {
            var op = PagedRequestOptions.Default;
            op.Page = count;
            var page = await beatSaver.Hot(op);
            return page;
        }

        public static async Task<Page<PagedRequestOptions>> GetDownloadsPage(BeatSaver beatSaver, uint count)
        {
            var op = PagedRequestOptions.Default;
            op.Page = count;
            var page = await beatSaver.Downloads(op);
            return page;
        }

        public static async Task<Page<PagedRequestOptions>> GetRatingPage(BeatSaver beatSaver, uint count)
        {
            var op = PagedRequestOptions.Default;
            op.Page = count;
            var page = await beatSaver.Rating(op);
            return page;
        }
        public static async Task<Scores> GetRankPage(ScoreSaber scoreSaber, uint count)
        {
            var op = PagedRequestOptions.Default;
            op.Page = count;
            var songs = await scoreSaber.Rank(count);
            return songs;
        }
    }
}

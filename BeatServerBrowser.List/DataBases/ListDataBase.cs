using BeatSaverSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeatServerBrowser.List.DataBases
{
    public static class ListDataBase
    {
        public static Page GetLatestPage(BeatSaver beatSaver, uint count)
        {
            var page = beatSaver.Latest(count).Result;
            return page;
        }

        public static Page GetHotPage(BeatSaver beatSaver, uint count)
        {
            var page = beatSaver.Hot(count).Result;
            return page;
        }

        public static Page GetDownloadsPage(BeatSaver beatSaver, uint count)
        {
            var page = beatSaver.Downloads(count).Result;
            return page;
        }

        public static Page GetRatingPage(BeatSaver beatSaver, uint count)
        {
            var page = beatSaver.Rating(count).Result;
            return page;
        }

        public static Page GetPlaysPage(BeatSaver beatSaver, uint count)
        {
            var page = beatSaver.Plays(count).Result;
            return page;
        }
    }
}

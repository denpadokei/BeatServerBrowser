using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BeatServerBrowser.Static.Enums
{
    public enum PageType
    {
        [Description("最新")]
        Latest,
        [Description("HOT！")]
        Hot,
        [Description("Upvoteレーティング")]
        Raiting,
        [Description("ダウンロード数")]
        Downloads,
        [Description("プレイカウント数")]
        Plays,
        [Description("ランク")]
        Rank
    }

    public enum Sigletype
    {
        [Description("detail")]
        Key,
        [Description("by-hash")]
        Hash
    }

    public enum SerchType
    {
        [Description("text")]
        Text,
        [Description("advanced")]
        Advanced
    }
    public enum ListType
    {
        [Description("グリッド形式")]
        Grid,
        [Description("パネル形式")]
        Panel
    }
}

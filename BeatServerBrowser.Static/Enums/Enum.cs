using System;
using System.ComponentModel;

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

    [Flags]
    public enum PlaySoundFlags : int
    {
        SND_SYNC = 0x0000,
        SND_ASYNC = 0x0001,
        SND_NODEFAULT = 0x0002,
        SND_MEMORY = 0x0004,
        SND_LOOP = 0x0008,
        SND_NOSTOP = 0x0010,
        SND_NOWAIT = 0x00002000,
        SND_FILENAME = 0x00020000,
        SND_RESOURCE = 0x00040004
    }

}

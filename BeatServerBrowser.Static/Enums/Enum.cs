﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BeatServerBrowser.Static.Enums
{
    enum SerchType
    {
        [Description("HOT！")]
        Hot,
        [Description("Upvoteレーティング")]
        Raiting,
        [Description("最新")]
        Latest,
        [Description("ダウンロード数")]
        Downloads,
        [Description("プレイカウント数")]
        Plays,
    }
}

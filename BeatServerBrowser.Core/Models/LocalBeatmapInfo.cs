using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace BeatServerBrowser.Core.Models
{
    public class LocalBeatmapInfo : IEquatable<LocalBeatmapInfo>
    {
        [JsonProperty("_songName")]
        public string SongTitle { get; set; }
        [JsonProperty("_songSubName")]
        public string SongSubTitle { get; set; }
        [JsonProperty("_levelAuthorName")]
        public string LevelAuthorName { get; set; }

        public override bool Equals(object obj) => this.Equals(obj as LocalBeatmapInfo);

        public bool Equals(LocalBeatmapInfo b)
        {
            if (b is null) return false;
            if (ReferenceEquals(this, b)) return true;
            if (this.GetType() != b.GetType()) return false;

            return (SongTitle == b.SongTitle) && (SongSubTitle == b.SongSubTitle) && (LevelAuthorName == b.LevelAuthorName);
        }
    }
}

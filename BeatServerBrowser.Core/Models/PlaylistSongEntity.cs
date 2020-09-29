using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeatServerBrowser.Core.Models
{
    public class PlaylistSongEntity
    {
        [JsonProperty("key")]
        public string Key { get; set; } = "";

        [JsonProperty("songName")]
        public string SongName { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }
    }
}

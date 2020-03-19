using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeatServerBrowser.Core.Models
{
    public class LocalSongJsonEntity
    {
        /// <summary>曲タイトル を取得、設定</summary>
        [JsonProperty("_songName")]
        public string SongName { get; set; }
        /// <summary>曲のサブタイトル を取得、設定</summary>
        [JsonProperty("_songSubName")]
        public string SongSubName { get; set; }
        /// <summary>曲のサブタイトル を取得、設定</summary>
        [JsonProperty("_songAuthorName")]
        public string SongAuthorName { get; set; }
        /// <summary>マッパー を取得、設定</summary>
        [JsonProperty("_levelAuthorName")]
        public string LevelAuthorName { get; set; }
        /// <summary>曲ファイル名</summary>
        [JsonProperty("_songFilename")]
        public string SongFileName { get; set; }
        /// <summary>カバー画像ソース を取得、設定</summary>
        [JsonProperty("_coverImageFilename")]
        public string CoverImageFilename { get; set; }
    }
}

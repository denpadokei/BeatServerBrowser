using Newtonsoft.Json;
using NLog;
using Prism.Commands;
using Prism.Mvvm;
using SongCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace BeatServerBrowser.Core.Models
{
    public class LocalBeatmapInfo : BindableBase, IEquatable<LocalBeatmapInfo>
    {
        [JsonIgnore]
        private Logger Logger => LogManager.GetCurrentClassLogger();

        
        /// <summary>説明 を取得、設定</summary>
        private string songTitle_;
        /// <summary>説明 を取得、設定</summary>
        [JsonProperty("_songName")]
        public string SongTitle
        {
            get => this.songTitle_;

            set => this.SetProperty(ref this.songTitle_, value);
        }

        /// <summary>曲のサブタイトル を取得、設定</summary>
        private string songSubTitle_;
        /// <summary>曲のサブタイトル を取得、設定</summary>
        [JsonProperty("_songSubName")]
        public string SongSubTitle
        {
            get => this.songSubTitle_;

            set => this.SetProperty(ref this.songSubTitle_, value);
        }

        /// <summary>マッパー を取得、設定</summary>
        private string levelAuthorName_;
        /// <summary>マッパー を取得、設定</summary>
        [JsonProperty("_levelAuthorName")]
        public string LevelAuthorName
        {
            get => this.levelAuthorName_;

            set => this.SetProperty(ref this.levelAuthorName_, value);
        }

        /// <summary>カバー画像URI を取得、設定</summary>
        private Uri cover_;
        /// <summary>カバー画像URI を取得、設定</summary>
        public Uri CoverUri
        {
            get => this.cover_;

            set => this.SetProperty(ref this.cover_, value);
        }

        [JsonIgnore]
        public DelegateCommand CreateCommand { get; set; }

        [JsonIgnore]
        public DirectoryInfo Directory { get; set; }

        public override bool Equals(object obj) => this.Equals(obj as LocalBeatmapInfo);

        public bool Equals(LocalBeatmapInfo b)
        {
            if (b is null) return false;
            if (ReferenceEquals(this, b)) return true;
            if (this.GetType() != b.GetType()) return false;

            return (this.SongTitle == b.SongTitle) && (this.SongSubTitle == b.SongSubTitle) && (this.LevelAuthorName == b.LevelAuthorName);
        }

        private void Create()
        {
            if (this.Directory == null) {
                return;
            }

            foreach (var file in this.Directory.EnumerateFiles("info.dat", SearchOption.TopDirectoryOnly)) {
                Debug.WriteLine($"ファイルを読み込みます。[{file.Name}][{file.FullName}]");
                this.Logger.Info($"ファイルを読み込みます。[{file.Name}][{file.FullName}]");

                var text = File.ReadAllText(file.FullName);
                var beatmap = JsonConvert.DeserializeObject<LocalBeatmapInfo>(text);
                Debug.WriteLine($"{beatmap.SongTitle}を読み込みました。");
                this.Logger.Info($"{beatmap.SongTitle}を読み込みました。");
                this.SongTitle = beatmap.SongTitle;
                this.SongSubTitle = beatmap.SongSubTitle;
                this.LevelAuthorName = beatmap.LevelAuthorName;
            }
            var uristring = this.Directory.EnumerateFiles("*.jpg", SearchOption.TopDirectoryOnly).FirstOrDefault()?.FullName;
            if (uristring != null) {
                this.CoverUri = new Uri(uristring);
            }
        }

        public LocalBeatmapInfo(DirectoryInfo directory)
        {
            this.Directory = directory;
            this.CreateCommand = new DelegateCommand(this.Create);
        }

        public LocalBeatmapInfo()
        {

        }
    }
}

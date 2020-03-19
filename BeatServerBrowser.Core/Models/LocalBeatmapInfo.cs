using Newtonsoft.Json;
using NLog;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;

namespace BeatServerBrowser.Core.Models
{
    public class LocalBeatmapInfo : BindableBase, IEquatable<LocalBeatmapInfo>
    {
        private Logger Logger => LogManager.GetCurrentClassLogger();

        
        /// <summary>説明 を取得、設定</summary>
        private string songTitle_;
        /// <summary>説明 を取得、設定</summary>
        public string SongTitle
        {
            get => this.songTitle_;

            set => this.SetProperty(ref this.songTitle_, value);
        }

        /// <summary>マッパー を取得、設定</summary>
        private string levelAuthorName_;
        /// <summary>マッパー を取得、設定</summary>
        public string LevelAuthorName
        {
            get => this.levelAuthorName_;

            set => this.SetProperty(ref this.levelAuthorName_, value);
        }

        /// <summary>カバー画像ソース を取得、設定</summary>
        private string coverSource_;
        /// <summary>カバー画像ソース を取得、設定</summary>
        public string CoverFileName
        {
            get => this.coverSource_;

            set => this.SetProperty(ref this.coverSource_, value);
        }

        /// <summary>カバー画像URI を取得、設定</summary>
        private Uri cover_;
        /// <summary>カバー画像URI を取得、設定</summary>
        public Uri CoverUri
        {
            get => this.cover_;

            set => this.SetProperty(ref this.cover_, value);
        }

        
        public DelegateCommand CreateCommand { get; set; }

        
        public DirectoryInfo Directory { get; set; }

        public override bool Equals(object obj) => this.Equals(obj as LocalBeatmapInfo);

        public bool Equals(LocalBeatmapInfo b)
        {
            if (b is null) return false;
            if (ReferenceEquals(this, b)) return true;
            if (this.GetType() != b.GetType()) return false;

            return (this.SongTitle == b.SongTitle) && (this.LevelAuthorName == b.LevelAuthorName);
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
                var beatmap = JsonConvert.DeserializeObject<LocalSongJsonEntity>(text);
                Debug.WriteLine($"{beatmap.SongName}を読み込みました。");
                this.Logger.Info($"{beatmap.SongName}を読み込みました。");
                this.SongTitle = $"{beatmap.SongName} - {beatmap.SongSubName}";
                this.CoverFileName = beatmap.CoverImageFilename;
                this.LevelAuthorName = beatmap.LevelAuthorName;
            }
            var uristring = this.Directory.EnumerateFileSystemInfos($@"{this.CoverFileName}", SearchOption.TopDirectoryOnly).FirstOrDefault()?.FullName;
            if (uristring != null && !string.IsNullOrWhiteSpace(uristring)) {
                this.CoverFileName = uristring;
                this.CoverUri = new Uri(uristring);
            }
            else {
                var local = new DirectoryInfo(@".\");
                try {
                    this.CoverUri = new Uri($@"{local.FullName}\Images\default.jpg");
                }
                catch (Exception e) {
                    this.Logger.Error(e);
                }
            }
        }

        public LocalBeatmapInfo(DirectoryInfo directory)
        {
            this.Directory = directory;
            this.CreateCommand = new DelegateCommand(this.Create);
            this.Create();
        }

        public LocalBeatmapInfo()
        {

        }
    }
}

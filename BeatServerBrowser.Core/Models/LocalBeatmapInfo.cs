using BeatServerBrowser.Core.Services;
using NAudio.Vorbis;
using NAudio.Wave;
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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace BeatServerBrowser.Core.Models
{
    public class LocalBeatmapInfo : BindableBase, IEquatable<LocalBeatmapInfo>, IDisposable
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
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
            get => this.cover_ ?? new Uri($@"{ConfigMaster.ThisDirectoryPath}\Images\default.jpg");

            set => this.SetProperty(ref this.cover_, value);
        }

        /// <summary>ディレクトリ名 を取得、設定</summary>
        private string directoryPath_;
        /// <summary>ディレクトリ名 を取得、設定</summary>
        public string DirectoryPath
        {
            get => this.directoryPath_;

            set => this.SetProperty(ref this.directoryPath_, value);
        }

        /// <summary>曲のハッシュ値 を取得、設定</summary>
        private string songHash_;
        /// <summary>曲のハッシュ値 を取得、設定</summary>
        public string SongHash
        {
            get => this.songHash_;

            set => this.SetProperty(ref this.songHash_, value);
        }

        /// <summary>再生中かどうか を取得、設定</summary>
        private bool isPreview_;
        /// <summary>再生中かどうか を取得、設定</summary>
        public bool IsPreView
        {
            get => this.isPreview_;

            set => this.SetProperty(ref this.isPreview_, value);
        }

        /// <summary>再生ボタンの名前 を取得、設定</summary>
        private string playContent_;
        /// <summary>再生ボタンの名前 を取得、設定</summary>
        public string PlayContent
        {
            get => this.playContent_;

            set => this.SetProperty(ref this.playContent_, value);
        }

        /// <summary>再生サービス を取得、設定</summary>
        private SoundPlayerService player_;
        /// <summary>再生サービス を取得、設定</summary>
        public SoundPlayerService Player
        {
            get => this.player_;

            set => this.SetProperty(ref this.player_, value);
        }

        public DirectoryInfo Directory { get; set; }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド
        public DelegateCommand CreateCommand { get; set; }

        /// <summary>キーコピーコマンド を取得、設定</summary>
        private DelegateCommand CopyCommand_;
        /// <summary>キーコピーコマンド を取得、設定</summary>
        public DelegateCommand CopyCommand => this.CopyCommand_ ?? (this.CopyCommand_ = new DelegateCommand(this.KeyCopy));


        /// <summary>プレビューコマンド を取得、設定</summary>
        private DelegateCommand preViewCommand_;
        /// <summary>プレビューコマンド を取得、設定</summary>
        public DelegateCommand PreViewCommand => this.preViewCommand_ ?? (this.preViewCommand_ = new DelegateCommand(this.PreView));

        /// <summary>曲削除コマンド を取得、設定</summary>
        private DelegateCommand deleteCommand_;
        /// <summary>曲削除コマンド を取得、設定</summary>
        public DelegateCommand DeleteCommand => this.deleteCommand_ ?? (this.deleteCommand_ = new DelegateCommand(this.Delete));
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド用メソッド
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
                this.SongTitle = $"{beatmap.SongName} - {beatmap.SongAuthorName}";
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
            this.DirectoryPath = this.Directory.Name;
            this.SongHash = SongHashDataProviderService.GenerateHash(this.Directory.FullName, this.SongHash);
        }

        private async void KeyCopy()
        {
            var beatmap = await ConfigMaster.Current.CurrentBeatSaver.Hash(this.SongHash);
            if (beatmap == null) {
                return;
            }
            Clipboard.SetText($"!bsr {beatmap.Key}");
            this.CopyKey?.Invoke();
            this.Logger.Info($"{beatmap.Key}をクリップボードに送りました。");
            Debug.WriteLine($"{beatmap.Key}をクリップボードに送りました。");
        }

        private void PreView()
        {
            var soundFileInfo = this.Directory.EnumerateFiles("*.egg", SearchOption.TopDirectoryOnly).FirstOrDefault();
            this.Player.Stop();
            this.Player.Play(soundFileInfo, this);
        }

        private void Delete()
        {
            this.DeleteSong?.Invoke();
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックイベント
        public Action CopyKey;

        public Action DeleteSong;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        public override bool Equals(object obj) => this.Equals(obj as LocalBeatmapInfo);
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        public bool Equals(LocalBeatmapInfo b)
        {
            if (b is null) return false;
            if (ReferenceEquals(this, b)) return true;
            if (this.GetType() != b.GetType()) return false;

            return (this.SongHash.ToUpper() == b.SongHash.ToUpper());
        }

        public string GetKey()
        {
            var beatmap = ConfigMaster.Current.CurrentBeatSaver.Hash(this.SongHash).Result;
            return beatmap.Key;
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        public LocalBeatmapInfo(DirectoryInfo directory)
        {
            this.Player = SoundPlayerService.CurrentPlayer;
            this.Directory = directory;
            this.CreateCommand = new DelegateCommand(this.Create);
            this.IsPreView = false;
            this.PlayContent = "再生";
            this.Create();
        }

        public LocalBeatmapInfo()
        {
            this.Player = SoundPlayerService.CurrentPlayer;
            this.CreateCommand = new DelegateCommand(this.Create);
            this.IsPreView = false;
            this.PlayContent = "再生";
        }

        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: マネージ状態を破棄します (マネージ オブジェクト)。
                    this.Player.Dispose();
                }

                // TODO: アンマネージ リソース (アンマネージ オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
                // TODO: 大きなフィールドを null に設定します。

                disposedValue = true;
            }
        }

        // TODO: 上の Dispose(bool disposing) にアンマネージ リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
        // ~LocalBeatmapInfo()
        // {
        //   // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
        //   Dispose(false);
        // }

        // このコードは、破棄可能なパターンを正しく実装できるように追加されました。
        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose(true);
            // TODO: 上のファイナライザーがオーバーライドされる場合は、次の行のコメントを解除してください。
            // GC.SuppressFinalize(this);
        }
        #endregion
        #endregion
    }
}

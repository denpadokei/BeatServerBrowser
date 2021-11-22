using BeatServerBrowser.Core.Interfaces;
using BeatServerBrowser.Core.Services;
using Microsoft.Toolkit.Uwp.Notifications;
using Newtonsoft.Json;
using NLog;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BeatServerBrowser.Core.Models
{
    public class LocalBeatmapInfo : BindableBase, IEquatable<LocalBeatmapInfo>, IIndexable
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

        /// <summary>曲のソート順 を取得、設定</summary>
        private int index__;
        /// <summary>曲のソート順 を取得、設定</summary>
        public int Index
        {
            get => this.index__;

            set => this.SetProperty(ref this.index__, value);
        }

        /// <summary>ローカルで作成されたものかどうか を取得、設定</summary>
        private bool isLocal_;
        /// <summary>ローカルで作成されたものかどうか を取得、設定</summary>
        public bool IsLocal
        {
            get => this.isLocal_;

            set => this.SetProperty(ref this.isLocal_, value);
        }

        public DirectoryInfo Directory { get; set; }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド
        public DelegateCommand CreateCommand { get; set; }

        /// <summary>キーコピーコマンド を取得、設定</summary>
        private DelegateCommand CopyCommand_;
        /// <summary>キーコピーコマンド を取得、設定</summary>
        public DelegateCommand CopyCommand => this.CopyCommand_ ?? (this.CopyCommand_ = new DelegateCommand(() => this.KeyCopy().Await()));


        /// <summary>プレビューコマンド を取得、設定</summary>
        private DelegateCommand preViewCommand_;
        /// <summary>プレビューコマンド を取得、設定</summary>
        public DelegateCommand PreViewCommand => this.preViewCommand_ ?? (this.preViewCommand_ = new DelegateCommand(this.PreView));

        /// <summary>曲削除コマンド を取得、設定</summary>
        private DelegateCommand deleteCommand_;
        /// <summary>曲削除コマンド を取得、設定</summary>
        public DelegateCommand DeleteCommand => this.deleteCommand_ ?? (this.deleteCommand_ = new DelegateCommand(this.Delete));

        /// <summary>曲詳細コマンド を取得、設定</summary>
        private DelegateCommand showDetailCommand_;
        /// <summary>曲詳細コマンド を取得、設定</summary>
        public DelegateCommand ShowDetailCommand => this.showDetailCommand_ ?? (this.showDetailCommand_ = new DelegateCommand(async () => await this.ShowDetail()));

        /// <summary>曲詳細コマンド を取得、設定</summary>
        private DelegateCommand showDetailCommandOnPlaylistView_;
        /// <summary>曲詳細コマンド を取得、設定</summary>
        public DelegateCommand ShowDeailCommandOnPlaylistView => this.showDetailCommandOnPlaylistView_ ?? (this.showDetailCommandOnPlaylistView_ = new DelegateCommand(async () => await this.ShowDetailOnPlaylistView()));


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

        private async Task KeyCopy()
        {
            try {
                var beatmap = await ConfigMaster.Current.CurrentBeatSaver.BeatmapByHash(this.SongHash);
                if (beatmap == null) {
                    new ToastContentBuilder().AddText($"{this.SongTitle}のキーのコピーに失敗しました。").Show();
                    return;
                }
                Clipboard.SetText($"!bsr {beatmap.ID}");
                new ToastContentBuilder().AddText($"「!bsr {beatmap.ID}」をクリップボードに送りました。").Show();
                this.CopyKey?.Invoke();
                this.Logger.Info($"{beatmap.ID}をクリップボードに送りました。");
                Debug.WriteLine($"{beatmap.ID}をクリップボードに送りました。");
            }
            catch (Exception e) {
                new ToastContentBuilder().AddText($"{this.SongTitle}のキーのコピーに失敗しました。").Show();
                Debug.WriteLine(e);
            }
        }

        private void PreView()
        {
            this.Player.Stop();
            var soundFileInfo = this.Directory.EnumerateFiles("*.egg", SearchOption.TopDirectoryOnly).FirstOrDefault();
            if (this.IsLocal) {
                var playlist = ConfigMaster.Current.SortedLocalBeatmaps.ToList();
                playlist.Remove(this);
                playlist.Insert(0, this);
                this.Player.Play(soundFileInfo, this, playlist);
            }
            else {
                this.Player.Play(soundFileInfo, this);
            }

        }

        private void Delete()
        {
            this.DeleteSongAction?.Invoke(this);
        }

        private async Task ShowDetail()
        {
            if (string.IsNullOrWhiteSpace(this.SongHash)) {
                return;
            }
            var beatmap = await ConfigMaster.Current.CurrentBeatSaver.BeatmapByHash(this.SongHash);
            if (beatmap == null) {
                return;
            }
            SongManager.CurrentSongManager.ShowDeailCommand?.Execute(new BeatmapEntity(beatmap));
            //this.ShowDetailAction?.Invoke(new BeatmapEntity(beatmap));
        }


        private async Task ShowDetailOnPlaylistView()
        {
            if (string.IsNullOrWhiteSpace(this.SongHash)) {
                return;
            }
            var beatmap = await ConfigMaster.Current.CurrentBeatSaver.BeatmapByHash(this.SongHash);
            if (beatmap == null) {
                return;
            }
            SongManager.CurrentSongManager.ShowDeailCommandOnPlaylistView?.Execute(new BeatmapEntity(beatmap));
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックイベント
        public Action CopyKey;

        public Action<LocalBeatmapInfo> DeleteSongAction;

        public Action<BeatmapEntity> ShowDetailAction;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        public override bool Equals(object obj)
        {
            return this.Equals(obj as LocalBeatmapInfo);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        public bool Equals(LocalBeatmapInfo b)
        {
            if (b is null)
                return false;
            if (ReferenceEquals(this, b))
                return true;
            if (this.GetType() != b.GetType())
                return false;

            return (this.SongHash?.ToUpper() == b.SongHash?.ToUpper());
        }

        public async Task<string> GetKey()
        {
            var beatmap = await ConfigMaster.Current.CurrentBeatSaver.BeatmapByHash(this.SongHash);
            return beatmap == null ? "" : beatmap.ID;
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        public LocalBeatmapInfo(DirectoryInfo directory, bool isLocal)
        {
            this.Player = SoundPlayerService.CurrentPlayer;
            this.Directory = directory;
            this.CreateCommand = new DelegateCommand(this.Create);
            this.IsPreView = false;
            this.PlayContent = "再生";
            this.IsLocal = isLocal;
            this.Create();
        }

        public LocalBeatmapInfo()
        {
            this.Player = SoundPlayerService.CurrentPlayer;
            this.CreateCommand = new DelegateCommand(this.Create);
            this.IsPreView = false;
            this.IsLocal = false;
            this.PlayContent = "再生";
        }
        #endregion
    }
}

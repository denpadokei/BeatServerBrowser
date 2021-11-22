using BeatSaverSharp.Models;
using BeatServerBrowser.Core.Services;
using Microsoft.Toolkit.Uwp.Notifications;
using NLog;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BeatServerBrowser.Core.Models
{
    public class BeatmapEntity : BindableBase
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        private Logger Logger => LogManager.GetCurrentClassLogger();

        /// <summary>譜面 を取得、設定</summary>
        private Beatmap beatmap_;
        /// <summary>譜面 を取得、設定</summary>
        public Beatmap Beatmap
        {
            get => this.beatmap_;

            set => this.SetProperty(ref this.beatmap_, value);
        }

        /// <summary>カバーURI を取得、設定</summary>
        private Uri coverUri_;
        /// <summary>カバーURI を取得、設定</summary>
        public Uri CoverUri
        {
            get => this.coverUri_;

            set => this.SetProperty(ref this.coverUri_, value);
        }

        /// <summary>カバー画像（byte[]） を取得、設定</summary>
        private byte[] coverbuff_;
        /// <summary>カバー画像（byte[]） を取得、設定</summary>
        public byte[] CoverBuff
        {
            get => this.coverbuff_;

            set => this.SetProperty(ref this.coverbuff_, value);
        }


        public string SongTitle => this.Beatmap.Name;

        public string UploaderName => this.Beatmap.Uploader.Name;

        public string Hash => this.Beatmap.LatestVersion.Hash;

        public string CoverURL => this.Beatmap.LatestVersion.CoverURL;

        public string DownloadURL => this.Beatmap.LatestVersion.DownloadURL;

        public string DirectDownload => this.Beatmap.LatestVersion.DownloadURL;

        public BeatmapVersion Version => this.Beatmap.LatestVersion;

        public BeatmapVersion.VersionState Stats => this.Beatmap.LatestVersion.State;

        public BeatmapMetadata Metadata => this.Beatmap.Metadata;

        public DateTime Uploaded => this.Beatmap.Uploaded;

        public User Uploader => this.Beatmap.Uploader;

        public string Description => this.Beatmap.Description;

        public string Name => this.Beatmap.Name;

        public string Key => this.Beatmap.ID;

        public string ID => this.Beatmap.ID;

        public string CoverFilename => this.Beatmap.LatestVersion.CoverURL;

        public int Upvotes => this.Beatmap.Stats.Upvotes;

        public int Downvotes => this.Beatmap.Stats.Downvotes;

        public int Downloads => this.Beatmap.Stats.Downloads;

        //public bool Partial => this.Beatmap.Partial;

        /// <summary>インストール済みかどうか を取得、設定</summary>
        private bool isInstalled_;
        /// <summary>インストール済みかどうか を取得、設定</summary>
        public bool IsInstalled
        {
            get => this.isInstalled_;

            set => this.SetProperty(ref this.isInstalled_, value);
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド
        /// <summary>ダウンロードコマンド を取得、設定</summary>
        private DelegateCommand downloadCommand_;
        /// <summary>ダウンロードコマンド を取得、設定</summary>
        public DelegateCommand DownloadCommand => this.downloadCommand_ ?? (this.downloadCommand_ = new DelegateCommand(this.Download, this.CanInstalled)).ObservesProperty(() => this.IsInstalled);

        /// <summary>コピー を取得、設定</summary>
        private DelegateCommand copyCommand_;
        /// <summary>コピー を取得、設定</summary>
        public DelegateCommand CopyCommand => this.copyCommand_ ?? (this.copyCommand_ = new DelegateCommand(async () => await this.Copy()));

        /// <summary>詳細表示 を取得、設定</summary>
        private DelegateCommand showDetailCommand_;
        /// <summary>詳細表示 を取得、設定</summary>
        public DelegateCommand ShowDetailCommand => this.showDetailCommand_ ?? (this.showDetailCommand_ = new DelegateCommand(this.ShowDetail));

        /// <summary>プレビューコマンド を取得、設定</summary>
        private DelegateCommand previewCommand_;
        /// <summary>プレビューコマンド を取得、設定</summary>
        public DelegateCommand PreviewCommand => this.previewCommand_ ?? (this.previewCommand_ = new DelegateCommand(this.Preview));
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド用メソッド
        private async void Download()
        {
            try {
                if (!Directory.Exists(@$"{ConfigMaster.Current.InstallFolder}\Beat Saber_Data\CustomLevels")) {
                    Debug.WriteLine("インストールフォルダが正しくありません。");
                    this.Logger.Info("インストールフォルダが正しくありません。");
                    return;
                }
                var path = $@"{ConfigMaster.Current.InstallFolder}\Beat Saber_Data\CustomLevels\{this.Key} ({this.Name})";
                if (Directory.Exists(path)) {
                    Debug.WriteLine("取得済みです。");
                    this.Logger.Info("取得済みです。");
                    return;
                }
                var directoryInfo = Directory.CreateDirectory(path);
                var body = await Application.Current.Dispatcher.Invoke(() => this.Beatmap.LatestVersion.DownloadZIP());
                using (var stream = new MemoryStream(body)) {
                    using (var aricive = new ZipArchive(stream)) {
                        foreach (var entry in aricive.Entries) {
                            entry.ExtractToFile(Path.Combine(path, entry.FullName), false);
                            this.Logger.Info($"{entry.Name}を展開しました。");
                            Debug.WriteLine($"{entry.Name}を展開しました。");
                        }
                    }
                }
                var newBeatmap = new LocalBeatmapInfo(directoryInfo, true);
                newBeatmap.CreateCommand.Execute();
                ConfigMaster.Current.LocalBeatmaps.Add(newBeatmap);
                this.IsInstalled = true;
            }
            catch (Exception e) {
                Debug.WriteLine(e);
                this.Logger.Error(e);
            }
        }

        private async Task Copy()
        {
            for (int i = 0; i < 10; i++) {
                try {
                    Clipboard.Clear();
                    await Task.Delay(500);
                    Clipboard.SetText($"!bsr {this.Key}");
                    new ToastContentBuilder().AddText($"「!bsr {this.Key}」をクリップボードに送りました。").Show();
                    this.CopyKey?.Invoke();
                    this.Logger.Info($"{this.Key}をクリップボードに送りました。");
                    Debug.WriteLine($"{this.Key}をクリップボードに送りました。");
                    return;
                }
                catch (Exception e) {
                    this.Logger.Error(e);
                }
                finally {
                    await Task.Delay(500);
                }
            }
            new ToastContentBuilder().AddText($"{this.SongTitle}のキーのコピーに失敗しました。").Show();
        }

        private bool CanInstalled()
        {
            this.IsInstalled = ConfigMaster.Current.LocalBeatmaps.Any(x => x.SongHash == this.Hash.ToUpper());

            return !this.IsInstalled;
        }

        private void ShowDetail()
        {
            SongManager.CurrentSongManager.ShowDeailCommand?.Execute(this);//this.ShowDetailAction?.Invoke(this);
        }

        private async void Preview()
        {
            try {
                if (SoundPlayerService.CurrentPlayer.IsPreview) {
                    SoundPlayerService.CurrentPlayer.Stop();
                }
                var body = await this.Beatmap.LatestVersion.DownloadZIP();
                var path = ConfigMaster.TempralyDirectory;
                Directory.CreateDirectory(Path.Combine(path, this.Hash));
                using (var stream = new MemoryStream(body)) {
                    using (var aricive = new ZipArchive(stream)) {
                        foreach (var entry in aricive.Entries) {
                            entry.ExtractToFile(Path.Combine(path, this.Hash, entry.FullName), true);
                            this.Logger.Info($"{entry.Name}を展開しました。");
                            Debug.WriteLine($"{entry.Name}を展開しました。");
                        }
                        var info = new DirectoryInfo(Path.Combine(path, this.Hash));
                        var localmap = new LocalBeatmapInfo(info, false);
                        localmap.PreViewCommand?.Execute();
                    }
                }
            }
            catch (Exception e) {
                Debug.WriteLine(e);
            }
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックイベント
        public Action CopyKey;

        public Action<BeatmapEntity> ShowDetailAction;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        private readonly object lockObject_ = new object();
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        public BeatmapEntity(Beatmap beatmap)
        {
            this.Beatmap = beatmap;
            this.CoverUri = new Uri(this.Beatmap?.LatestVersion?.CoverURL);
            this.Beatmap?.LatestVersion.DownloadCoverImage().Await(r =>
            {
                this.CoverBuff = r;
            }, e => { });
        }
        #endregion
    }
}

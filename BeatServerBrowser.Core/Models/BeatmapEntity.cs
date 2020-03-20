using BeatSaverSharp;
using NLog;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
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


        public string SongTitle => this.Beatmap.Name;

        public string UploaderName => this.Beatmap.Uploader.Username;

        public string Hash => this.Beatmap.Hash;

        public string CoverURL => this.Beatmap.CoverURL;

        public string DownloadURL => this.Beatmap.DownloadURL;

        public string DirectDownload => this.Beatmap.DirectDownload;

        public Stats Stats => this.Beatmap.Stats;

        public Metadata Metadata => this.Beatmap.Metadata;

        public DateTime Uploaded => this.Beatmap.Uploaded;

        public User Uploader => this.Beatmap.Uploader;

        public string Description => this.Beatmap.Description;

        public string Name => this.Beatmap.Name;

        public string Key => this.Beatmap.Key;

        public string ID => this.Beatmap.ID;

        public string CoverFilename => this.Beatmap.CoverFilename;

        public bool Partial => this.Beatmap.Partial;

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
        public DelegateCommand CopyCommand => this.copyCommand_ ?? (this.copyCommand_ = new DelegateCommand(this.Copy));
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
                var body = await Application.Current.Dispatcher.Invoke(() => this.Beatmap.DownloadZip());
                using (var stream = new MemoryStream(body)) {
                    using (var aricive = new ZipArchive(stream)) {
                        foreach (var entry in aricive.Entries) {
                            entry.ExtractToFile(Path.Combine(path, entry.FullName), false);
                            this.Logger.Info($"{entry.Name}を展開しました。");
                            Debug.WriteLine($"{entry.Name}を展開しました。");
                        }
                    }
                }
                var newBeatmap = new LocalBeatmapInfo(directoryInfo);
                newBeatmap.CreateCommand.Execute();
                ConfigMaster.Current.LocalBeatmaps.Add(newBeatmap);
                this.IsInstalled = true;
            }
            catch (Exception e) {
                Debug.WriteLine(e);
                this.Logger.Error(e);
            }
            //ConfigMaster.Current.CreateLocalBeatmaps();
        }

        private void Copy()
        {
            Clipboard.SetText($"!bsr{this.Key}");
            this.CopyKey?.Invoke();
            this.Logger.Info($"{this.Key}をクリップボードに送りました。");
            Debug.WriteLine($"{this.Key}をクリップボードに送りました。");
        }

        private bool CanInstalled()
        {
            this.IsInstalled = ConfigMaster.Current.LocalBeatmaps.Any(x => x.SongHash == this.Hash.ToUpper());

            return !this.IsInstalled;
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックイベント
        public Action CopyKey;
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
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        public BeatmapEntity(Beatmap beatmap)
        {
            this.Beatmap = beatmap;
            this.CoverUri = new Uri(BeatSaver.BaseURL + $"{this.Beatmap.CoverURL}");
        }
        #endregion
    }
}

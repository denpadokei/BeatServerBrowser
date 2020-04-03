using BeatSaverSharp;
using BeatServerBrowser.Core.Collections;
using BeatServerBrowser.Core.Extentions;
using BeatServerBrowser.Core.ScoreSaberSherp;
using NLog;
using Prism.Mvvm;
using StatefulModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatServerBrowser.Core.Models
{
    public class ConfigMaster : BindableBase
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        private Logger Logger => LogManager.GetCurrentClassLogger();

        public static readonly string ThisDirectoryPath = new DirectoryInfo(@".\").FullName;

        public static readonly string TempralyDirectory = Path.Combine(Path.GetTempPath(), "BSBTemp");

        /// <summary>ダークモードフラグ を取得、設定</summary>
        private bool isDark_;
        /// <summary>ダークモードフラグ を取得、設定</summary>
        public bool IsDark
        {
            get => this.isDark_;

            set => this.SetProperty(ref this.isDark_, value);
        }

        /// <summary>インストールフォルダのパス を取得、設定</summary>
        private string installFolder__;
        /// <summary>インストールフォルダのパス を取得、設定</summary>
        public string InstallFolder
        {
            get => this.installFolder__;

            set => this.SetProperty(ref this.installFolder__, value);
        }

        /// <summary>アクセス用のBeatServer を取得、設定</summary>
        private BeatSaver currentBeatSarver_;
        /// <summary>アクセス用のBeatServer を取得、設定</summary>
        public BeatSaver CurrentBeatSaver
        {
            get => this.currentBeatSarver_;

            set => this.SetProperty(ref this.currentBeatSarver_, value);
        }

        /// <summary>アクセス用スコアセイバー を取得、設定</summary>
        private ScoreSaber currentScoreSaber_;
        /// <summary>アクセス用スコアセイバー を取得、設定</summary>
        public ScoreSaber CurrentScoreSaber
        {
            get => this.currentScoreSaber_;

            set => this.SetProperty(ref this.currentScoreSaber_, value);
        }

        /// <summary>ローカルライブラリリスト を取得、設定</summary>
        private ObservableSynchronizedCollection<LocalBeatmapInfo> localBeatmaps_;
        /// <summary>ローカルライブラリリスト を取得、設定</summary>
        public ObservableSynchronizedCollection<LocalBeatmapInfo> LocalBeatmaps
        {
            get => this.localBeatmaps_;

            set => this.SetProperty(ref this.localBeatmaps_, value);
        }

        /// <summary>ソートしたローカルライブラリ を取得、設定</summary>
        private SortedObservableCollection<LocalBeatmapInfo, string> sortedLocalBeatmaps_;
        /// <summary>ソートしたローカルライブラリ を取得、設定</summary>
        public SortedObservableCollection<LocalBeatmapInfo, string> SortedLocalBeatmaps
        {
            get => this.sortedLocalBeatmaps_;

            set => this.SetProperty(ref this.sortedLocalBeatmaps_, value);
        }

        /// <summary>ロード中かどうか を取得、設定</summary>
        private bool isLoading_;
        /// <summary>ロード中かどうか を取得、設定</summary>
        public bool IsLoading
        {
            get => this.isLoading_;

            set => this.SetProperty(ref this.isLoading_, value);
        }

        /// <summary>ボリューム を取得、設定</summary>
        private double volume_;
        /// <summary>ボリューム を取得、設定</summary>
        public double Volume
        {
            get => this.volume_;

            set => this.SetProperty(ref this.volume_, value);
        }

        /// <summary>ｼｭの有効かどうか を取得、設定</summary>
        private bool isEnambleSh_;
        /// <summary>ｼｭの有効かどうか を取得、設定</summary>
        public bool IsEnableSh
        {
            get => this.isEnambleSh_;

            set => this.SetProperty(ref this.isEnambleSh_, value);
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド用メソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // リクエスト
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if (args.PropertyName == nameof(this.IsDark)) {
                Properties.Settings.Default.IsDark = this.IsDark;
            }
            if (args.PropertyName == nameof(this.InstallFolder)) {
                Properties.Settings.Default.InstallFolder = this.InstallFolder;
            }
            if (args.PropertyName == nameof(this.Volume)) {
                Properties.Settings.Default.Volume = this.Volume;
            }

            Properties.Settings.Default.Save();
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        public void CreateLocalBeatmaps()
        {
            try
            {
                this.IsLoading = true;
                this.LocalBeatmaps.Clear();
                var path = $@"{this.InstallFolder}\Beat Saber_Data\CustomLevels";
                var info = new DirectoryInfo(path);
                if (!Directory.Exists(info.FullName))
                {
                    return;
                }
                info.EnumerateDirectories("*", SearchOption.TopDirectoryOnly).AsParallel().ForAll(folder => {
                    this.LocalBeatmaps.Add(new LocalBeatmapInfo(folder));
                    
                });
            }
            catch (Exception e)
            {
                this.Logger.Error(e);
            }
            finally
            {
                this.IsLoading = false;
            }
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        private readonly HttpOptions options_ = new HttpOptions()
        {
            ApplicationName = "BeatServerBrowser",
            Version = new Version(0, 1, 5),
        };
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        private static readonly ConfigMaster current_ = new ConfigMaster();
        public static ConfigMaster Current => current_;
        private ConfigMaster()
        {
            this.InstallFolder = Properties.Settings.Default.InstallFolder;
            this.Volume = Properties.Settings.Default.Volume;
            this.CurrentBeatSaver = new BeatSaver(this.options_);
            this.CurrentScoreSaber = new ScoreSaber();
            this.LocalBeatmaps = new ObservableSynchronizedCollection<LocalBeatmapInfo>();
            this.SortedLocalBeatmaps = this.LocalBeatmaps.ToSyncedSortedObservableCollection(key => key.SongTitle, isDescending: false);
            this.IsEnableSh = false;
        }
        #endregion
    }
}

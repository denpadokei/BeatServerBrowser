using BeatSaverSharp;
using BeatServerBrowser.Core.Collections;
using BeatServerBrowser.Core.ScoreSaber;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NLog;
using Prism.Mvvm;
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
        private ScoreSaberSharp currentScoreSaber_;
        /// <summary>アクセス用スコアセイバー を取得、設定</summary>
        public ScoreSaberSharp CurrentScoreSaber
        {
            get => this.currentScoreSaber_;

            set => this.SetProperty(ref this.currentScoreSaber_, value);
        }

        /// <summary>ローカルライブラリリスト を取得、設定</summary>
        private MTObservableCollection<LocalBeatmapInfo> localBeatmaps_;
        /// <summary>ローカルライブラリリスト を取得、設定</summary>
        public MTObservableCollection<LocalBeatmapInfo> LocalBeatmaps
        {
            get => this.localBeatmaps_;

            set => this.SetProperty(ref this.localBeatmaps_, value);
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
            var sirializer = new JsonSerializer();
            this.LocalBeatmaps.Clear();
            var path = $@"{this.InstallFolder}\Beat Saber_Data\CustomLevels";
            var info = new DirectoryInfo(path);
            var files = info.GetDirectories();
            var beatmaps = new List<LocalBeatmapInfo>();
            
            foreach (var folder in files) {
                foreach (var file in folder.GetFiles()) {
                    Debug.WriteLine($"ファイルを読み込みます。[{file.Name}][{file.FullName}]");
                    this.Logger.Info($"ファイルを読み込みます。[{file.Name}][{file.FullName}]");
                    if (file.Name == "info.dat") {
                        using (var steram = new StreamReader(file.FullName)) {
                            using (var reader = new StringReader(steram.ReadToEnd())) {
                                using (var jsonText = new JsonTextReader(reader)) {
                                    var beatmap = sirializer.Deserialize<LocalBeatmapInfo>(jsonText);
                                    this.LocalBeatmaps.Add(beatmap);
                                    Debug.WriteLine($"{beatmap.SongTitle}を読み込みました。");
                                    this.Logger.Info($"{beatmap.SongTitle}を読み込みました。");
                                }
                            }
                        }
                    }
                }
            }
            var temp = this.LocalBeatmaps.OrderBy(x => x.SongTitle);
            this.LocalBeatmaps = new MTObservableCollection<LocalBeatmapInfo>(temp);
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        private readonly HttpOptions options_ = new HttpOptions()
        {
            ApplicationName = "BeatServerBrowser",
            Version = new Version(0, 0, 1),
        };
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        private static readonly ConfigMaster current_ = new ConfigMaster();
        public static ConfigMaster Current => current_;
        private ConfigMaster()
        {
            this.InstallFolder = Properties.Settings.Default.InstallFolder;
            this.CurrentBeatSaver = new BeatSaver(this.options_);
            this.CurrentScoreSaber = new ScoreSaberSharp();
            this.LocalBeatmaps = new MTObservableCollection<LocalBeatmapInfo>();
        }
        #endregion
    }
}

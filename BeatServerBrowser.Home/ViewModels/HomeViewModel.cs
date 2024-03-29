﻿using BeatServerBrowser.Core.Bases;
using BeatServerBrowser.Core.Interfaces;
using BeatServerBrowser.Core.Models;
using MaterialDesignThemes.Wpf;
using NAudio.Wave;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace BeatServerBrowser.Home.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        public int LocalSongCount => this.Config.LocalBeatmaps.Count;

        /// <summary>説明 を取得、設定</summary>
        private AudioFileReader soudFile_;
        /// <summary>説明 を取得、設定</summary>
        public AudioFileReader SoundFile
        {
            get => this.soudFile_;

            set => this.SetProperty(ref this.soudFile_, value);
        }

        /// <summary>ｼｭ再生プレーヤー を取得、設定</summary>
        private WaveOut shPlayer_;
        /// <summary>ｼｭ再生プレーヤー を取得、設定</summary>
        public WaveOut ShPlayer
        {
            get => this.shPlayer_;

            set => this.SetProperty(ref this.shPlayer_, value);
        }

        /// <summary>メニューが開いているかどうか を取得、設定</summary>
        private bool isOpenDraw_;
        /// <summary>メニューが開いているかどうか を取得、設定</summary>
        public bool IsOpenDraw
        {
            get => this.isOpenDraw_;

            set => this.SetProperty(ref this.isOpenDraw_, value);
        }

        /// <summary>リピートアイコン を取得、設定</summary>
        private PackIconKind repeatIcon_;
        /// <summary>リピートアイコン を取得、設定</summary>
        public PackIconKind RepeatIcon
        {
            get => this.repeatIcon_;

            set => this.SetProperty(ref this.repeatIcon_, value);
        }

        /// <summary>シャッフルアイコン を取得、設定</summary>
        private PackIconKind shuffuleIcon_;
        /// <summary>シャッフルアイコン を取得、設定</summary>
        public PackIconKind ShuffuleIcon
        {
            get => this.shuffuleIcon_;

            set => this.SetProperty(ref this.shuffuleIcon_, value);
        }
        /// <summary>リピートするかどうか を取得、設定</summary>
        private bool isRepeat_;
        /// <summary>リピートするかどうか を取得、設定</summary>
        public bool IsRepeat
        {
            get => this.isRepeat_;

            set => this.SetProperty(ref this.isRepeat_, value);
        }

        /// <summary>シャッフルするかどうか を取得、設定</summary>
        private bool isShuffule_;
        /// <summary>シャッフルするかどうか を取得、設定</summary>
        public bool IsShuffule
        {
            get => this.isShuffule_;

            set => this.SetProperty(ref this.isShuffule_, value);
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド
        /// <summary>リストコマンド を取得、設定</summary>
        private DelegateCommand navigateListCommand_;
        /// <summary>リストコマンド を取得、設定</summary>
        public DelegateCommand NavigateListCommand => this.navigateListCommand_ ?? (this.navigateListCommand_ = new DelegateCommand(this.NavigateList));

        /// <summary>ローカルライブラリコマンド を取得、設定</summary>
        private DelegateCommand navigateLocalCommand_;
        /// <summary>ローカルライブラリコマンド を取得、設定</summary>
        public DelegateCommand NavigateLocalCommand => this.navigateLocalCommand_ ?? (this.navigateLocalCommand_ = new DelegateCommand(this.NavigateLocal));

        /// <summary>検索コマンド を取得、設定</summary>
        private DelegateCommand navigateSerchCommand_;
        /// <summary>検索コマンド を取得、設定</summary>
        public DelegateCommand NavigateSerchCommand => this.navigateSerchCommand_ ?? (this.navigateSerchCommand_ = new DelegateCommand(this.NavigateSerch));

        /// <summary>設定コマンド を取得、設定</summary>
        private DelegateCommand showSettingWindowCommand_;
        /// <summary>設定コマンド を取得、設定</summary>
        public DelegateCommand ShowSettingWindowCommand => this.showSettingWindowCommand_ ?? (this.showSettingWindowCommand_ = new DelegateCommand(this.ShowSettingWindow));

        /// <summary>プレイリストコマンド を取得、設定</summary>
        private DelegateCommand navigatePlaylistCommand_;
        /// <summary>プレイリストコマンド を取得、設定</summary>
        public DelegateCommand NavigatePlaylistCommand => this.navigatePlaylistCommand_ ?? (this.navigatePlaylistCommand_ = new DelegateCommand(this.NavigatePlaylist));

        /// <summary>停止 を取得、設定</summary>
        private DelegateCommand stopCommand_;
        /// <summary>停止 を取得、設定</summary>
        public DelegateCommand StopCommand => this.stopCommand_ ?? (this.stopCommand_ = new DelegateCommand(this.Stop));

        /// <summary>次の曲へのコマンド を取得、設定</summary>
        private DelegateCommand skipForwordCommand_;
        /// <summary>次の曲へのコマンド を取得、設定</summary>
        public DelegateCommand SkipForwordCommand => this.skipForwordCommand_ ?? (this.skipForwordCommand_ = new DelegateCommand(this.SkipForword));

        /// <summary>前の曲へコマンド を取得、設定</summary>
        private DelegateCommand skipBackwordCommand_;
        /// <summary>前の曲へコマンド を取得、設定</summary>
        public DelegateCommand SkipBackwordCommand => this.skipBackwordCommand_ ?? (this.skipBackwordCommand_ = new DelegateCommand(this.SkipBackword));

        /// <summary>詳細表示 を取得、設定</summary>
        private DelegateCommand showDetailCommand_;
        /// <summary>詳細表示 を取得、設定</summary>
        public DelegateCommand ShowDetailCommand => this.showDetailCommand_ ?? (this.showDetailCommand_ = new DelegateCommand(this.ShowDetail));
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド用メソッド
        private void NavigateList()
        {
            this.regionManager_?.RequestNavigate("MainRegion", "ListMain");
        }

        private void NavigateSerch()
        {
            this.regionManager_?.RequestNavigate("MainRegion", "SerchMain");
        }

        private void NavigateLocal()
        {
            this.regionManager_?.RequestNavigate("MainRegion", "LocalMain");
        }

        private void NavigatePlaylist()
        {
            this.regionManager_?.RequestNavigate("MainRegion", "PlaylistMain");
        }

        private void ShowSettingWindow()
        {
            this.dialogService_?.ShowDialog("SettingView", new DialogParameters(), _ => { });
        }

        private void Stop()
        {
            this.Player.Stop();
        }

        private void ShowDetail()
        {
            this.Player.Beatmap.ShowDetailCommand?.Execute();
        }

        private void SkipForword()
        {
            this.Player.SkipForward();
        }

        private void SkipBackword()
        {
            this.Player.SkipBackword();
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // リクエスト
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        public override void OnInitialize()
        {
            ConfigMaster.Current.IsDark = Core.Properties.Settings.Default.IsDark;

            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();

            var baseTheme = ConfigMaster.Current.IsDark ? new MaterialDesignDarkTheme() : (IBaseTheme)new MaterialDesignLightTheme();

            theme.SetBaseTheme(baseTheme);
            paletteHelper.SetTheme(theme);

            WeakEventManager<INotifyPropertyChanged, PropertyChangedEventArgs>.AddHandler(
                ConfigMaster.Current, nameof(INotifyPropertyChanged.PropertyChanged), this.OnPropertyChangeConfigMaster);
            if (this.loadingService_ is INotifyPropertyChanged service) {
                WeakEventManager<INotifyPropertyChanged, PropertyChangedEventArgs>.AddHandler(
                    service, nameof(INotifyPropertyChanged.PropertyChanged), this.OnLoadingServicePropertyChanged);
            }
            this.Config.LocalBeatmaps.CollectionChanged += this.OnCollectionChanged;
            if (string.IsNullOrWhiteSpace(ConfigMaster.Current.InstallFolder) || !File.Exists(@$"{ConfigMaster.Current.InstallFolder}\Beat Saber.exe")) {
                this.dialogService_.Show("SettingView", new DialogParameters(), _ => { });
            }
            this.RepeatIcon = PackIconKind.RepeatOff;
            this.Player.RepeatMode = PackIconKind.RepeatOff;
            this.ShuffuleIcon = PackIconKind.ShuffleDisabled;

            this.IsRepeat = true;
            this.IsShuffule = true;
            this.loadingService_.Load(this.Config.CreateLocalBeatmaps);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if (this.Config.IsEnableSh && args.PropertyName == nameof(this.IsOpenDraw) && this.IsOpenDraw) {
                lock (this.lockObject_) {
                    try {
                        this.SoundFile.Position = 0;
                        this.ShPlayer.Init(this.SoundFile);
                        this.ShPlayer?.Play();
                    }
                    catch (Exception e) {
                        Debug.WriteLine(e);
                    }
                }
            }
            if (args.PropertyName == nameof(this.IsRepeat)) {
                if (this.IsRepeat == true && this.RepeatIcon == PackIconKind.RepeatOff) {
                    this.RepeatIcon = PackIconKind.Repeat;
                }
                else if (this.IsRepeat == false && this.RepeatIcon == PackIconKind.Repeat) {
                    this.IsRepeat = true;
                    this.RepeatIcon = PackIconKind.RepeatOne;
                }
                else if (this.IsRepeat == false && this.RepeatIcon == PackIconKind.RepeatOne) {
                    this.RepeatIcon = PackIconKind.RepeatOff;
                }
                this.Player.RepeatMode = this.RepeatIcon;
            }
            if (args.PropertyName == nameof(this.IsShuffule)) {
                if (this.IsShuffule) {
                    this.ShuffuleIcon = PackIconKind.ShuffleVariant;
                }
                else {
                    this.ShuffuleIcon = PackIconKind.ShuffleDisabled;
                }
                this.Player.IsShuffule = this.IsShuffule;
            }
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        private void OnPropertyChangeConfigMaster(object sender, PropertyChangedEventArgs e)
        {
            if (sender is ConfigMaster) {
                if (e.PropertyName == nameof(ConfigMaster.IsDark)) {
                    var paletteHelper = new PaletteHelper();
                    var theme = paletteHelper.GetTheme();

                    var baseTheme = ConfigMaster.Current.IsDark ? new MaterialDesignDarkTheme() : (IBaseTheme)new MaterialDesignLightTheme();

                    theme.SetBaseTheme(baseTheme);
                    paletteHelper.SetTheme(theme);

                    Core.Properties.Settings.Default.IsDark = ConfigMaster.Current.IsDark;
                    Core.Properties.Settings.Default.Save();
                }
                else if (e.PropertyName == nameof(ConfigMaster.IsLoading)) {
                    this.IsLoading = this.Config.IsLoading;
                }
            }
        }

        private void OnCollectionChanged(object sendor, NotifyCollectionChangedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(this.LocalSongCount));
        }

        private void OnLoadingServicePropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            if (sender is ILoadingService && e.PropertyName == nameof(ILoadingService.IsLoading)) {
                this.IsLoading = this.loadingService_.IsLoading;
            }
        }
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
        public HomeViewModel()
        {
            this.ShPlayer = new WaveOut();
            //this.SoundFile = new AudioFileReader(Path.Combine(ConfigMaster.ThisDirectoryPath, @"Sounds\shukansei.wav"));
        }
        #endregion
    }
}

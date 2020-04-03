using BeatServerBrowser.Core.Bases;
using BeatServerBrowser.Core.Interfaces;
using BeatServerBrowser.Core.Models;
using MaterialDesignThemes.Wpf;
using NAudio.Wave;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;

namespace BeatServerBrowser.Home.ViewModels
{
    public class HomeViewModel : ViewModelBase, INavigationAware
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
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
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
            this.SoundFile = new AudioFileReader(Path.Combine(ConfigMaster.ThisDirectoryPath, @"Sounds\shukansei.wav"));
        }
        #endregion
    }
}

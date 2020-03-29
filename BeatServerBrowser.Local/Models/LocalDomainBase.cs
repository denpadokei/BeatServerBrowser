using BeatServerBrowser.Core.Models;
using Prism.Mvvm;
using BeatServerBrowser.Core.Collections;
using StatefulModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NLog;
using BeatServerBrowser.Core.Extentions;
using System.Windows;
using System.ComponentModel;

namespace BeatServerBrowser.Local.Models
{
    public abstract class LocalDomainBase : BindableBase
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        protected virtual Logger Logger => LogManager.GetCurrentClassLogger();

        /// <summary>ローカルライブラリコレクション を取得、設定</summary>
        private ObservableSynchronizedCollection<LocalBeatmapInfo> localBeatmaps_;
        /// <summary>ローカルライブラリコレクション を取得、設定</summary>
        public ObservableSynchronizedCollection<LocalBeatmapInfo> LocalBeatmaps
        {
            get => this.localBeatmaps_;

            set => this.SetProperty(ref this.localBeatmaps_, value);
        }

        /// <summary>絞り込み済みのリスト を取得、設定</summary>
        private ObservableSynchronizedCollection<LocalBeatmapInfo> filterdmaps_;
        /// <summary>絞り込み済みのリスト を取得、設定</summary>
        public ObservableSynchronizedCollection<LocalBeatmapInfo> FilteredMaps
        {
            get => this.filterdmaps_;

            set => this.SetProperty(ref this.filterdmaps_, value);
        }

        /// <summary>フィルター を取得、設定</summary>
        private ListFilter filter_;
        /// <summary>フィルター を取得、設定</summary>
        public ListFilter Filter
        {
            get => this.filter_;

            set => this.SetProperty(ref this.filter_, value);
        }

        /// <summary>カウンター を取得、設定</summary>
        private int count_;
        /// <summary>カウンター を取得、設定</summary>
        public int Count
        {
            get => this.count_;

            set => this.SetProperty(ref this.count_, value);
        }

        /// <summary>読み込み中かどうか を取得、設定</summary>
        private bool isLoadiong_;
        /// <summary>読み込み中かどうか を取得、設定</summary>
        public bool IsLoading
        {
            get => this.isLoadiong_;

            set => this.SetProperty(ref this.isLoadiong_, value);
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
        public Action<LocalBeatmapInfo> DeleteSong { get; set; }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        protected abstract void OnLocalmapPropertyChanged(object sender, PropertyChangedEventArgs e);
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        public abstract void Serch();

        public void Filtering()
        {
            if (this.IsLoading) {
                return;
            }

            this.FilteredMaps.Clear();
            if (string.IsNullOrWhiteSpace(this.Filter.FilterText)) {
                foreach (var beatmap in ConfigMaster.Current.LocalBeatmaps) {
                    beatmap.DeleteSongAction -= this.DeleteSong;
                    beatmap.DeleteSongAction += this.DeleteSong;
                    this.FilteredMaps.Add(beatmap);
                }
            }
            else if (ConfigMaster.Current.LocalBeatmaps.Where(x => x.SongTitle !=null || x.LevelAuthorName != null).Any()) {
                foreach (var beatmap in ConfigMaster.Current.LocalBeatmaps.Where(x => x.SongTitle != null && x.LevelAuthorName != null
                && (x.SongTitle.ToUpper().Contains(this.Filter.FilterText.ToUpper())
                || x.LevelAuthorName.ToUpper().Contains(this.Filter.FilterText.ToUpper())))) {
                    beatmap.DeleteSongAction -= this.DeleteSong;
                    beatmap.DeleteSongAction += this.DeleteSong;
                    this.FilteredMaps.Add(beatmap);
                }
            }
            this.Count = 0;
            this.LocalBeatmaps.Clear();
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        public LocalDomainBase()
        {
            this.LocalBeatmaps = new ObservableSynchronizedCollection<LocalBeatmapInfo>();
            this.FilteredMaps = new ObservableSynchronizedCollection<LocalBeatmapInfo>();
            this.Filter = new ListFilter();
            this.Count = 0;
            WeakEventManager<INotifyPropertyChanged, PropertyChangedEventArgs>.AddHandler(
                ConfigMaster.Current.LocalBeatmaps, nameof(INotifyPropertyChanged.PropertyChanged), this.OnLocalmapPropertyChanged);
        }
        #endregion
    }
}

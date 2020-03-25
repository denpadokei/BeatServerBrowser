using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using StatefulModel;
using BeatServerBrowser.Core.Models;
using System.Collections;
using BeatServerBrowser.Core.Extentions;

namespace BeatServerBrowser.PlayList.Models
{
    public class PlaylistSongsDomain : BindableBase
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        /// <summary>ローカルライブラリコレクション を取得、設定</summary>
        private ObservableSynchronizedCollection<LocalBeatmapInfo> localBeatmaps_;
        /// <summary>ローカルライブラリコレクション を取得、設定</summary>
        public ObservableSynchronizedCollection<LocalBeatmapInfo> LocalBeatmaps
        {
            get => this.localBeatmaps_;

            set => this.SetProperty(ref this.localBeatmaps_, value);
        }

        /// <summary>プレイリストコレクション を取得、設定</summary>
        private ObservableSynchronizedCollection<LocalBeatmapInfo> playlistBeatmaps_;
        /// <summary>プレイリストコレクション を取得、設定</summary>
        public ObservableSynchronizedCollection<LocalBeatmapInfo> PlaylistBeatmaps
        {
            get => this.playlistBeatmaps_;

            set => this.SetProperty(ref this.playlistBeatmaps_, value);
        }

        /// <summary>ローカルフィルター を取得、設定</summary>
        private PlaylistFilter localBeatmapFilter_;
        /// <summary>ローカルフィルター を取得、設定</summary>
        public PlaylistFilter LocalBeatmapFilter
        {
            get => this.localBeatmapFilter_;

            set => this.SetProperty(ref this.localBeatmapFilter_, value);
        }

        /// <summary>プレイリストフィルター を取得、設定</summary>
        private PlaylistFilter playlistFilter_;
        /// <summary>プレイリストフィルター を取得、設定</summary>
        public PlaylistFilter PlaylistFilter
        {
            get => this.playlistFilter_;

            set => this.SetProperty(ref this.playlistFilter_, value);
        }

        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド用メソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        public void Add(IList list)
        {
            foreach (var item in list) {
                if (item is LocalBeatmapInfo beatmap) {
                    this.nonFilteredLocalbeatmaps_.Remove(beatmap);
                    this.nonFilteredPlaylistbeatmaps_.Add(beatmap);
                }
            }
            this.FilteringLocalBeatmap();
            this.FilteringPlaylist();
        }

        public void Delete(IList list)
        {
            foreach (var item in list) {
                if (item is LocalBeatmapInfo beatmap) {
                    this.nonFilteredPlaylistbeatmaps_.Remove(beatmap);
                    this.nonFilteredLocalbeatmaps_.Add(beatmap);
                }
                this.FilteringLocalBeatmap();
                this.FilteringPlaylist();
            }
        }

        public void FilteringLocalBeatmap()
        {
            this.LocalBeatmaps.Clear();
            if (string.IsNullOrWhiteSpace(this.LocalBeatmapFilter.FilterText)) {
                this.LocalBeatmaps.AddRange(this.nonFilteredLocalbeatmaps_);
            }
            else {
                foreach (var beatmap in this.nonFilteredLocalbeatmaps_.Where(x =>
            x.SongTitle.ToUpper().Contains(this.LocalBeatmapFilter.FilterText.ToUpper())
            || x.LevelAuthorName.ToUpper().Contains(this.LocalBeatmapFilter.FilterText.ToUpper()))) {
                    this.LocalBeatmaps.Add(beatmap);
                }
            }
        }

        public void FilteringPlaylist()
        {
            this.PlaylistBeatmaps.Clear();
            if (string.IsNullOrWhiteSpace(this.PlaylistFilter.FilterText)) {
                this.PlaylistBeatmaps.AddRange(this.nonFilteredPlaylistbeatmaps_);
            }
            else {
                foreach (var beatmap in this.nonFilteredPlaylistbeatmaps_.Where(x =>
            x.SongTitle.ToUpper().Contains(this.LocalBeatmapFilter.FilterText.ToUpper())
            || x.LevelAuthorName.ToUpper().Contains(this.LocalBeatmapFilter.FilterText.ToUpper()))) {
                    this.PlaylistBeatmaps.Add(beatmap);
                }
            }
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        public List<LocalBeatmapInfo> nonFilteredLocalbeatmaps_;

        public List<LocalBeatmapInfo> nonFilteredPlaylistbeatmaps_;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        public PlaylistSongsDomain()
        {
            this.LocalBeatmaps = new ObservableSynchronizedCollection<LocalBeatmapInfo>();
            this.PlaylistBeatmaps = new ObservableSynchronizedCollection<LocalBeatmapInfo>();
            this.LocalBeatmapFilter = new PlaylistFilter();
            this.PlaylistFilter = new PlaylistFilter();
            this.nonFilteredLocalbeatmaps_ = new List<LocalBeatmapInfo>();
            this.nonFilteredPlaylistbeatmaps_ = new List<LocalBeatmapInfo>();
        }
        #endregion
    }
}

using BeatServerBrowser.Core.Extentions;
using BeatServerBrowser.Core.Models;
using Prism.Mvvm;
using StatefulModel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>ソートしたローカルライブラリ を取得、設定</summary>
        private SortedObservableCollection<LocalBeatmapInfo, string> sortedLocalBeatmaps_;
        /// <summary>ソートしたローカルライブラリ を取得、設定</summary>
        public SortedObservableCollection<LocalBeatmapInfo, string> SortedLocalBeatmaps
        {
            get => this.sortedLocalBeatmaps_;

            set => this.SetProperty(ref this.sortedLocalBeatmaps_, value);
        }

        /// <summary>ソートしたプレイリスト を取得、設定</summary>
        private SortedObservableCollection<LocalBeatmapInfo, int> sortedPlaylistBeatmaps_;
        /// <summary>ソートしたプレイリスト を取得、設定</summary>
        public SortedObservableCollection<LocalBeatmapInfo, int> SortedPlaylistBeatmaps
        {
            get => this.sortedPlaylistBeatmaps_;

            set => this.SetProperty(ref this.sortedPlaylistBeatmaps_, value);
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
                    beatmap.Index = this.nonFilteredPlaylistbeatmaps_.Count;
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
                    beatmap.Index = 0;
                    this.nonFilteredLocalbeatmaps_.Add(beatmap);
                }
                this.FilteringLocalBeatmap();
                this.FilteringPlaylist();
            }
        }

        public void Move(IList list, bool isUp)
        {
            if (isUp) {
                if (list.OfType<LocalBeatmapInfo>().OrderBy(x => x.Index).FirstOrDefault().Index == 0) {
                    return;
                }
                foreach (var beatmap in list.OfType<LocalBeatmapInfo>().OrderBy(x => x.Index)) {
                    var tmp = this.SortedPlaylistBeatmaps[beatmap.Index - 1];
                    this.SortedPlaylistBeatmaps.Remove(this.SortedPlaylistBeatmaps.FirstOrDefault(x => x.Index == beatmap.Index - 1));
                    beatmap.Index -= 1;
                    tmp.Index += 1;
                    this.SortedPlaylistBeatmaps.Add(tmp);
                }
            }
            else {
                if (list.OfType<LocalBeatmapInfo>().OrderByDescending(x => x.Index).FirstOrDefault().Index == this.PlaylistBeatmaps.Count - 1) {
                    return;
                }
                foreach (var beatmap in list.OfType<LocalBeatmapInfo>().OrderByDescending(x => x.Index)) {
                    var tmp = this.SortedPlaylistBeatmaps[beatmap.Index + 1];
                    this.SortedPlaylistBeatmaps.Remove(this.SortedPlaylistBeatmaps.FirstOrDefault(x => x.Index == beatmap.Index + 1));
                    beatmap.Index += 1;
                    tmp.Index -= 1;
                    this.SortedPlaylistBeatmaps.Add(tmp);
                }
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
            x.SongTitle.ToUpper().Contains(this.PlaylistFilter.FilterText.ToUpper())
            || x.LevelAuthorName.ToUpper().Contains(this.PlaylistFilter.FilterText.ToUpper()))) {
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
            this.SortedPlaylistBeatmaps = this.PlaylistBeatmaps.ToSyncedSortedObservableCollection(key => key.Index, isDescending: false);
            this.SortedLocalBeatmaps = this.LocalBeatmaps.ToSyncedSortedObservableCollection(key => key.SongTitle, isDescending: false);
            this.LocalBeatmapFilter = new PlaylistFilter();
            this.PlaylistFilter = new PlaylistFilter();
            this.nonFilteredLocalbeatmaps_ = new List<LocalBeatmapInfo>();
            this.nonFilteredPlaylistbeatmaps_ = new List<LocalBeatmapInfo>();
        }
        #endregion
    }
}

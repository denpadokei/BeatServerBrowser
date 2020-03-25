using BeatServerBrowser.Core.Collections;
using BeatServerBrowser.Core.Models;
using BeatServerBrowser.PlayList.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BeatServerBrowser.PlayList.ViewModels
{
    public class PlaylistSongsViewModel : BindableBase, IDialogAware
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        /// <summary>タイトル を取得、設定</summary>
        private string title_;
        /// <summary>タイトル を取得、設定</summary>
        public string Title
        {
            get => this.title_;

            set => this.SetProperty(ref this.title_, value);
        }

        /// <summary>ローカルライブラリ を取得、設定</summary>
        private MTObservableCollection<LocalBeatmapInfo> localBeatmaps_;
        /// <summary>ローカルライブラリ を取得、設定</summary>
        public MTObservableCollection<LocalBeatmapInfo> LocalBeatmaps
        {
            get => this.localBeatmaps_;

            set => this.SetProperty(ref this.localBeatmaps_, value);
        }

        /// <summary>プレイリスト を取得、設定</summary>
        private MTObservableCollection<LocalBeatmapInfo> playlistBeatmaps_;
        /// <summary>プレイリスト を取得、設定</summary>
        public MTObservableCollection<LocalBeatmapInfo> PlaylistBeatmaps
        {
            get => this.playlistBeatmaps_;

            set => this.SetProperty(ref this.playlistBeatmaps_, value);
        }

        /// <summary>プレイリストエンティティ を取得、設定</summary>
        private PlaylistPreviewEntity playlistPreview_;
        /// <summary>プレイリストエンティティ を取得、設定</summary>
        public PlaylistPreviewEntity PlaylistPreview
        {
            get => this.playlistPreview_;

            set => this.SetProperty(ref this.playlistPreview_, value);
        }

        /// <summary>ローカルライブラリフィルター を取得、設定</summary>
        private PlaylistFilter localBeatmapFilter_;
        /// <summary>ローカルライブラリフィルター を取得、設定</summary>
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
        /// <summary>設定コマンド を取得、設定</summary>
        private DelegateCommand applyCommand_;
        /// <summary>設定コマンド を取得、設定</summary>
        public DelegateCommand ApplyCommand => this.applyCommand_ ?? (this.applyCommand_ = new DelegateCommand(this.Apply));

        /// <summary>キャンセル を取得、設定</summary>
        private DelegateCommand cancelCommand_;
        /// <summary>キャンセル を取得、設定</summary>
        public DelegateCommand CancelCommand => this.cancelCommand_ ?? (this.cancelCommand_ = new DelegateCommand(this.Cancel));

        /// <summary>追加コマンド を取得、設定</summary>
        private DelegateCommand<IList> addCommand_;
        /// <summary>追加コマンド を取得、設定</summary>
        public DelegateCommand<IList> AddComand => this.addCommand_ ?? (this.addCommand_ = new DelegateCommand<IList>(this.Add));

        /// <summary>削除コマンド を取得、設定</summary>
        private DelegateCommand<IList> deletecCommand_;
        /// <summary>削除コマンド を取得、設定</summary>
        public DelegateCommand<IList> DeleteCommand => this.deletecCommand_ ?? (this.deletecCommand_ = new DelegateCommand<IList>(this.Delete));
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド用メソッド
        private async void Apply()
        {
            var jsonentity = new PlaylistJsonEntity();
            jsonentity.Image = $"data:image/jpg;base64,{this.PlaylistPreview.CoverImage}";
            jsonentity.PlayListAuthor = this.PlaylistPreview.Author;
            jsonentity.PlayListTytle = this.PlaylistPreview.PlaylistName;
            jsonentity.PlayListDescription = this.PlaylistPreview.DesctiptionText;
            jsonentity.FileLock = null;
            foreach (var item in this.domain_.nonFilteredPlaylistbeatmaps_) {
                var beatmap = new PlaylistSongEntity();
                beatmap.Key = await item.GetKey();
                beatmap.SongName = item.SongName;
                beatmap.Hash = item.SongHash;
                jsonentity.Songs.Add(beatmap);
            }

            var json = JsonConvert.SerializeObject(jsonentity);
            var param = new DialogParameters()
            {
                { "Playlist", this.PlaylistPreview },
                { "Songlist", json }
            };
            var result = new DialogResult(ButtonResult.OK, param);
            this.RequestClose?.Invoke(result);
        }

        private void Cancel()
        {
            this.RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel, null));
        }

        private void Add(IList songs)
        {
            if (songs is IList songlist) {
                this.domain_.Add(songlist);
            }
        }

        private void Delete(IList songs)
        {
            if (songs is IList songlist) {
                this.domain_.Delete(songlist);
            }
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックイベント
        public event Action<IDialogResult> RequestClose;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            this.Title = parameters.GetValue<string>("Title");
            this.PlaylistPreview = parameters.GetValue<PlaylistPreviewEntity>("Playlist");
            this.domain_.nonFilteredLocalbeatmaps_.AddRange(ConfigMaster.Current.LocalBeatmaps);
            var songlist = new List<LocalBeatmapInfo>();
            foreach (var beatmap in this.PlaylistPreview.Entity.Songs) {
                var playlistsong = ConfigMaster.Current.LocalBeatmaps.FirstOrDefault(x => x.SongHash == beatmap.Hash);
                if (playlistsong == null) {
                    continue;
                }
                songlist.Add(playlistsong);
            }
            this.domain_.Add(songlist);
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        private PlaylistSongsDomain domain_;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        public PlaylistSongsViewModel()
        {
            this.domain_ = new PlaylistSongsDomain();
            this.PlaylistFilter = this.domain_.PlaylistFilter;
            this.LocalBeatmapFilter = this.domain_.LocalBeatmapFilter;

            this.LocalBeatmaps = new MTObservableCollection<LocalBeatmapInfo>(this.domain_.LocalBeatmaps);
            this.PlaylistBeatmaps = new MTObservableCollection<LocalBeatmapInfo>(this.domain_.PlaylistBeatmaps);
        }
        #endregion
    }
}

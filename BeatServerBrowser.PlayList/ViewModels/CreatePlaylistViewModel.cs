using BeatServerBrowser.Core.Bases;
using BeatServerBrowser.Core.Collections;
using BeatServerBrowser.Core.Models;
using BeatServerBrowser.PlayList.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace BeatServerBrowser.PlayList.ViewModels
{
    public class CreatePlaylistViewModel : ViewModelBase
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        /// <summary>プレイリストコレクション を取得、設定</summary>
        private MTObservableCollection<PlaylistPreviewEntity> playlists_;
        /// <summary>プレイリストコレクション を取得、設定</summary>
        public MTObservableCollection<PlaylistPreviewEntity> Playlists
        {
            get => this.playlists_;

            set => this.SetProperty(ref this.playlists_, value);
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド
        /// <summary>新規作成コマンド を取得、設定</summary>
        private DelegateCommand addCommand_;
        /// <summary>新規作成コマンド を取得、設定</summary>
        public DelegateCommand AddCommand => this.addCommand_ ?? (this.addCommand_ = new DelegateCommand(this.Add));

        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド用メソッド
        private void Add()
        {
            var entity = new PlaylistPreviewEntity();
            var param = new DialogParameters()
            {
                { "Title", entity.PlaylistName },
                { "Playlist", entity }
            };
            this.dialogService_.ShowDialog("PlaylistSongs", param, result => {
                if (result.Result == ButtonResult.OK) {
                    // ここでJSON化する処理
                    var json = result.Parameters.GetValue<string>("Songlist");
                    var playlist = result.Parameters.GetValue<PlaylistPreviewEntity>("Playlist");
                    using (var writer = this.domain_.CreateJsonFile(entity)) {
                        writer.Write(json);
                        writer.Close();
                    }
                    this.loadingService_?.Load(this.domain_.LoadPlaylists);
                }
            });
        }

        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        public override void OnInitialize()
        {
            base.OnInitialize();

            this.loadingService_?.Load(this.domain_.LoadPlaylists);
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックイベント
        
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        private void EditPlaylist(PlaylistPreviewEntity entity)
        {
            var param = new DialogParameters()
            {
                { "Title", entity.PlaylistName },
                { "Playlist", entity }
            };
            this.dialogService_.ShowDialog("PlaylistSongs", param, result => {
                if (result.Result == ButtonResult.OK) {
                    // ここでJSON化する処理
                    var json = result.Parameters.GetValue<string>("Songlist");
                    var playlist = result.Parameters.GetValue<PlaylistPreviewEntity>("Playlist");
                    using (var writer = this.domain_.CreateJsonFile(entity)) {
                        writer.Write(json);
                        writer.Close();
                    }
                    this.loadingService_?.Load(this.domain_.LoadPlaylists);
                }
            });
        }

        private void DeletePlaylist(PlaylistPreviewEntity entity)
        {
            var param = new DialogParameters()
            {
                { "Title", "確認" },
                { "Message", "プレイリストを削除します。よろしいですか？"}
            };
            this.dialogService_?.ShowDialog("ConfimationDialog", param, result => {
                if (result.Result == ButtonResult.Yes) {
                    entity.Json.Delete();
                    this.loadingService_?.Load(this.domain_.LoadPlaylists);
                }
            });
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        private PlaylistDomain domain_;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        public CreatePlaylistViewModel()
        {
            this.domain_ = new PlaylistDomain();
            this.domain_.EditEvent += this.EditPlaylist;
            this.domain_.DeleteEvent += this.DeletePlaylist;
            this.Playlists = new MTObservableCollection<PlaylistPreviewEntity>(this.domain_.Playlists);
        }
        #endregion
    }
}

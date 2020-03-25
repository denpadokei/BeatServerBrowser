using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

namespace BeatServerBrowser.Core.Models
{
    public class PlaylistPreviewEntity : BindableBase
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        /// <summary>プレイリストタイトル を取得、設定</summary>
        private string playlistName_;
        /// <summary>プレイリストタイトル を取得、設定</summary>
        public string PlaylistName
        {
            get => this.playlistName_;

            set => this.SetProperty(ref this.playlistName_, value);
        }

        /// <summary>作者 を取得、設定</summary>
        private string author_;
        /// <summary>作者 を取得、設定</summary>
        public string Author
        {
            get => this.author_;

            set => this.SetProperty(ref this.author_, value);
        }

        /// <summary>説明 を取得、設定</summary>
        private string dependencyText_;
        /// <summary>説明 を取得、設定</summary>
        public string DesctiptionText
        {
            get => this.dependencyText_;

            set => this.SetProperty(ref this.dependencyText_, value);
        }

        /// <summary>カバー画像(base64) を取得、設定</summary>
        private string coverImage_;
        /// <summary>カバー画像(base64) を取得、設定</summary>
        public string CoverImage
        {
            get => this.coverImage_;

            set => this.SetProperty(ref this.coverImage_, value);
        }

        /// <summary>JSONオブジェクト を取得、設定</summary>
        private PlaylistJsonEntity entity_;
        /// <summary>JSONオブジェクト を取得、設定</summary>
        public PlaylistJsonEntity Entity
        {
            get => this.entity_;

            set => this.SetProperty(ref this.entity_, value);
        }

        /// <summary>プレイリストカバー画像 を取得、設定</summary>
        private Uri coverUrl_;
        /// <summary>プレイリストカバー画像 を取得、設定</summary>
        public Uri CoverUri
        {
            get => this.coverUrl_;

            set => this.SetProperty(ref this.coverUrl_, value);
        }

        /// <summary>カバー画像ファイルパス を取得、設定</summary>
        private string coverPath_;
        /// <summary>カバー画像ファイルパス を取得、設定</summary>
        public string CoverPath
        {
            get => this.coverPath_;

            set => this.SetProperty(ref this.coverPath_, value);
        }

        /// <summary>JSONファイル情報 を取得、設定</summary>
        private FileInfo json_;
        /// <summary>JSONファイル情報 を取得、設定</summary>
        public FileInfo Json
        {
            get => this.json_;

            set => this.SetProperty(ref this.json_, value);
        }

        public bool IsLock => this.Entity.FileLock == null;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド
        /// <summary>編集コマンド を取得、設定</summary>
        private DelegateCommand editCommand_;
        /// <summary>編集コマンド を取得、設定</summary>
        public DelegateCommand EditCommand => this.editCommand_ ?? (this.editCommand_ = new DelegateCommand(this.Edit).ObservesCanExecute(() => this.IsLock));

        /// <summary>削除コマンド を取得、設定</summary>
        private DelegateCommand deleteCommand_;
        /// <summary>削除コマンド を取得、設定</summary>
        public DelegateCommand DeleteCommand => this.deleteCommand_ ?? (this.deleteCommand_ = new DelegateCommand(this.Delete).ObservesCanExecute(() => this.IsLock));
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド用メソッド
        private void Edit()
        {
            this.EditEvent?.Invoke(this);
        }

        private void Delete()
        {
            this.DeleteEvent?.Invoke();
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if (args.PropertyName == nameof(this.Entity)) {
                this.PlaylistName = this.Entity.PlayListTytle;
                this.Author = this.Entity.PlayListAuthor;
                this.DesctiptionText = this.Entity.PlayListDescription;
                try {
                    var stringArray = this.Entity.Image.Split(',');
                    this.CoverImage = stringArray[1];
                }
                catch (Exception e) {
                    Debug.WriteLine(e);
                }
            }
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        public void CreateCoverUri(string coverPath = null)
        {
            if (coverPath == null) {

            }
            else {

            }
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックイベント
        public Action<PlaylistPreviewEntity> EditEvent;
        public Action DeleteEvent;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        public PlaylistPreviewEntity()
        {
            
        }
        #endregion
    }
}

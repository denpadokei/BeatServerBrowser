using BeatServerBrowser.Core.Services;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
            get => this.playlistName_ ?? "";

            set => this.SetProperty(ref this.playlistName_, value);
        }

        /// <summary>作者 を取得、設定</summary>
        private string author_;
        /// <summary>作者 を取得、設定</summary>
        public string Author
        {
            get => this.author_ ?? "";

            set => this.SetProperty(ref this.author_, value);
        }

        /// <summary>説明 を取得、設定</summary>
        private string descriptionText_;
        /// <summary>説明 を取得、設定</summary>
        public string DescriptionText
        {
            get => this.descriptionText_ ?? "";

            set => this.SetProperty(ref this.descriptionText_, value);
        }

        /// <summary>カバー画像(base64) を取得、設定</summary>
        private string coverImage_;
        /// <summary>カバー画像(base64) を取得、設定</summary>
        public string CoverImage
        {
            get => this.coverImage_ ?? "";

            set => this.SetProperty(ref this.coverImage_, value);
        }

        /// <summary>JSONオブジェクト を取得、設定</summary>
        private JObject entity_;
        /// <summary>JSONオブジェクト を取得、設定</summary>
        public JObject Entity
        {
            get => this.entity_ ?? JObject.FromObject(new object());

            set => this.SetProperty(ref this.entity_, value);
        }

        /// <summary>プレイリストカバー画像 を取得、設定</summary>
        private Uri coverUrl_;
        /// <summary>プレイリストカバー画像 を取得、設定</summary>
        public Uri CoverUri
        {
            get => this.coverUrl_ ?? new Uri($@"{ConfigMaster.ThisDirectoryPath}\Images\default.jpg");

            set => this.SetProperty(ref this.coverUrl_, value);
        }

        /// <summary>カバー画像ファイルパス を取得、設定</summary>
        private string coverPath_;
        /// <summary>カバー画像ファイルパス を取得、設定</summary>
        public string CoverPath
        {
            get => this.coverPath_ ?? "";

            set => this.SetProperty(ref this.coverPath_, value);
        }

        /// <summary>Base64の情報 を取得、設定</summary>
        private string base64Info_;
        /// <summary>Base64の情報 を取得、設定</summary>
        public string Base64Info
        {
            get => this.base64Info_ ?? "";

            set => this.SetProperty(ref this.base64Info_, value);
        }

        /// <summary>JSONファイル情報 を取得、設定</summary>
        private FileInfo json_;
        /// <summary>JSONファイル情報 を取得、設定</summary>
        public FileInfo Json
        {
            get => this.json_;

            set => this.SetProperty(ref this.json_, value);
        }

        public bool IsLock => true;
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

        /// <summary>プレビュー を取得、設定</summary>
        private DelegateCommand previewCommand_;
        /// <summary>プレビュー を取得、設定</summary>
        public DelegateCommand PreviewCommand => this.previewCommand_ ?? (this.previewCommand_ = new DelegateCommand(this.Preview));
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド用メソッド
        private void Edit()
        {
            this.EditEvent?.Invoke(this);
        }

        private void Delete()
        {
            this.DeleteEvent?.Invoke(this);
        }

        private void Preview()
        {
            var list = new List<LocalBeatmapInfo>();
            if (this.Entity.TryGetValue("songs", out var songs)) {
                foreach (var songjson in songs.ToObject<List<JObject>>()) {
                    if (songjson.TryGetValue("hash", out var hash)) {
                        var beatmap = ConfigMaster.Current.SortedLocalBeatmaps.FirstOrDefault(x => string.Equals(x.SongHash, hash.ToString(), StringComparison.CurrentCultureIgnoreCase));
                        list.Add(beatmap);
                    }
                }
            }

            var soundFileInfo = list[0].Directory.EnumerateFiles("*.egg", SearchOption.TopDirectoryOnly).FirstOrDefault();
            SoundPlayerService.CurrentPlayer.Play(soundFileInfo, list[0], list);
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if (args.PropertyName == nameof(this.Entity)) {
                if (this.Entity.TryGetValue("playlistTitle", out var title)) {
                    this.PlaylistName = title.ToString();
                }
                if (this.Entity.TryGetValue("playlistAuthor", out var author)) {
                    this.Author = author.ToString();
                }
                if (this.Entity.TryGetValue("playlistDescription", out var description)) {
                    this.DescriptionText = description.ToString();
                }



                try {
                    if (this.Entity.TryGetValue("image", out var image)) {
                        var stringArray = image.ToString().Split(',');
                        this.Base64Info = stringArray.FirstOrDefault();
                        this.CoverImage = stringArray.LastOrDefault();
                    }
                }
                catch (Exception e) {
                    Debug.WriteLine(e);
                }
            }
            else if (args.PropertyName == nameof(this.CoverPath)) {
                using (var stream = new FileStream(this.CoverPath, FileMode.Open, FileAccess.Read)) {
                    var body = new byte[stream.Length];
                    var readByte = stream.Read(body, 0, (int)stream.Length);
                    this.CoverImage = Convert.ToBase64String(body);
                    stream.Close();
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
        public Action<PlaylistPreviewEntity> DeleteEvent;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        public PlaylistPreviewEntity()
        {

        }

        public PlaylistPreviewEntity(FileInfo info)
        {
            this.Json = info;
        }
        #endregion
    }
}

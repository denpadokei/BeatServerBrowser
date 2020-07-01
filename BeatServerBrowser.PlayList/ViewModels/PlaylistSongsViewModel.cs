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
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using BeatServerBrowser.Core.Bases;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Text;

namespace BeatServerBrowser.PlayList.ViewModels
{
    public class PlaylistSongsViewModel : ViewModelBase, IDialogAware
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
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

        /// <summary>設定可能かどうか を取得、設定</summary>
        private bool canApply_;
        /// <summary>設定可能かどうか を取得、設定</summary>
        public bool CanApply
        {
            get => this.canApply_;

            set => this.SetProperty(ref this.canApply_, value);
        }

        /// <summary>プレイリストのフィルタリングテキスト を取得、設定</summary>
        private string playlistFilteringText_;
        /// <summary>プレイリストのフィルタリングテキスト を取得、設定</summary>
        public string PlaylistFilteringText
        {
            get => this.playlistFilteringText_;

            set => this.SetProperty(ref this.playlistFilteringText_, value);
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド
        /// <summary>設定コマンド を取得、設定</summary>
        private DelegateCommand applyCommand_;
        /// <summary>設定コマンド を取得、設定</summary>
        public DelegateCommand ApplyCommand => this.applyCommand_ ?? (this.applyCommand_ = new DelegateCommand(this.Apply, this.CanApplyMethod).ObservesProperty(() => this.CanApply));

        /// <summary>キャンセル を取得、設定</summary>
        private DelegateCommand cancelCommand_;
        /// <summary>キャンセル を取得、設定</summary>
        public DelegateCommand CancelCommand => this.cancelCommand_ ?? (this.cancelCommand_ = new DelegateCommand(this.Cancel));

        /// <summary>追加コマンド を取得、設定</summary>
        private DelegateCommand<IList> addCommand_;
        /// <summary>追加コマンド を取得、設定</summary>
        public DelegateCommand<IList> AddCommand => this.addCommand_ ?? (this.addCommand_ = new DelegateCommand<IList>(this.Add));

        /// <summary>削除コマンド を取得、設定</summary>
        private DelegateCommand<IList> deletecCommand_;
        /// <summary>削除コマンド を取得、設定</summary>
        public DelegateCommand<IList> DeleteCommand => this.deletecCommand_ ?? (this.deletecCommand_ = new DelegateCommand<IList>(this.Delete));

        /// <summary>カバー画像選択 を取得、設定</summary>
        private DelegateCommand selectCoverCommand_;
        /// <summary>カバー画像選択 を取得、設定</summary>
        public DelegateCommand SelectCoverCommand => this.selectCoverCommand_ ?? (this.selectCoverCommand_ = new DelegateCommand(this.SelectCover));

        /// <summary>停止コマンド を取得、設定</summary>
        private DelegateCommand stopCommand_;
        /// <summary>停止コマンド を取得、設定</summary>
        public DelegateCommand StopCommand => this.stopCommand_ ?? (this.stopCommand_ = new DelegateCommand(this.Stop));

        /// <summary>上に移動 を取得、設定</summary>
        private DelegateCommand<IList> moveUpCommand_;
        /// <summary>上に移動 を取得、設定</summary>
        public DelegateCommand<IList> MoveUpCommand => this.moveUpCommand_ ?? (this.moveUpCommand_ = new DelegateCommand<IList>(this.MoveUp, this.IsFilterd).ObservesProperty(() => this.PlaylistFilteringText));

        /// <summary>下に移動 を取得、設定</summary>
        private DelegateCommand<IList> moveDownCommand_;
        /// <summary>下に移動 を取得、設定</summary>
        public DelegateCommand<IList> MoveDownCommand => this.moveDownCommand_ ?? (this.moveDownCommand_ = new DelegateCommand<IList>(this.MoveDown, this.IsFilterd).ObservesProperty(() => this.PlaylistFilteringText));

        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド用メソッド
        private async void Apply()
        {
            try {
                var param = new DialogParameters(); ;

                await Task.Run(() => {
                    this.IsLoading = true;
                    var jsonentity = new PlaylistJsonEntity();
                    string metadata = "";
                    var ex = "jpg";
                    if (!string.IsNullOrWhiteSpace(this.PlaylistPreview.CoverPath)) {
                        var info = new FileInfo(this.PlaylistPreview.CoverPath);
                        var match = this.extention_.Matches(info.Extension);
                        var builder = new StringBuilder();
                        foreach (var exchar in match.Select(x => x.Value)) {
                            builder.Append(exchar);
                        }
                        ex = builder.ToString();
                        metadata = $"data:image/{ex};base64";
                    }
                    else {
                        metadata = this.PlaylistPreview.Base64Info;
                    }
                    jsonentity.Image = $"{metadata},{this.PlaylistPreview.CoverImage}";
                    jsonentity.PlayListAuthor = this.PlaylistPreview.Author;
                    jsonentity.PlayListTytle = this.PlaylistPreview.PlaylistName;
                    jsonentity.PlayListDescription = this.PlaylistPreview.DescriptionText;
                    jsonentity.FileLock = null;
                    foreach (var item in this.domain_.nonFilteredPlaylistbeatmaps_.OrderBy(x => x.Index)) {
                        var beatmap = new PlaylistSongEntity();
                        beatmap.Key = "";// await item.GetKey();
                        beatmap.SongName = item.SongTitle;
                        beatmap.Hash = item.SongHash;
                        jsonentity.Songs.Add(beatmap);
                    }

                    var json = JsonConvert.SerializeObject(jsonentity);
                    param = new DialogParameters()
                    {
                        { "Playlist", this.PlaylistPreview },
                        { "Songlist", json }
                    };
                    this.IsLoading = false;
                });
                var result = new DialogResult(ButtonResult.OK, param);
                this.RequestClose?.Invoke(result);
            }
            catch (Exception e) {
                this.Logger.Error(e);
            }
            finally {
                this.IsLoading = false;
            }
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

        private void SelectCover()
        {
            var list = new Dictionary<string, string>()
            {
                { "画像ファイル", "*.jpg, *.png" }
            };
            var paramas = new OpenFileDialogParameters
            {
                FilterList = list
            };

            IEnumerable<string> fileNames = null;

            this.customDialogService_?.ShowOpenFileDialog(paramas, out fileNames);

            if (fileNames == null) {
                return;
            }
            this.PlaylistPreview.CoverPath = fileNames.FirstOrDefault();
        }

        private void MoveUp(IList list)
        {
            if (list.Count == 0) {
                return;
            }
            this.domain_.Move(list, true);
        }

        private void MoveDown(IList list)
        {
            if (list.Count == 0) {
                return;
            }
            this.domain_.Move(list, false);
        }

        private void Stop()
        {
            this.Player.Stop();
        }

        private bool IsFilterd(IList _)
        {
            return this.domain_.nonFilteredPlaylistbeatmaps_.Count == this.domain_.SortedPlaylistBeatmaps.Count;
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if (args.PropertyName == nameof(this.PlaylistFilteringText)) {
                this.PlaylistFilter.FilterText = this.PlaylistFilteringText;
            }
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックイベント
        public event Action<IDialogResult> RequestClose;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        public bool CanCloseDialog()
        {
            return !this.IsLoading;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            this.Title = parameters.GetValue<string>("Title");
            this.PlaylistPreview = parameters.GetValue<PlaylistPreviewEntity>("Playlist");
            this.domain_.nonFilteredLocalbeatmaps_.AddRange(ConfigMaster.Current.SortedLocalBeatmaps);
            var songlist = new List<LocalBeatmapInfo>();
            foreach (var beatmap in this.PlaylistPreview.Entity.Songs) {
                var playlistsong = ConfigMaster.Current.LocalBeatmaps.FirstOrDefault(x => x.SongHash == beatmap.Hash);
                if (playlistsong == null) {
                    continue;
                }
                songlist.Add(playlistsong);
            }
            this.domain_.Add(songlist);
            WeakEventManager<INotifyPropertyChanged, PropertyChangedEventArgs>.AddHandler(
                this.PlaylistPreview, nameof(INotifyPropertyChanged.PropertyChanged), this.OnPlaylistPreviewPropertyChanged);
            this.RaisePropertyChanged(nameof(this.CanApply));
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        private void OnLocalmapFilterPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.domain_.FilteringLocalBeatmap();
        }

        private void OnPlaylistFilterPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.domain_.FilteringPlaylist();
        }

        private void OnPlaylistPreviewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(this.CanApply));
        }

        private bool CanApplyMethod()
        {
            this.CanApply = !string.IsNullOrWhiteSpace(this.PlaylistPreview.PlaylistName) && !string.IsNullOrWhiteSpace(this.PlaylistPreview.Author);
            return this.CanApply;
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        private PlaylistSongsDomain domain_;
        private Regex extention_ = new Regex("[a-z, A-Z]");
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        public PlaylistSongsViewModel()
        {
            this.domain_ = new PlaylistSongsDomain();
            this.PlaylistFilter = this.domain_.PlaylistFilter;
            this.LocalBeatmapFilter = this.domain_.LocalBeatmapFilter;
            this.PlaylistPreview = new PlaylistPreviewEntity();

            WeakEventManager<INotifyPropertyChanged, PropertyChangedEventArgs>.AddHandler(
                this.LocalBeatmapFilter, nameof(INotifyPropertyChanged.PropertyChanged), this.OnLocalmapFilterPropertyChanged);
            WeakEventManager<INotifyPropertyChanged, PropertyChangedEventArgs>.AddHandler(
                this.PlaylistFilter, nameof(INotifyPropertyChanged.PropertyChanged), this.OnPlaylistFilterPropertyChanged);

            this.LocalBeatmaps = new MTObservableCollection<LocalBeatmapInfo>(this.domain_.SortedLocalBeatmaps);
            this.PlaylistBeatmaps = new MTObservableCollection<LocalBeatmapInfo>(this.domain_.SortedPlaylistBeatmaps);
        }
        #endregion
    }
}

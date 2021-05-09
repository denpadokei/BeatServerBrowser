using Prism.Mvvm;

namespace BeatServerBrowser.PlayList.Models
{
    public class PlaylistFilter : BindableBase
    {
        /// <summary>絞り込みテキスト を取得、設定</summary>
        private string filterText_;
        /// <summary>絞り込みテキスト を取得、設定</summary>
        public string FilterText
        {
            get => this.filterText_ ?? "";

            set => this.SetProperty(ref this.filterText_, value);
        }

        public PlaylistFilter()
        {
            this.FilterText = "";
        }
    }
}

using Prism.Mvvm;

namespace BeatSaverBrowser.BeatStar.ViewModels
{
    public class ViewAViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get => this._message;
            set => this.SetProperty(ref this._message, value);
        }

        public ViewAViewModel()
        {
            this.Message = "View A from your Prism Module";
        }
    }
}

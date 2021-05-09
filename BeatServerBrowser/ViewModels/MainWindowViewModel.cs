using Prism.Mvvm;

namespace BeatServerBrowser.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public string Title { get; set; }
        public MainWindowViewModel()
        {
            this.Title = "BeatSaverBrowser";
        }
    }
}

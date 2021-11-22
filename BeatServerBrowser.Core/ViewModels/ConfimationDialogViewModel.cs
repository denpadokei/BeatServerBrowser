using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace BeatServerBrowser.Core.ViewModels
{
    public class ConfimationDialogViewModel : BindableBase, IDialogAware
    {
        public ConfimationDialogViewModel()
        {

        }

        /// <summary>タイトル を取得、設定</summary>
        private string title_;
        /// <summary>タイトル を取得、設定</summary>
        public string Title
        {
            get => this.title_;

            set => this.SetProperty(ref this.title_, value);
        }

        /// <summary>メッセージ を取得、設定</summary>
        private string message_;
        /// <summary>メッセージ を取得、設定</summary>
        public string Message
        {
            get => this.message_;

            set => this.SetProperty(ref this.message_, value);
        }

        /// <summary>OKコマンド を取得、設定</summary>
        private DelegateCommand YesOommand_;
        /// <summary>OKコマンド を取得、設定</summary>
        public DelegateCommand YesCommand => this.YesOommand_ ?? (this.YesOommand_ = new DelegateCommand(this.Yes));

        private void Yes()
        {
            this.RequestClose?.Invoke(new DialogResult(ButtonResult.Yes));
        }

        /// <summary>NOコマンド を取得、設定</summary>
        private DelegateCommand noCommand_;
        /// <summary>NOコマンド を取得、設定</summary>
        public DelegateCommand NoCommand => this.noCommand_ ?? (this.noCommand_ = new DelegateCommand(this.No));

        private void No()
        {
            this.RequestClose?.Invoke(new DialogResult(ButtonResult.No));
        }

        public event Action<IDialogResult> RequestClose;

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
            this.Message = parameters.GetValue<string>("Message");
        }
    }
}

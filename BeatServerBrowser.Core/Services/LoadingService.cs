using BeatServerBrowser.Core.Interfaces;
using NLog;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BeatServerBrowser.Core.Services
{
    public class LoadingService : BindableBase, ILoadingService
    {
        private Logger Logger => LogManager.GetCurrentClassLogger();

        /// <summary>読み込み中かどうか を取得、設定</summary>
        private bool isLoading_;
        /// <summary>読み込み中かどうか を取得、設定</summary>
        public bool IsLoading
        {
            get => this.isLoading_;

            set => this.SetProperty(ref this.isLoading_, value);
        }
        public async void Load(Action action)
        {
            try {
                this.StartLoading();
                await this.dispatcher_?.InvokeAsync(() => action?.Invoke());
            }
            catch (Exception e) {
                Debug.WriteLine(e);
                this.Logger.Error(e);
            }
            finally {
                this.EndLoading();
            }
        }

        private void StartLoading()
        {
            lock (this.lockObject_) {
                this.lockCounter_++;
                this.IsLoading = true;
            }
        }

        private void EndLoading()
        {
            lock (this.lockObject_) {
                this.lockCounter_--;
                if (this.lockCounter_ == 0) {
                    this.IsLoading = false;
                }
            }
        }

        private readonly Dispatcher dispatcher_;

        private readonly Object lockObject_ = new object();

        private int lockCounter_;

        public LoadingService()
        {
            this.lockObject_ = new Object();
            // スレッドを起動して、そこで dispatcher を実行する
            var dispatcherSource = new TaskCompletionSource<Dispatcher>();
            var thread = new Thread(new ThreadStart(() =>
            {
                dispatcherSource.SetResult(Dispatcher.CurrentDispatcher);
                Dispatcher.Run();
            }));
            thread.Start();
            this.dispatcher_ = dispatcherSource.Task.Result; // メンバ変数に dispatcher を保存

            // 表のディスパッチャーが終了するタイミングで、こちらのディスパッチャーも終了する
            Dispatcher.CurrentDispatcher.ShutdownStarted += (s, e) => this.dispatcher_.BeginInvokeShutdown(DispatcherPriority.Normal);
        }
    }
}

using Prism.Mvvm;
using StatefulModel;

namespace BeatServerBrowser.Core.Collections
{
    /// <summary>
    /// よくわかんないけどこれを使うとスレッドセーフなコレクションになるらしい
    /// Itemsにバインドするとよい感じになる
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MTObservableCollection<T> : BindableBase
    {
        /// <summary>コレクション を取得、設定</summary>
        private SynchronizationContextCollection<T> items_;
        /// <summary>コレクション を取得、設定</summary>
        public SynchronizationContextCollection<T> Items
        {
            get => this.items_;

            set => this.SetProperty(ref this.items_, value);
        }

        public T this[int i] => this.Items[i];

        public MTObservableCollection(ISynchronizableNotifyChangedCollection<T> collection)
        {
            this.Items = collection.ToSyncedSynchronizationContextCollection(System.Threading.SynchronizationContext.Current);
        }
    }
}

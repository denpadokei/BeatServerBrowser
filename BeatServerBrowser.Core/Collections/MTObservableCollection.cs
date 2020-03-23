using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Data;
using System.Windows.Threading;
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
        //public override event NotifyCollectionChangedEventHandler CollectionChanged;
        //protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        //{
        //    NotifyCollectionChangedEventHandler CollectionChanged = this.CollectionChanged;
        //    if (CollectionChanged != null) {
        //        foreach (NotifyCollectionChangedEventHandler nh in CollectionChanged.GetInvocationList()) {
        //            DispatcherObject dispObj = nh.Target as DispatcherObject;
        //            if (dispObj != null) {
        //                Dispatcher dispatcher = dispObj.Dispatcher;
        //                if (dispatcher != null && !dispatcher.CheckAccess()) {
        //                    dispatcher.BeginInvoke((Action)(() => nh.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset))), DispatcherPriority.DataBind);
        //                    continue;
        //                }
        //            }
        //            nh.Invoke(this, e);
        //        }
        //    }
        //}

        //public override event NotifyCollectionChangedEventHandler CollectionChanged;
        //private static object _syncLock = new object();

        //public MTObservableCollection(IEnumerable<T> enamble) : base(enamble)
        //{
        //    enableCollectionSynchronization(this, _syncLock);
        //}
        //public MTObservableCollection()
        //{
        //    enableCollectionSynchronization(this, _syncLock);
        //}

        //protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        //{
        //    using (this.BlockReentrancy()) {
        //        var eh = CollectionChanged;
        //        if (eh == null) return;

        //        var dispatcher = (from NotifyCollectionChangedEventHandler nh in eh.GetInvocationList()
        //                          let dpo = nh.Target as DispatcherObject
        //                          where dpo != null
        //                          select dpo.Dispatcher).FirstOrDefault();

        //        if (dispatcher != null && dispatcher.CheckAccess() == false) {
        //            dispatcher.Invoke(DispatcherPriority.DataBind, (Action)(() => this.OnCollectionChanged(e)));
        //        }
        //        else {
        //            foreach (NotifyCollectionChangedEventHandler nh in eh.GetInvocationList())
        //                nh.Invoke(this, e);
        //        }
        //    }
        //}

        //private static void enableCollectionSynchronization(IEnumerable<T> collection, object lockObject)
        //{
        //    var method = typeof(BindingOperations).GetMethod("EnableCollectionSynchronization",
        //          new Type[] { typeof(IEnumerable<T>), typeof(object) });
        //    if (method != null) {
        //        //It's .NET 4.5 
        //        method.Invoke(null, new object[] { collection, lockObject });
        //    }
        //}

        /// <summary>コレクション を取得、設定</summary>
        private SynchronizationContextCollection<T> items_;
        /// <summary>コレクション を取得、設定</summary>
        public SynchronizationContextCollection<T> Items
        {
            get => this.items_;

            set => this.SetProperty(ref this.items_, value);
        }

        public MTObservableCollection(ISynchronizableNotifyChangedCollection<T> collection)
        {
            this.Items = collection.ToSyncedSynchronizationContextCollection<T>(System.Threading.SynchronizationContext.Current);
        }
    }
}

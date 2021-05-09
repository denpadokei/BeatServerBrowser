using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;

namespace BeatServerBrowser.Core.Actions
{
    public class DataBaseContextDispose : TriggerAction<FrameworkElement>
    {
        protected override void Invoke(object parameter)
        {
            if (this.AssociatedObject.DataContext is IDisposable context) {
                context.Dispose();
            }
        }
    }
}

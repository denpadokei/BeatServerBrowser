using BeatServerBrowser.Core.Interfaces;
using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace BeatServerBrowser.Core.Actions
{
    public class DataContextInitialize : TriggerAction<FrameworkElement>
    {
        protected override void Invoke(object parameter)
        {
            if (this.AssociatedObject.DataContext is IInitializable context) {
                context.OnInitialize();
            }
        }
    }
}

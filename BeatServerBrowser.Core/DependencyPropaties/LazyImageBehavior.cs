using BeatServerBrowser.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BeatServerBrowser.Core.DependencyPropaties
{
    public class LazyImageBehavior
    {
        #region LazySource 添付プロパティ

        [AttachedPropertyBrowsableForType(typeof(Image))]
        public static Uri GetLazySource(Image element)
        {
            return (Uri)element.GetValue(LazySourceProperty);
        }

        [AttachedPropertyBrowsableForType(typeof(Image))]
        public static void SetLazySource(Image element, Uri value)
        {
            element.SetValue(LazySourceProperty, value);
        }

        public static readonly DependencyProperty LazySourceProperty =
            DependencyProperty.RegisterAttached("LazySource", typeof(Uri), typeof(LazyImageBehavior), new PropertyMetadata(null, LazySource_Changed));

        [AttachedPropertyBrowsableForType(typeof(Image))]
        public static string GetBase64Source(Image element)
        {
            return (string)element.GetValue(Base64SourceProperty);
        }

        [AttachedPropertyBrowsableForType(typeof(Image))]
        public static void SetBase64Source(Image element, string value)
        {
            element.SetValue(Base64SourceProperty, value);
        }

        public static readonly DependencyProperty Base64SourceProperty =
            DependencyProperty.RegisterAttached("Base64Source", typeof(string), typeof(LazyImageBehavior), new PropertyMetadata("", Base64Source_Changed));

        [AttachedPropertyBrowsableForType(typeof(Image))]
        public static string GetKeySource(Image element)
        {
            return (string)element.GetValue(KeySourceProperty);
        }

        [AttachedPropertyBrowsableForType(typeof(Image))]
        public static void SetKeySource(Image element, string value)
        {
            element.SetValue(KeySourceProperty, value);
        }

        public static readonly DependencyProperty KeySourceProperty =
            DependencyProperty.RegisterAttached("KeySource", typeof(byte[]), typeof(LazyImageBehavior), new PropertyMetadata(null, KeySourse_Changed));

        


        #endregion

        #region LazyImageSource 添付プロパティ

        [AttachedPropertyBrowsableForType(typeof(ImageBrush))]
        public static Uri GetLazyImageSource(ImageBrush element)
        {
            return (Uri)element.GetValue(LazyImageSourceProperty);
        }

        [AttachedPropertyBrowsableForType(typeof(ImageBrush))]
        public static void SetLazyImageSource(ImageBrush element, Uri value)
        {
            element.SetValue(LazyImageSourceProperty, value);
        }

        public static readonly DependencyProperty LazyImageSourceProperty =
            DependencyProperty.RegisterAttached("LazyImageSource", typeof(Uri), typeof(LazyImageBehavior), new PropertyMetadata(null, LazyImageSource_Changed));

        #endregion

        private static async void LazySource_Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var element = sender as Image;
            if (element == null) {
                return;
            }
            var image = await LazyBitmapImage.GetImage(e.NewValue as Uri);
            if (image != null) {
                element.Source = image;
            }
        }

        private static async void Base64Source_Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var element = sender as Image;
            if (element == null) {
                return;
            }
            var image = await LazyBitmapImage.ConvertImage(e.NewValue as string);
            if (image != null) {
                element.Source = image;
            }
        }

        private static async void KeySourse_Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var element = sender as Image;
            if (element == null) {
                return;
            }
            var image = await LazyBitmapImage.GetBeatmapImage(e.NewValue as byte[]);
            if (image != null) {
                element.Source = image;
            }
        }


        private static async void LazyImageSource_Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var element = sender as ImageBrush;
            if (element == null) {
                return;
            }
            var image = await LazyBitmapImage.GetImage(e.NewValue as Uri);
            if (image != null) {
                element.ImageSource = image;
            }
        }
    }
}

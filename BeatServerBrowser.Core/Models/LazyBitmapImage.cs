using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BeatServerBrowser.Core.Models
{
    public class LazyBitmapImage
    {
        public static Task<BitmapImage> GetImage(Uri uri)
        {
            var logger = LogManager.GetCurrentClassLogger();

            return Task.Run(() =>
            {
                var wc = new WebClient { CachePolicy = new RequestCachePolicy(RequestCacheLevel.CacheIfAvailable) };
                try {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.CreateOptions = BitmapCreateOptions.None;
                    image.StreamSource = new MemoryStream(wc.DownloadData(uri));
                    image.EndInit();
                    image.Freeze();
                    return image;
                }
                catch (WebException webe) {
                    logger.Error(webe);
                }
                catch (IOException ioe) {
                    logger.Error(ioe);
                }
                catch (InvalidOperationException ie) {
                    logger.Error(ie);
                }
                catch (Exception e) {
                    logger.Error(e);
                }
                finally {
                    wc.Dispose();
                }
                return null;
            });
        }

        public static Task<BitmapImage> ConvertImage(string base64string)
        {
            return Task.Run(() => {
                try {
                    var coverbyte = Convert.FromBase64String(base64string);
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.CreateOptions = BitmapCreateOptions.None;
                    image.StreamSource = new MemoryStream(coverbyte);
                    image.EndInit();
                    image.Freeze();
                    return image;
                }
                catch (Exception e) {
                    var logger = LogManager.GetCurrentClassLogger();
                    logger.Error(e);
                }

                return null;
            });
        }

        public static Task<BitmapImage> GetImage(string uri)
        {
            return GetImage(new Uri(uri));
        }
    }
}

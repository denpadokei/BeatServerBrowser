using BeatServerBrowser.Local.Finderes;
using System;
using System.ComponentModel;

namespace BeatServerBrowser.Local.Models
{
    public class LocalPanelDomain : LocalDomainBase
    {
        public override void Serch()
        {
            try {
                this.IsLoading = true;
                foreach (var beatmap in LocalSerch.LocalSerching(this.Count, this.FilteredMaps)) {
                    this.LocalBeatmaps.Add(beatmap);
                }
                this.Count++;
            }
            catch (Exception e) {
                this.Logger.Error(e);
            }
            finally {
                this.IsLoading = false;
            }

        }

        protected override void OnLocalmapPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Filtering();
            this.Serch();
        }
    }
}

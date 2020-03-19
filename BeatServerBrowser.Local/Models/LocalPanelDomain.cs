using BeatServerBrowser.Local.Finderes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeatServerBrowser.Local.Models
{
    public class LocalPanelDomain : LocalDomainBase
    {
        public override void Serch()
        {
            foreach (var beatmap in LocalSerch.LocalSerching(this.Count)) {
                this.LocalBeatmaps.Add(beatmap);
            }
            this.Count++;
        }
    }
}

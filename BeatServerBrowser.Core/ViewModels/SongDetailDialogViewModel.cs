using BeatSaverSharp.Models;
using BeatServerBrowser.Core.Bases;
using BeatServerBrowser.Core.Models;
using StatefulModel;
using System;
using System.ComponentModel;
using System.Linq;

namespace BeatServerBrowser.Core.ViewModels
{
    public class SongDetailDialogViewModel : ViewModelBase
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        /// <summary>譜面 を取得、設定</summary>
        private BeatmapEntity beatmap_;
        /// <summary>譜面 を取得、設定</summary>
        public BeatmapEntity Beatmap
        {
            get => this.beatmap_;

            set => this.SetProperty(ref this.beatmap_, value);
        }

        /// <summary>難易度種別 を取得、設定</summary>
        private ObservableSynchronizedCollection<BeatmapDifficulty.BeatmapCharacteristic> characteristic_;
        /// <summary>難易度種別 を取得、設定</summary>
        public ObservableSynchronizedCollection<BeatmapDifficulty.BeatmapCharacteristic> Characteristics
        {
            get => this.characteristic_;

            set => this.SetProperty(ref this.characteristic_, value);
        }

        /// <summary>難易度リスト を取得、設定</summary>
        private ObservableSynchronizedCollection<BeatmapDifficulty> beatmapDifficults_;
        /// <summary>難易度リスト を取得、設定</summary>
        public ObservableSynchronizedCollection<BeatmapDifficulty> BeatmapDifficults
        {
            get => this.beatmapDifficults_;

            set => this.SetProperty(ref this.beatmapDifficults_, value);
        }

        /// <summary>選択中の種別 を取得、設定</summary>
        private object selectedCharacteeristic_;
        /// <summary>選択中の種別 を取得、設定</summary>
        public object SelectedCharacteristic
        {
            get => this.selectedCharacteeristic_;

            set => this.SetProperty(ref this.selectedCharacteeristic_, value);
        }

        /// <summary>選択中の難易度表 を取得、設定</summary>
        private object selectedDifficalt_;
        /// <summary>選択中の難易度表 を取得、設定</summary>
        public object SelectedDifficult
        {
            get => this.selectedDifficalt_;

            set => this.SetProperty(ref this.selectedDifficalt_, value);
        }

        /// <summary>選択中の難易度 を取得、設定</summary>
        private BeatmapDifficulty difficulty_;
        /// <summary>選択中の難易度 を取得、設定</summary>
        public BeatmapDifficulty Difficulity
        {
            get => this.difficulty_;

            set => this.SetProperty(ref this.difficulty_, value);
        }

        /// <summary>NPS を取得、設定</summary>
        private string nps_;
        /// <summary>NPS を取得、設定</summary>
        public string NPS
        {
            get => this.nps_;

            set => this.SetProperty(ref this.nps_, value);
        }

        /// <summary>時間 を取得、設定</summary>
        private string time_;
        /// <summary>時間 を取得、設定</summary>
        public string Time
        {
            get => this.time_;

            set => this.SetProperty(ref this.time_, value);
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド用メソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if (args.PropertyName == nameof(this.SelectedCharacteristic) && this.SelectedCharacteristic is BeatmapDifficulty.BeatmapCharacteristic bch) {
                this.BeatmapDifficults.Clear();
                foreach (var item in this.Beatmap.Version.Difficulties.Where(x => x.Characteristic == bch)) {
                    this.BeatmapDifficults.Add(item);
                }
                this.SelectedDifficult = this.BeatmapDifficults[0];
            }
            else
            if (args.PropertyName == nameof(this.SelectedDifficult) && this.SelectedDifficult is BeatmapDifficulty dict) {
                this.Difficulity = dict;
                this.NPS = $"{this.Difficulity.Notes / (double)this.Difficulity.Length:N2}";
            }
        }

        public override void OnInitialize()
        {
            base.OnInitialize();
            this.Characteristics.Clear();
            foreach (var difficulty in this.Beatmap.Version.Difficulties.GroupBy(x => x.Characteristic)) {
                this.Characteristics.Add(difficulty.Key);
            }
            var span = new TimeSpan(0, 0, this.Beatmap.Metadata.Duration);
            this.Time = $"{span}";
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        public SongDetailDialogViewModel()
        {
            this.BeatmapDifficults = new ObservableSynchronizedCollection<BeatmapDifficulty>();
            this.Characteristics = new ObservableSynchronizedCollection<BeatmapDifficulty.BeatmapCharacteristic>();
        }
        #endregion
    }
}

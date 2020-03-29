using BeatSaverSharp;
using BeatServerBrowser.Core.Bases;
using BeatServerBrowser.Core.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using StatefulModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace BeatServerBrowser.Core.ViewModels
{
    public class SongDetailDialogViewModel : ViewModelBase, IDialogAware
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
        private ObservableSynchronizedCollection<BeatmapCharacteristic> BeatmapDifficults_;
        /// <summary>難易度種別 を取得、設定</summary>
        public ObservableSynchronizedCollection<BeatmapCharacteristic> BeatmapDifficults
        {
            get => this.BeatmapDifficults_;

            set => this.SetProperty(ref this.BeatmapDifficults_, value);
        }

        /// <summary>難易度リスト を取得、設定</summary>
        private ObservableSynchronizedCollection<KeyValuePair<string, BeatmapCharacteristicDifficulty?>> diffivults_;
        /// <summary>難易度リスト を取得、設定</summary>
        public ObservableSynchronizedCollection<KeyValuePair<string, BeatmapCharacteristicDifficulty?>> Difficults
        {
            get => this.diffivults_;

            set => this.SetProperty(ref this.diffivults_, value);
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
        private BeatmapCharacteristicDifficulty? difficulty_;
        /// <summary>選択中の難易度 を取得、設定</summary>
        public BeatmapCharacteristicDifficulty? Difficulity
        {
            get => this.difficulty_;

            set => this.SetProperty(ref this.difficulty_, value);
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
            if (args.PropertyName == nameof(this.SelectedCharacteristic) && this.SelectedCharacteristic is BeatmapCharacteristic bch) {
                this.Difficults.Clear();
                foreach (var item in bch.Difficulties.Where(x => x.Value != null)) {
                    this.Difficults.Add(item);
                }
                this.SelectedDifficult = this.Difficults[0];
            }
            else if (args.PropertyName == nameof(this.SelectedDifficult) && this.SelectedDifficult is KeyValuePair<string, BeatmapCharacteristicDifficulty?> dict) {
                this.Difficulity = dict.Value;
            }
        }

        public override void OnInitialize()
        {
            base.OnInitialize();
            this.BeatmapDifficults.Clear();
            foreach (var difficulty in this.Beatmap.Metadata.Characteristics) {
                this.BeatmapDifficults.Add(difficulty);
            }
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        public event Action<IDialogResult> RequestClose;
        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            this.Title = parameters.GetValue<string>("Title");

        }
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
            this.Difficults = new ObservableSynchronizedCollection<KeyValuePair<string, BeatmapCharacteristicDifficulty?>>();
            this.BeatmapDifficults = new ObservableSynchronizedCollection<BeatmapCharacteristic>();
        }
        #endregion
    }
}

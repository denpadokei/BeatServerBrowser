using BeatServerBrowser.Core.Interfaces;
using BeatServerBrowser.Core.Models;
using BeatServerBrowser.Static.Enums;
using NAudio.Vorbis;
using NAudio.Wave;
using NLog;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace BeatServerBrowser.Core.Services
{
    public class SoundPlayerService : BindableBase
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ

        private static SoundPlayerService player_ = new SoundPlayerService();

        public static SoundPlayerService CurrentPlayer => player_;
        private Logger Logger => LogManager.GetCurrentClassLogger();

        /// <summary>再生中の譜面情報 を取得、設定</summary>
        private LocalBeatmapInfo beatmap_;
        /// <summary>再生中の譜面情報 を取得、設定</summary>
        public LocalBeatmapInfo Beatmap
        {
            get => this.beatmap_ ?? new LocalBeatmapInfo();

            set => this.SetProperty(ref this.beatmap_, value);
        }

        /// <summary>再生中かどうか を取得、設定</summary>
        private bool isPreview_;
        /// <summary>再生中かどうか を取得、設定</summary>
        public bool IsPreview
        {
            get => this.isPreview_;

            set => this.SetProperty(ref this.isPreview_, value);
        }

        /// <summary>再生中の音楽ファイル を取得、設定</summary>
        private VorbisWaveReader sounfFile_;
        /// <summary>再生中の音楽ファイル を取得、設定</summary>
        public VorbisWaveReader SoundFile
        {
            get => this.sounfFile_;

            set => this.SetProperty(ref this.sounfFile_, value);
        }

        /// <summary>再生中の音楽ファイル を取得、設定</summary>
        private WaveFileReader soudStream_;
        /// <summary>再生中の音楽ファイル を取得、設定</summary>
        public WaveFileReader SoundStream
        {
            get => this.soudStream_;

            set => this.SetProperty(ref this.soudStream_, value);
        }

        /// <summary>曲の進捗 を取得、設定</summary>
        private double songPosition_;
        /// <summary>曲の進捗 を取得、設定</summary>
        public double SongPosition
        {
            get => this.songPosition_;

            set => this.SetProperty(ref this.songPosition_, value);
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
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        public void Play(FileInfo soundFileInfo, LocalBeatmapInfo info = null)
        {
            try {
                this.Player.Stop();
                this.IsPreview = true;
                this.SoundFile = new VorbisWaveReader(soundFileInfo.FullName);
                this.Player.Init(this.SoundFile);
                this.SetTimer();
                lock (this.lockObject_) {
                    this.Player.Play();
                    this.Beatmap = info;
                }
            }
            catch (Exception e) {
                this.Logger.Error(e);
                this.Beatmap = null;
                this.Player.Stop();
            }
        }

        public void Play(Stream stream, LocalBeatmapInfo info = null)
        {
            try {
                this.Player.Stop();
                this.IsPreview = true;
                this.SoundFile = new VorbisWaveReader(stream);
                this.Player.Init(this.SoundFile);
                this.SetTimer();
                lock (this.lockObject_) {
                    this.Player.Play();
                    this.Beatmap = info;
                }
            }
            catch (Exception e) {
                this.Logger.Error(e);
                this.Beatmap = null;
                this.Player.Stop();
            }
        }

        public void Play(String fileName, LocalBeatmapInfo info = null)
        {
            try {
                this.Player.Stop();
                this.IsPreview = true;
                this.SoundFile = new VorbisWaveReader(fileName);
                this.Player.Init(this.SoundFile);
                this.SetTimer();
                lock (this.lockObject_) {
                    this.Player.Play();
                    this.Beatmap = info;
                }
            }
            catch (Exception e) {
                this.Logger.Error(e);
                this.Beatmap = null;
                this.Player.Stop();
                this.SoundFile?.Dispose();
            }
        }

        public void Stop()
        {
            try {
                this.Player.Stop();
                this.SoundFile?.Dispose();
            }
            catch (Exception e) {
                Debug.WriteLine(e);
            }
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        private void OnConfigPropertyChanged(object sendor, PropertyChangedEventArgs e)
        {
            if (sendor is ConfigMaster && e.PropertyName == nameof(ConfigMaster.Volume)) {
                if (float.TryParse(ConfigMaster.Current.Volume.ToString(), out var floatVol)) {
                    var soundVol = floatVol / 100f;
                    this.Player.Volume = soundVol;
                }
            }
        }

        private void OnPlayBackStopped(object sender, StoppedEventArgs e)
        {
            lock (this.lockObject_) {
                this.timer_.Stop();
                this.IsPreview = false;
                if (this.Player.PlaybackState != PlaybackState.Playing) {
                    this.Beatmap = null;
                }
                Debug.WriteLine($"{this.Player.PlaybackState}:曲が停止しました。");
            }
        }

        private void SetTimer()
        {
            this.timer_.Start();
        }

        private async void RaiseLength(object sender, ElapsedEventArgs e)
        {
            await Task.Run(() =>
            {
                try {
                    this.SongPosition = ((double)this.Player.GetPosition() / (double)this.SoundFile.Length) * 100d;
                    Debug.WriteLine($"{DateTime.Now:yyyy/MM/dd hh:mm:ss} {this.SongPosition}");
                }
                catch (Exception e) {
                    this.Player?.Stop();
                    this.timer_.Stop();
                    this.SoundFile?.Dispose();
                    Debug.WriteLine(e);
                }
            });
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        private readonly WaveOutEvent Player;

        private Timer timer_;

        private readonly object lockObject_ = new object();
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        private SoundPlayerService()
        {
            this.Player = new WaveOutEvent();
            this.Player.PlaybackStopped += this.OnPlayBackStopped;
            WeakEventManager<INotifyPropertyChanged, PropertyChangedEventArgs>.AddHandler(
                ConfigMaster.Current, nameof(INotifyPropertyChanged.PropertyChanged), this.OnConfigPropertyChanged);
            this.timer_ = new Timer(1000);
            this.timer_.Elapsed += this.RaiseLength;
            this.timer_.Enabled = true;
        }
        #endregion
    }
}

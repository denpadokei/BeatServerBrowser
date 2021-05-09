using BeatServerBrowser.Core.Extentions;
using BeatServerBrowser.Core.Models;
using MaterialDesignThemes.Wpf;
using NAudio.Vorbis;
using NAudio.Wave;
using NLog;
using Prism.Mvvm;
using StatefulModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace BeatServerBrowser.Core.Services
{
    public class SoundPlayerService : BindableBase
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ

        private static readonly SoundPlayerService player_ = new SoundPlayerService();

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

        /// <summary>再生する曲のリスト を取得、設定</summary>
        private ObservableSynchronizedCollection<LocalBeatmapInfo> playlist_;
        /// <summary>再生する曲のリスト を取得、設定</summary>
        public ObservableSynchronizedCollection<LocalBeatmapInfo> Playlist
        {
            get => this.playlist_;

            set => this.SetProperty(ref this.playlist_, value);
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

        /// <summary>リピートモード を取得、設定</summary>
        private PackIconKind repeatMode_;
        /// <summary>リピートモード を取得、設定</summary>
        public PackIconKind RepeatMode
        {
            get => this.repeatMode_;

            set => this.SetProperty(ref this.repeatMode_, value);
        }

        /// <summary>シャッフルするかどうか を取得、設定</summary>
        private bool isShuffule_;
        /// <summary>シャッフルするかどうか を取得、設定</summary>
        public bool IsShuffule
        {
            get => this.isShuffule_;

            set => this.SetProperty(ref this.isShuffule_, value);
        }

        /// <summary>曲の進捗 を取得、設定</summary>
        private double songPosition_;
        /// <summary>曲の進捗 を取得、設定</summary>
        public double SongPosition
        {
            get => this.songPosition_;

            set => this.SetProperty(ref this.songPosition_, value);
        }

        /// <summary>再生中の曲のインデックス を取得、設定</summary>
        private int playIndex_;
        /// <summary>再生中の曲のインデックス を取得、設定</summary>
        public int PlayIndex
        {
            get => this.playIndex_;

            set => this.SetProperty(ref this.playIndex_, value);
        }

        /// <summary>スキップで停止したかどうか を取得、設定</summary>
        private bool isSkip_;
        /// <summary>スキップで停止したかどうか を取得、設定</summary>
        public bool IsSkip
        {
            get => this.isSkip_;

            set => this.SetProperty(ref this.isSkip_, value);
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
        /// <summary>
        /// ファイル情報から音楽を再生します
        /// </summary>
        /// <param name="soundFileInfo"></param>
        /// <param name="info"></param>
        /// <param name="playlist"></param>
        public void Play(FileInfo soundFileInfo, LocalBeatmapInfo info = null, IList playlist = null)
        {
            try {
                this.Player.Stop();
                this.IsPreview = true;
                this.SoundFile = new VorbisWaveReader(soundFileInfo.FullName);
                this.Player.Init(this.SoundFile);
                this.SetTimer();
                lock (this.lockObject_) {
                    if (playlist != null) {
                        this.PlayIndex = playlist.IndexOf(info);
                        this.CreatePlaylist(playlist.OfType<LocalBeatmapInfo>().ToList());
                    }
                    else {
                        this.Playlist.Clear();
                        this.Playlist.Add(info);
                    }
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

        /// <summary>
        /// <see cref="Stream"/>から音楽ファイルを再生します。
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="info"></param>
        /// <param name="playlist"></param>
        public void Play(Stream stream, LocalBeatmapInfo info = null, IList playlist = null)
        {
            try {
                this.Player.Stop();
                this.IsPreview = true;
                this.SoundFile = new VorbisWaveReader(stream);
                this.Player.Init(this.SoundFile);
                this.SetTimer();
                lock (this.lockObject_) {
                    if (playlist != null) {
                        this.CreatePlaylist(playlist.OfType<LocalBeatmapInfo>().ToList());
                    }
                    else {
                        this.Playlist.Clear();
                        this.Playlist.Add(info);
                    }
                    this.Beatmap = info;
                    this.Player.Play();
                }
            }
            catch (Exception e) {
                this.Logger.Error(e);
                this.Beatmap = null;
                this.Player.Stop();
            }
        }

        /// <summary>
        /// ファイル名から音楽ファイル再生します。
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="info"></param>
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
                lock (this.lockObject_) {
                    this.Player.Stop();
                    this.SoundFile?.Dispose();
                    this.IsPreview = false;
                }
            }
            catch (Exception e) {
                Debug.WriteLine(e);
            }
        }

        public void SkipBackword()
        {
            if (this.Playlist.Count == 0) {
                return;
            }

            lock (this.lockObject_) {
                this.timer_.Stop();
                this.Player.Stop();
                if (0 <= this.SongPosition && this.SongPosition <= 2) {
                    if (this.PlayIndex == 0) {
                        this.PlayIndex = this.Playlist.Count - 1;
                    }
                    else {
                        this.PlayIndex--;
                    }
                }
                this.SongPosition = 0;
                this.IsSkip = true;
            }
        }

        public void SkipForward()
        {
            if (this.Playlist.Count == 0) {
                return;
            }

            lock (this.lockObject_) {
                this.timer_.Stop();
                this.Player.Stop();
                this.SongPosition = 0;
                if (this.PlayIndex >= this.Playlist.Count - 1) {
                    this.PlayIndex = 0;
                }
                else {
                    this.PlayIndex++;
                }
                this.IsSkip = true;
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
                this.SongPosition = 0;
                if (this.Player.PlaybackState != PlaybackState.Playing) {
                    this.Beatmap = null;
                }
                Debug.WriteLine($"{this.Player.PlaybackState}:曲が停止しました。");
                if (this.IsPreview == false) {
                    return;
                }
                this.PlayNextSong();
            }
        }

        private void SetTimer() => this.timer_.Start();

        private async void RaiseLength(object sender, ElapsedEventArgs e) => await Task.Run(() =>
                                                                           {
                                                                               try {
                                                                                   this.SongPosition = ((double)this.Player.GetPosition() / (double)this.SoundFile.Length) * 100d;
                                                                                   Debug.WriteLine($"{DateTime.Now:yyyy/MM/dd hh:mm:ss} {this.SongPosition}");
                                                                                   //this.Logger.Info($"{this.SongPosition}");
                                                                               }
                                                                               catch (Exception e) {
                                                                                   this.Player?.Stop();
                                                                                   this.timer_.Stop();
                                                                                   this.SoundFile?.Dispose();
                                                                                   Debug.WriteLine(e);
                                                                               }
                                                                           });

        private void CreatePlaylist(IList<LocalBeatmapInfo> list)
        {
            this.Playlist.Clear();
            this.Playlist.Add(list[0]);
            list.RemoveAt(0);
            if (this.IsShuffule) {
                var ramdam = new Random();
                while (list.Count > 0) {
                    var beatmap = list[ramdam.Next(0, list.Count)];
                    this.Playlist.Add(beatmap);
                    list.Remove(beatmap);
                }
            }
            else {
                this.Playlist.AddRange(list);
            }
        }

        private void PlayNextSong()
        {
            if (this.IsSkip) {
                this.IsPreview = true;
                this.Beatmap = this.Playlist[this.PlayIndex];
                this.SoundFile?.Dispose();
                this.SoundFile = new VorbisWaveReader(this.Beatmap.Directory.EnumerateFiles("*.egg", SearchOption.TopDirectoryOnly).FirstOrDefault().FullName);
                this.Player.Init(this.SoundFile);
                this.SetTimer();
                this.Player.Play();
            }
            else {
                switch (this.RepeatMode) {
                    case PackIconKind.RepeatOne:
                        this.Beatmap = this.Playlist[this.PlayIndex];
                        this.IsPreview = true;
                        this.SoundFile?.Dispose();
                        this.SoundFile = new VorbisWaveReader(this.Beatmap.Directory.EnumerateFiles("*.egg", SearchOption.TopDirectoryOnly).FirstOrDefault().FullName);
                        this.Player.Init(this.SoundFile);
                        this.SetTimer();
                        this.IsPreview = true;
                        this.Player.Play();
                        break;
                    case PackIconKind.Repeat:
                        this.PlayIndex++;
                        if (this.Playlist.Count <= this.PlayIndex) {
                            this.PlayIndex = 0;
                        }
                        this.Beatmap = this.Playlist[this.PlayIndex];
                        var file = this.Beatmap.Directory.EnumerateFiles("*.egg", SearchOption.TopDirectoryOnly).FirstOrDefault();
                        this.IsPreview = true;
                        this.SoundFile?.Dispose();
                        this.SoundFile = new VorbisWaveReader(file.FullName);
                        this.Player.Init(this.SoundFile);
                        this.SetTimer();
                        this.Player.Play();
                        break;
                    default:
                        this.PlayIndex++;
                        if (this.Playlist.Count <= this.PlayIndex) {
                            this.PlayIndex = 0;
                            this.IsPreview = false;
                            return;
                        }
                        else {
                            this.Beatmap = this.Playlist[this.PlayIndex];
                            file = this.Beatmap.Directory.EnumerateFiles("*.egg", SearchOption.TopDirectoryOnly).FirstOrDefault();
                            this.IsPreview = true;
                            this.SoundFile?.Dispose();
                            this.SoundFile = new VorbisWaveReader(file.FullName);
                            this.Player.Init(this.SoundFile);
                            this.SetTimer();
                            this.Player.Play();
                        }
                        break;
                }
            }
            this.IsSkip = false;
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        private readonly Timer timer_;

        private readonly WaveOut Player;

        private readonly object lockObject_ = new object();
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        private SoundPlayerService()
        {
            this.Player = new WaveOut();
            this.Playlist = new ObservableSynchronizedCollection<LocalBeatmapInfo>();
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

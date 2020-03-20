using BeatServerBrowser.Core.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BeatServerBrowser.Core.Models
{
    public class SongHashData : IEquatable<SongHashData>
    {
        [JsonIgnore]
        private string _directory;
        [JsonIgnore]
        public string Directory
        {
            get { return _directory; }
            set
            {
                _directory = value?.TrimEnd('\\', '/');
            }
        }
        [JsonProperty("directoryHash")]
        public long DirectoryHash { get; set; }
        [JsonProperty("songHash")]
        public string SongHash { get; set; }

        public SongHashData() { }

        /// <summary>
        /// Creates a new SongHashData for a song directory.
        /// </summary>
        /// <param name="directory"></param>
        /// <exception cref="ArgumentNullException">Thrown when the given directory string is null or empty.</exception>
        /// <exception cref="System.Security.SecurityException">Thrown from Path.GetFullPath().</exception>
        /// <exception cref="ArgumentException">Thrown from Path.GetFullPath().</exception>
        /// <exception cref="NotSupportedException">Thrown from Path.GetFullPath().</exception>
        /// <exception cref="PathTooLongException">Thrown from Path.GetFullPath().</exception>
        public SongHashData(string directory)
        {
            if (string.IsNullOrEmpty(directory))
                throw new ArgumentNullException(nameof(directory), "directory cannot be null or empty for SongHashData's constructor.");
            Directory = Path.GetFullPath(directory.TrimEnd('/', '\\'));
        }

        /// <summary>
        /// Creates a new SongHashData for a song directory and gets the song and directory hash from the provided JProperty.
        /// </summary>
        /// <param name="directory"></param>
        /// <exception cref="ArgumentNullException">Thrown when the given directory string is null or empty.</exception>
        /// <exception cref="System.Security.SecurityException">Thrown from Path.GetFullPath().</exception>
        /// <exception cref="ArgumentException">Thrown from Path.GetFullPath().</exception>
        /// <exception cref="NotSupportedException">Thrown from Path.GetFullPath().</exception>
        /// <exception cref="PathTooLongException">Thrown from Path.GetFullPath().</exception>
        public SongHashData(JProperty token, string directory)
            : this(directory)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token), "JProperty token cannot be null when included in SongHashData's constructor.");
            token.Value.Populate(this);
        }

        public string GenerateHash()
        {
            SongHash = SongHashDataProviderService.GenerateHash(Directory, SongHash);
            return SongHash;
        }

        public void GenerateDirectoryHash()
        {
            DirectoryHash = SongHashDataProviderService.GenerateDirectoryHash(Directory);
        }

        /// <summary>
        /// Returns true if the folder path matches. Case sensitive
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(SongHashData other)
        {
            if (other == null)
                return false;
            return Directory.Equals(other.Directory, StringComparison.CurrentCulture);
        }
    }

    public static class JsonExtensions
    {
        public static void Populate<T>(this JToken value, T target) where T : class
        {
            using (var sr = value.CreateReader()) {
                JsonSerializer.CreateDefault().Populate(sr, target); // Uses the system default JsonSerializerSettings
            }
        }
    }
}

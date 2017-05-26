using System.Collections.Generic;
using SC.SimplSharp.Config;
using SCConfigSplus.JSON;
using SCConfigSplus.Readers;
using SCSplusConfig.Delegates;
using SSMono.IO;

namespace SCSplusConfig
{
    public class SourceConfigurationReader
    {
        private string _sectionName;
        private string _fileName;

        private FileSystemWatcher _watcher;

        public SourcesConfigChangedDel OnConfigurationChanged { get; set; }

        public void Initialize(string path, string sectionName)
        {
            _fileName = path;
            _sectionName = sectionName;

            _watcher = new FileSystemWatcher();

            ConfigureFileSystemWatcher(path);
        }

        public void ReadSettings()
        {
            var reader = new JsonSettingsReader(_fileName);

            var sources = reader.LoadSection<List<Source>>(_sectionName);

            FireOnConfigChangedEvent(sources);
        }

        private void ConfigureFileSystemWatcher(string path)
        {
            var directory = Path.GetDirectoryName(path);
            var fileName = Path.GetFileName(path);

            _watcher = new FileSystemWatcher
            {
                Path = directory,
                NotifyFilter = NotifyFilters.LastWrite,
                Filter = fileName
            };

            _watcher.Changed += OnWatcherChanged;

            _watcher.EnableRaisingEvents = true;
        }

        private void OnWatcherChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }

            var reader = new JsonSettingsReader(_fileName);

            var sources = reader.LoadSection<List<Source>>(_sectionName);

            FireOnConfigChangedEvent(sources);
        }

        private void FireOnConfigChangedEvent(IList<Source> settings)
        {
            var handler = OnConfigurationChanged;

            if (OnConfigurationChanged == null)
            {
                return;
            }

            for (ushort i = 0; i < settings.Count; i++)
            {
                handler(i, settings[i]);
            }
        }
    }
}
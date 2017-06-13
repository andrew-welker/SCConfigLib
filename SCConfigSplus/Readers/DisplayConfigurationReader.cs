using System.Collections.Generic;
using SCConfigLib.Readers;
using SCConfigSPlus.Delegates;
using SCConfigSPlus.JSON;
using SSMono.IO;

namespace SCConfigSPlus
{
    public class DisplayConfigurationReader
    {
        private string _sectionName;
        private string _fileName;

        private FileSystemWatcher _watcher;

        /// <summary>
        /// Delegate to update SIMPL Configuration
        /// </summary>
        public DisplaysConfigChangedDel OnConfigurationChanged { get; set; }

        /// <summary>
        /// Method to intialize the reader
        /// </summary>
        /// <param name="path">File to save settings to</param>
        /// <param name="sectionName">Section name to save settings in</param>
        public void Initialize(string path, string sectionName)
        {
            _fileName = path;
            _sectionName = sectionName;

            _watcher = new FileSystemWatcher();

            _watcher.ConfigureWatcher(path);

            _watcher.Changed += OnWatcherChanged;

            _watcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Method to read the Source Settings
        /// </summary>
        public void ReadSettings()
        {
            var reader = new JsonSettingsReader(_fileName);

            var sources = reader.LoadSection<List<Display>>(_sectionName);

            FireOnConfigChangedEvent(sources);
        }

        /// <summary>
        /// Updates the stored configuration when the FileSystemWatcher determines that the file has changed.
        /// </summary>
        /// <param name="sender">Watcher that fired the event</param>
        /// <param name="e">Arguments</param>
        private void OnWatcherChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }

            ReadSettings();
        }

        /// <summary>
        /// Fires the event for S+ to send the data to the rest of the program.
        /// </summary>
        /// <param name="settings">Object to send to S+</param>
        private void FireOnConfigChangedEvent(IList<Display> settings)
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
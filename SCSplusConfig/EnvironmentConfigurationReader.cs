using SCConfigSplus.JSON;
using SCConfigSplus.Readers;
using SCSplusConfig.Delegates;
using SSMono.IO;

namespace SCSplusConfig
{
    

    public class EnvironmentConfigurationReader
    {
        private string _filePath;
        private FileSystemWatcher _watcher;

        /// <summary>
        /// Event to update configuration when the file changes.
        /// </summary>
        public event EnvControlsConfigChangedDel OnConfigurationChanged;


        /// <summary>
        /// Method to initialize the system.
        /// </summary>
        /// <param name="path">Path to file to be read</param>
        public void Initialize(string path)
        {
            _filePath = path;

            _watcher = new FileSystemWatcher();

            ConfigureFileSystemWatcher(path);
        }

        /// <summary>
        /// Method to read the EnvironmentControls settings
        /// </summary>
        /// <returns>EnvironmentControls indicating the stored settings</returns>
        public void ReadSettings()
        {
            var reader = new JsonSettingsReader(_filePath);
            var settings = reader.LoadSection<EnvironmentControls>();

            FireOnConfigChangedEvent(settings);
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

            var reader = new JsonSettingsReader(_filePath);

            var settings = reader.LoadSection<EnvironmentControls>();

            FireOnConfigChangedEvent(settings);
        }


        /// <summary>
        /// Fires the event for S+ to send the data to the rest of the program.
        /// </summary>
        /// <param name="settings">Object to send to S+</param>
        private void FireOnConfigChangedEvent(EnvironmentControls settings)
        {
            var onFileChangedHandler = OnConfigurationChanged;

            if (onFileChangedHandler != null)
            {
                onFileChangedHandler(this, settings);
            }
        }
    }
}

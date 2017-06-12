using SSMono.IO;

namespace SCConfigSPlus
{
    public static class WatcherExtensions
    {
        /// <summary>
        /// Configures the specified watcher
        /// </summary>
        /// <param name="watcher">FileSystemWatcher to configure</param>
        /// <param name="path">File to watch</param>
        public static void ConfigureWatcher(this FileSystemWatcher watcher, string path)
        {
            var directory = Path.GetDirectoryName(path);
            var fileName = Path.GetFileName(path);

            watcher.Path = directory;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = fileName;
        }
    }
}
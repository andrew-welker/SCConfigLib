using System.Linq;
using SCConfigLib.Writers;
using SCConfigSplus.JSON;

namespace SCConfigSPlus
{
    public class SourceConfigurationWriter
    {
        private string _sectionName;
        private string _filePath;

        /// <summary>
        /// Method to initialize the writer.
        /// </summary>
        /// <param name="filePath">File to save settings to</param>
        /// <param name="sectionName">Section to write settings to</param>
        public void Initialize(string filePath, string sectionName)
        {
            _filePath = filePath;
            _sectionName = sectionName;
        }

        /// <summary>
        /// Method to save settings to specified file
        /// </summary>
        /// <param name="sources">Object To save</param>
        public void SaveSettings(Source[] sources)
        {
            //Have to skip the first 2 elements because S+ is stupid.
            var sourcesList = sources.Skip(2).ToList();
            var writer = new JsonSettingsWriter(_filePath);

            writer.SaveSection(sourcesList, _sectionName);
        }
    }
}
using System;
using SCConfigLib.Writers;

namespace SCConfigSPlus
{
    public abstract class ConfigurationWriterBase
    {
        private string _fileName;
        private string _sectionName;

        /// <summary>
        /// Method to initialize the writer.
        /// </summary>
        /// <param name="path">File to save settings to</param>
        public void Initialize(string path)
        {
            _fileName = path;
            _sectionName = String.Empty;
        }

        /// <summary>
        /// Method to initialize the writer with a section name
        /// </summary>
        /// <param name="path">File to save</param>
        /// <param name="sectionName">Section to write</param>
        public void InitializeWithSectionName(string path, string sectionName)
        {
            _fileName = path;
            _sectionName = sectionName;
        }

        /// <summary>
        /// Method to save settings to specified file
        /// </summary>
        /// <param name="settings">Object to save</param>
        protected void SaveSettings<T>(T settings) where T : class, new()
        {
            var writer = new JsonSettingsWriter(_fileName);

            if (String.IsNullOrEmpty(_sectionName))
            {
                writer.SaveSection(settings);
            }
            else
            {
                writer.SaveSection(settings, _sectionName);
            }
        }
    }
}
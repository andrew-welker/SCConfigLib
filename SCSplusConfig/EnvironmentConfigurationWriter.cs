using System;
using System.Linq;
using Crestron.SimplSharp;
using SC.SimplSharp.Config;
using SCConfigSplus.JSON;
using SCSplusConfig.Writers;

namespace SCSplusConfig
{
    /// <summary>
    /// Class to write 
    /// </summary>
    public class EnvironmentConfigurationWriter
    {
        private string _fileName;

        /// <summary>
        /// Method to initialize the writer.
        /// </summary>
        /// <param name="path">File to save settings to.</param>
        public void Initialize(string path)
        {
            _fileName = path;
        }

        /// <summary>
        /// Method to save settings to specified file
        /// </summary>
        /// <param name="settings">Object to save</param>
        public void SaveSettings(EnvironmentControls settings)
        {
            var writer = new JsonSettingsWriter(_fileName);

            writer.SaveSection(settings);
        }
    }
}
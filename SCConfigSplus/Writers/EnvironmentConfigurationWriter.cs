using SCConfigSPlus.JSON;

namespace SCConfigSPlus
{
    /// <summary>
    /// Class to write environment configurations
    /// </summary>
    public class EnvironmentConfigurationWriter:ConfigurationWriterBase
    {
        /// <summary>
        /// Method to save settings to specified file
        /// </summary>
        /// <param name="settings">Object to save</param>
        public void SaveSettings(EnvironmentControls settings)
        {
            base.SaveSettings(settings);
        }
    }
}
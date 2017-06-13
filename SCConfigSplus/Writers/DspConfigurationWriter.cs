using SCConfigSPlus.JSON;

namespace SCConfigSPlus
{
    /// <summary>
    /// Class to write Dsp configurations
    /// </summary>
    public class DspConfigurationWriter : ConfigurationWriterBase
    {
        /// <summary>
        /// Method to save settings to specified file
        /// </summary>
        /// <param name="settings">Object to save</param>
        public void SaveSettings(Dsp settings)
        {
            base.SaveSettings(settings);
        }
    }
}
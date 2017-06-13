using System.Linq;
using SCConfigSPlus.JSON;

namespace SCConfigSPlus
{
    public class SourceConfigurationWriter:ConfigurationWriterBase
    {
        /// <summary>
        /// Method to save settings to specified file
        /// </summary>
        /// <param name="sources">Object To save</param>
        public void SaveSettings(Source[] sources)
        {
            //Have to skip the first 2 elements because S+ is stupid.
            var sourcesList = sources.Skip(2).ToList();

            SaveSettings(sourcesList);
        }
    }
}
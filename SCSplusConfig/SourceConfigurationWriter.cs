using System.Linq;
using SCConfigSplus.JSON;
using SCSplusConfig.Writers;

namespace SCSplusConfig
{
    public class SourceConfigurationWriter
    {
        private string _sectionName;
        private string _filePath;

        public void Initialize(string filePath, string sectionName)
        {
            _filePath = filePath;
            _sectionName = sectionName;
        }

        public void SaveSettings(Source[] sources)
        {
            //Have to skip the first 2 elements because S+ is stupid.
            var sourcesList = sources.Skip(2).ToList();
            var writer = new JsonSettingsWriter(_filePath);

            writer.SaveSection(sourcesList, _sectionName);
        }
    }
}
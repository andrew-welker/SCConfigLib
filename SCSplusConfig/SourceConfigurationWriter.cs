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

        /// <summary>
        /// Get the current source count. Needed since S+ doesn't have an easy way to keep track of the length of an array.
        /// </summary>
        /// <param name="sources">Array to count</param>
        /// <returns>Length of array as a string</returns>
        public ushort GetCurrentSourceCount(Source[] sources)
        {
            // Have to subtract 2 because S+ arrays are 1-based...
            // And I can't resize the array to 0 elements, so what corresponds to element 1 is actually element 3. 
            //BUT they still have a index 0 element, EVEN THOUGH that's not accessible from S+.
            var count = sources.Count() - 2;

            return (ushort) count;
        }

        public void RemoveSource(ushort index, ref Source[] sources)
        {
            var list = sources.ToList();

            list.RemoveAt(index + 1);

            sources = list.ToArray();
        }
    }
}
using System;
using System.Text;
using Crestron.SimplSharp.CrestronIO;
using Newtonsoft.Json.Linq;
using SC.SimplSharp.Config;

namespace SCSplusConfig.Writers
{
    /// <summary>
    /// Class to write settings to a file in a JSON format. Implements ISettingsWriter
    /// </summary>
    public class JsonSettingsWriter : ISettingsWriter
    {
        private readonly string _settingsFilePath;

        /// <summary>
        /// Initializes the writer and sets the path to the file to write
        /// </summary>
        /// <param name="path">File path to the file to write</param>
        public JsonSettingsWriter(string path)
        {
            _settingsFilePath = path;
        }

        /// <summary>
        /// Save the entire configuration to the specified file
        /// </summary>
        /// <typeparam name="T">Type to save. Must be a class and have a public default constructor</typeparam>
        /// <param name="settings">Object to save</param>
        public void Save<T>(T settings) where T : class, new()
        {
            Save(typeof (T), settings);
        }

        /// <summary>
        /// Save a section of the configuration to the specified file
        /// </summary>
        /// <typeparam name="T">Type to save. Must be a class and have a public default constructor</typeparam>
        /// <param name="settings">Object to save</param>
        public void SaveSection<T>(T settings) where T : class, new()
        {
            SaveSection(typeof (T), settings);
        }

        /// <summary>
        /// Save a section of the configuration to the specified file
        /// </summary>
        /// <typeparam name="T">Type to save. Must be a class and have a public default constructor</typeparam>
        /// <param name="settings">Object to save</param>
        /// <param name="sectionName">Section name for JSON file</param>
        public void SaveSection<T>(T settings, string sectionName) where T : class, new()
        {
            SaveSection(typeof (T), settings, sectionName);
        }

        /// <summary>
        /// Save the specified object to the file.
        /// </summary>
        /// <param name="type">Type of the object to save.</param>
        /// <param name="settings">Object to save</param>
        public void Save(Type type, object settings)
        {
            if (!File.Exists(_settingsFilePath))
            {
                File.Create(_settingsFilePath);
            }
            var json = JObject.FromObject(settings);

            WriteToFile(json);
        }

        /// <summary>
        /// Save the specified object to the file. If the file doesn't exist or is empty, the file will be created at the specified path. 
        /// </summary>
        /// <param name="type">Type of the object to save</param>
        /// <param name="settings">Object to save</param>
        public void SaveSection(Type type, object settings)
        {
            SaveSection(type, settings, type.Name);
        }

        /// <summary>
        /// Save the specified object to the specfied section of the file
        /// </summary>
        /// <param name="type">Object type</param>
        /// <param name="settings">Object to save</param>
        /// <param name="sectionName">JSON section name</param>
        public void SaveSection(Type type, object settings, string sectionName)
        {
            if (!File.Exists(_settingsFilePath) || new FileInfo(_settingsFilePath).Length == 0)
            {
                File.Create(_settingsFilePath);

                var jo = GetNewJson(settings, sectionName);

                WriteToFile(jo);
                return;
            }

            var settingsData = UpdateExistingJson(settings, sectionName);

            WriteToFile(settingsData);
        }

        /// <summary>
        /// Updates the JSON in the file with the new object's information.
        /// </summary>
        /// <param name="settings">Object to save</param>
        /// <param name="section">Section name to update or add</param>
        /// <returns>JSON version of the object to save</returns>
        private JObject UpdateExistingJson(object settings, string section)
        {
            var jsonSection = JToken.FromObject(settings);

            string existingJson;

            using (var stream = new StreamReader(_settingsFilePath))
            {
                existingJson = stream.ReadToEnd();
            }

            var settingsData = JObject.Parse(existingJson);

            if (settingsData[section] == null)
            {
                settingsData.Add(section, jsonSection);
            }
            else
            {
                settingsData[section] = jsonSection;
            }
            return settingsData;
        }

        /// <summary>
        /// Get a new JSON Object from the specified object
        /// </summary>
        /// <param name="settings">Object to convert</param>
        /// <param name="sectionName"></param>
        /// <returns>JSON version of the object to save</returns>
        private JObject GetNewJson(object settings, string sectionName)
        {
            var jo = new JObject {{sectionName, JToken.FromObject(settings)}};

            return jo;
        }

        /// <summary>
        /// Write the object to the specified file. If another object is using the file, blocks until it is available.
        /// </summary>
        /// <param name="jo">Object to write to file</param>
        private void WriteToFile(JObject jo)
        {
            try
            {
                FileOperations.FileCriticalSection.Enter();
                using (var stream = File.OpenWrite(_settingsFilePath))
                {
                    stream.Write(jo.ToString(), Encoding.Default);
                }
            }
            finally
            {
                FileOperations.FileCriticalSection.Leave();
            }
        }
    }
}
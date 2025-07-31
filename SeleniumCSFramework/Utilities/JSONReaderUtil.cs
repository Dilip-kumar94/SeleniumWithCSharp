using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCSFramework.Utilities
{
    public class JSONReaderUtil
    {
        private JToken jsonData;
        private string filePath;
        public JSONReaderUtil(string filePath)
        {
            this.filePath = filePath;
            LoadJsonData(filePath);
        }

        private void LoadJsonData(string filePath)
        {
            try
            {
                string jsonContent = File.ReadAllText(filePath);
                jsonData = JToken.Parse(jsonContent);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading JSON file: {ex.Message}");
            }
        }
        public String GetValue(string key)
        {
            try
            {
                var value = jsonData.SelectToken(key);
                if (value != null)
                {
                    return value.ToString();
                }
                else
                {
                    throw new Exception($"Key '{key}' not found in JSON data.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving value for key '{key}': {ex.Message}");
            }
        }
    }
}

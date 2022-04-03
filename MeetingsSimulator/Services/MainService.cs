using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MeetingsSimulator.Services
{
    public class MainService
    {
        public void Create<T>(T model, string path)
        {
            List<T> modelArr = Get<T>(path);
            modelArr.Add(model);
            Save(modelArr, path);
        }

        public List<T> Get<T>(string path)
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                if(string.IsNullOrEmpty(json))
                    return new List<T>();

                List<T> modelArr = JsonConvert.DeserializeObject<List<T>>(json);
                return modelArr;
            }
            return new List<T>();
        }

        public void Delete<T>(int objIndex, string path)
        {
            if (objIndex != -1)
            {
                List<T> modelArr = Get<T>(path);
                modelArr.RemoveAt(objIndex);
                Save(modelArr, path);
            }
        }
        private void Save<T>(List<T> modelArr, string path)
        {
            string newJson = JsonConvert.SerializeObject(modelArr);
            File.AppendAllText(path, newJson);
        }

        public void Clean()
        {
            File.WriteAllText(@"meetings.json", string.Empty);
        }
    }
}

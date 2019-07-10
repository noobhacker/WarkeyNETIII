using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Warkey.Infrastructure
{
    public class ClientSettings
    {
        private readonly string _filename;
        public ClientSettings(string filename)
        {
            _filename = filename;
        }

        public async Task<T> GetAsync<T>()
        {
            if (File.Exists(_filename))
            {
                var sr = new StreamReader(_filename);
                var json = await sr.ReadToEndAsync();
                sr.Dispose();

                var result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }

            return default;
        }

        public async Task SaveAsync<T>(T saveModel)
        {
            if (File.Exists(_filename))
            {
                File.Delete(_filename);
            }

            var json = JsonConvert.SerializeObject(saveModel);
            using(var streamWriter = new StreamWriter(_filename))
            {
                await streamWriter.WriteAsync(json);
            }           
        }
    }
}

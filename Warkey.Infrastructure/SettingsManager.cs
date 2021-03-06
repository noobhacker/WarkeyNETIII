﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Warkey.Infrastructure
{
    public class SettingsManager
    {
        private readonly string _filename;
        public SettingsManager(string filename)
        {
            _filename = filename;
        }

        public async Task<T> GetAsync<T>()
            where T : new()
        {
            if (File.Exists(_filename))
            {
                var streamReader = new StreamReader(_filename);
                var json = await streamReader.ReadToEndAsync();
                streamReader.Dispose();

                try
                {
                    var result = JsonConvert.DeserializeObject<T>(json);
                    return result;
                }
                catch
                {
                    return new T();
                }
            }

            return new T();
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

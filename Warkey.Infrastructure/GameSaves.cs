using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warkey.Infrastructure
{
    public class GameSaves
    {
        private const string _tkokFolderName = "TKoK_Save_Files";
        public async Task<List<TkokSave>> GetTkokSaveFilesAsync(int saveCount)
        {
            if (IsLoadFunctionAvailable())
            {
                var result = await LoadTkokSaveFilesAsync(5);
                return result;
            }

            return null;
        }

        private async Task<List<TkokSave>> LoadTkokSaveFilesAsync(int saveCount)
        {
            var path = Directory.GetCurrentDirectory() + @"\" + _tkokFolderName;
            var directoryList = Directory.GetDirectories(path);
            var result = new List<TkokSave>();

            int count = 0;

            // folders but not search file .txt is for game version, sort by latest version
            foreach (var folder in directoryList.OrderByDescending(x => x))
            {
                // create file list with last modified then orderby last modified
                var fileList = Directory.GetFiles(folder);
                var fileObject = new List<FileModel>();
                foreach (var file in fileList)
                {
                    var lastModified = File.GetLastWriteTime(file);
                    fileObject.Add(new FileModel()
                    {
                        Path = file,
                        LastModified = lastModified
                    });
                }

                foreach (var file in fileObject.OrderByDescending(x => x.LastModified))
                {
                    if (count == saveCount)
                        break;

                    var sr = new StreamReader(file.Path);

                    var text = await sr.ReadToEndAsync();
                    var newLine = new string[] { "\r\n\n" };
                    var lines = text.Split(newLine, StringSplitOptions.None);
                    var saveItem = new TkokSave()
                    {
                        LastModified = file.LastModified,
                        Version = new DirectoryInfo(folder).Name,
                    };

                    foreach (var item in lines)
                    {
                        // character length + " :" (length of 2)
                        // password ":\r\n" (length of 3) 
                        if (item.StartsWith("Name"))
                            saveItem.Name = item.Substring(6);
                        else if (item.StartsWith("Class"))
                            saveItem.Class = item.Substring(7);
                        else if (item.StartsWith("Gold"))
                            saveItem.Gold = int.Parse(item.Substring(6));
                        else if (item.StartsWith("Level"))
                            saveItem.Level = int.Parse(item.Substring(7));
                        else if (item.StartsWith("Password"))
                            saveItem.Password = item.Substring(11);
                        else if (item.StartsWith("Exp"))
                            saveItem.Exp = int.Parse(item.Substring(5));
                    }

                    sr.Dispose();
                    result.Add(saveItem);
                    count += 1;
                }

            }

            result = result.OrderByDescending(x => x.LastModified)
                .ToList();

            return result;
        }

        public bool IsLoadFunctionAvailable()
        {
            return Directory.Exists(_tkokFolderName);
        }

        public class FileModel
        {
            public string Path { get; set; }
            public DateTime LastModified { get; set; }
        }

        public class TkokSave
        {
            public string Version { get; set; }
            public DateTime LastModified { get; set; }
            public string Name { get; set; }
            public int Level { get; set; }
            public string Class { get; set; }
            public int Gold { get; set; }
            public string Password { get; set; }
            public int Exp { get; set; }
        }
    }
}

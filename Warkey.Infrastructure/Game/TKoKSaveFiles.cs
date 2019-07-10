using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warkey.Shared;

namespace Warkey.Infrastructure.External
{
    public class TKoKSaveFiles
    {
        private const string _tkokFolderName = "TKoK_Save_Files";
        public async Task<List<TkokSave>> GetAsync(int saveCount)
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
                var fileObject = new List<FileItem>();
                foreach (var file in fileList)
                {
                    var lastModified = File.GetLastWriteTime(file);
                    fileObject.Add(new FileItem()
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

        public class FileItem
        {
            public string Path { get; set; }
            public DateTime LastModified { get; set; }
        }
    }
}

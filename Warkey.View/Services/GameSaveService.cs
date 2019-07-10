using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warkey.Core.Items;
using Warkey.Core.Items.Saves;
using Warkey.Core.ViewModels;

namespace Warkey.Core.Services
{
    public static class GameSaveService
    {
        public const string TkokFolderName = "TKoK_Save_Files";

        public static async void InitializeAsync()
        {
            if (IsLoadFunctionAvailable())
            {
                var result = await LoadTkokSaveFilesAsync(5);
                foreach (var item in result)
                    MainWindow.LoadGameViewModel.Saves.Add(item);
            }
        }

        public static async Task<List<TkokSaveItem>> LoadTkokSaveFilesAsync(int saveCount)
        {
            var path = Directory.GetCurrentDirectory() + @"\" + TkokFolderName;
            var directoryList = Directory.GetDirectories(path);
            var result = new List<TkokSaveItem>();

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
                        LastModified=lastModified
                    });
                }

                foreach (var file in fileObject.OrderByDescending(x=>x.LastModified))
                {
                    if (count == saveCount)
                        break;

                    var sr = new StreamReader(file.Path);

                    var text = await sr.ReadToEndAsync();
                    var newLine = new string[] { "\r\n\n" };
                    var lines = text.Split(newLine, StringSplitOptions.None);
                    var saveItem = new TkokSaveItem()
                    {
                        LastModified = file.LastModified,
                        Version = new DirectoryInfo(folder).Name,
                    };

                    foreach(var item in lines)
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
                        else if(item.StartsWith("Exp"))
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

        public static bool IsLoadFunctionAvailable()
        {
            return Directory.Exists(TkokFolderName);
        }
        
    }
}

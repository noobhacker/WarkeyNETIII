using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarkeyNETIII.Items.Saves;
using WarkeyNETIII.ViewModels;

namespace WarkeyNETIII.Services
{
    public static class SaveFileService
    {
        public const string TkokFolderName = "TKoK_Save_Files";

        public static async void InitializeAsync()
        {
            var result = await LoadTkokSaveFilesAsync(5);
            foreach (var item in result)
                MainWindow.LoadGameViewModel.Saves.Add(item);
        }

        public static async Task<List<TkokSaveItem>> LoadTkokSaveFilesAsync(int saveCount)
        {
            var path = Directory.GetCurrentDirectory() + @"\" + TkokFolderName;
            var directoryList = Directory.GetDirectories(path);
            var result = new List<TkokSaveItem>();

            int count = 0;
            foreach (var folder in directoryList.OrderByDescending(x => x))
            {
                var fileList = Directory.GetFiles(folder);
                foreach (var file in fileList)
                {
                    if (count == saveCount)
                        break;

                    var dateTime = File.GetLastWriteTime(file);
                    var sr = new StreamReader(file);

                    var text = await sr.ReadToEndAsync();
                    var newLine = new string[] { "\r\n\n" };
                    var lines = text.Split(newLine, StringSplitOptions.None);
                    var saveItem = new TkokSaveItem()
                    {
                        DateTime = dateTime,
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
                    }

                    sr.Dispose();
                    result.Add(saveItem);
                    count += 1;
                }

            }

            result = result.OrderByDescending(x => x.DateTime)
                .ToList();

            return result;
        }

        public static bool IsLoadFunctionAvailable()
            => isTkokFolderExist(); // && conditions here for more games

        private static bool isTkokFolderExist()
            => Directory.Exists(TkokFolderName);

    }
}

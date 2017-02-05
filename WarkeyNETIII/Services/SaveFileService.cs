using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarkeyNETIII.Items.Saves;

namespace WarkeyNETIII.Services
{
    public static class SaveFileService
    {
        public const string TkokFolderName = "TKoK_Save_Files";
        const int SAVECOUNT = 5;

        public static async Task<List<TkokSaveItem>> LoadTkokSaveFiles()
        {
            var path = Directory.GetCurrentDirectory() + @"\" + TkokFolderName;
            var directoryList = Directory.GetDirectories(path);
            var result = new List<TkokSaveItem>();

            int count = 0;
            foreach (var folder in directoryList)
            {
                var fileList = Directory.GetFiles(folder);
                foreach (var file in fileList)
                {
                    if (count == SAVECOUNT)
                        break;

                    var dateTime = File.GetLastWriteTime(file);

                    using (var sr = new StreamReader(file))
                    {
                        var text = await sr.ReadToEndAsync();

                    }

                    count += 1;
                }

            }

            result = result.OrderByDescending(x => x.DateTime)
                .ToList();

            return result;
        }

        public static bool IsLoadFunctionAvailable()
            => IsTkokFolderExist(); // && conditions here for more games

        public static bool IsTkokFolderExist()
            => isFolderExist(TkokFolderName);

        private static string getValueFromTkok(string value)
            => value.Substring(value.IndexOf(": "));

        private static bool isFolderExist(string folderName)
            => Directory.Exists(folderName);
    }
}

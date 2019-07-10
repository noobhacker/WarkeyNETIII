using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Warkey.Core.Items;

namespace Warkey.Core.Services
{
    public static class RegistryService
    {
        public static void WriteResolution(int width, int height)
        {
            writeKey("reswidth", width);
            writeKey("resheight", height);
        }

        public static ScreenResolutionItem ReadResolution()
        {
            try
            {
                var width = (int)readKey("reswidth");
                var height = (int)readKey("resheight");
                return new ScreenResolutionItem()
                {
                    Width = width,
                    Height = height
                };
            }
            catch
            {
                MessageBox.Show("Please start Warcraft III once for optimal game resolution.");
                return new ScreenResolutionItem()
                {
                    Width = 0,
                    Height = 0
                };
            }
        }

        public static int ReadLockFb()
            => (int)readKey("lockfb");

        public static void WriteLockFb()
            => writeKey("lockfb", 0);

        // below here can move to warcraft III registry infrastructure

        static RegistryKey baseKey = Registry.CurrentUser;
        static string KeyPath = @"SOFTWARE\Blizzard Entertainment\Warcraft III\Video";
        private static void writeKey(string keyName, object value)
        {
            var key = baseKey.CreateSubKey(KeyPath);
            key.SetValue(keyName, value);
        }

        private static object readKey(string keyName)
        {
            var key = baseKey.OpenSubKey(KeyPath);
            if (key == null)
                return null;
            else
                return key.GetValue(keyName);
        }

    }
}

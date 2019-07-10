using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarkeyNETIII.Infrastructure.Game
{
    public class GameSettings
    {
        private readonly RegistryKey _baseKey = Registry.CurrentUser;
        private const string _videoKeyPath = @"SOFTWARE\Blizzard Entertainment\Warcraft III\Video";

        public void WriteVideoSettings(string parameter, object value)
        {
            var key = _baseKey.CreateSubKey(_videoKeyPath);
            key.SetValue(parameter, value);
        }

        public object ReadVideoSettings(string parameter)
        {
            var key = _baseKey.OpenSubKey(_videoKeyPath);
            if (key == null)
            {
                return null;
            }
            else
            {
                return key.GetValue(parameter);
            }
        }
    }
}

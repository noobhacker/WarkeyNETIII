using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Warkey.Core.Items;
using Warkey.Infrastructure;

namespace Warkey.Core.Services
{
    public class GameVideoSettingsService
    {
        private readonly GameRegistry _gameRegistry = new GameRegistry();
        public void WriteResolution(int width, int height)
        {
            _gameRegistry.WriteVideoSettings("reswidth", width);
            _gameRegistry.WriteVideoSettings("resheight", height);
        }

        public ScreenResolutionItem ReadResolution()
        {
            try
            {
                var width = (int)_gameRegistry.ReadVideoSettings("reswidth");
                var height = (int)_gameRegistry.ReadVideoSettings("resheight");
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

        public int ReadLockFb()
        {
            return (int)_gameRegistry.ReadVideoSettings("lockfb");
        }

        public void WriteLockFb()
        { 
            _gameRegistry.WriteVideoSettings("lockfb", 0);
        }

    }
}

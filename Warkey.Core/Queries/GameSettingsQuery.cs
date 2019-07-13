using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warkey.Infrastructure;
using static Warkey.Infrastructure.Monitor;

namespace Warkey.Core.Queries
{
    public class GameSettingsQuery
    {
        private readonly GameSettings _gameSettings;
        private readonly Monitor _monitor;

        public GameSettingsQuery()
        {
            _gameSettings = new GameSettings(); 
            _monitor = new Monitor();
        }

        public ScreenResolution GetScreenResolution()
        {
            return _monitor.GetCurrentResolution();
        }

        public ScreenResolution GetGameResolution()
        {
            try
            {
                var width = (int)_gameSettings.ReadVideoSettings("reswidth");
                var height = (int)_gameSettings.ReadVideoSettings("resheight");
                return new ScreenResolution()
                {
                    Width = width,
                    Height = height
                };
            }
            catch
            {
                // handle please run warcraft once message on frontend
                return null;
            }
        }

        public bool GetLockFb()
        {
            var query = _gameSettings.ReadVideoSettings("lockfb");
            return Convert.ToBoolean(query);
        }
    }
}

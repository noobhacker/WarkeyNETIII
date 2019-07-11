using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warkey.Infrastructure;

namespace Warkey.Core.Commands
{
    public class GameSettingsCommand
    {
        private readonly GameSettings _gameSettings;

        public GameSettingsCommand()
        {
            _gameSettings = new GameSettings();
        }

        public void WriteResolution(int width, int height)
        {
            _gameSettings.WriteVideoSettings("reswidth", width);
            _gameSettings.WriteVideoSettings("resheight", height);
        }

        public void WriteLockFb()
        {
            _gameSettings.WriteVideoSettings("lockfb", 0);
        }
    }
}

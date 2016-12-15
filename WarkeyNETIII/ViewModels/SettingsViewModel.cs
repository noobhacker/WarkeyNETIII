using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarkeyNETIII.Services;

namespace WarkeyNETIII.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private int gameWidth;
        private int gameHeight;
        private int screenWidth;
        private int screenHeight;

        public int GameWidth
        {
            get
            {
                return gameWidth;
            }

            set
            {
                gameWidth = value;
                OnPropertyChanged();
            }
        }

        public int GameHeight
        {
            get
            {
                return gameHeight;
            }

            set
            {
                gameHeight = value;
                OnPropertyChanged();
            }
        }

        public int ScreenWidth
        {
            get
            {
                return screenWidth;
            }

            set
            {
                screenWidth = value;
                OnPropertyChanged();
            }
        }

        public int ScreenHeight
        {
            get
            {
                return screenHeight;
            }

            set
            {
                screenHeight = value;
                OnPropertyChanged();
            }
        }

        public SettingsViewModel()
        {
            var gameResolution = ScreenResolutionService.GetCurrentResolution();
            var screenResolution = RegistryService.ReadResolution();

            GameWidth = gameResolution.Item1;
            GameHeight = gameResolution.Item2;

            ScreenWidth = screenResolution.Item1;
            screenHeight = screenResolution.Item2;
        }
    }
}

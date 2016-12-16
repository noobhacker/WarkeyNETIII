using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarkeyNETIII.Items;
using WarkeyNETIII.Services;

namespace WarkeyNETIII.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private ScreenResolutionItem currentResolution;
        private ScreenResolutionItem gameResolution;
        private string lockFbStatus;
        private bool autoStartWar3;
        private bool disableImage;
        private bool isGameResolutionNeedsOptimize;
        private bool isLockFbNeedsOptimize;
        private bool isAutoStartWar3;

        public ScreenResolutionItem CurrentResolution
        {
            get
            {
                return currentResolution;
            }

            set
            {
                currentResolution = value;
                OnPropertyChanged();
            }
        }

        public ScreenResolutionItem GameResolution
        {
            get
            {
                return gameResolution;
            }

            set
            {
                gameResolution = value;
                OnPropertyChanged();
            }
        }

        public string LockFbStatus
        {
            get
            {
                return lockFbStatus;
            }

            set
            {
                lockFbStatus = value;
                OnPropertyChanged();
            }
        }

        public bool AutoStartWar3
        {
            get
            {
                return autoStartWar3;
            }

            set
            {
                autoStartWar3 = value;
                OnPropertyChanged();
            }
        }

        public bool DisableImage
        {
            get
            {
                return disableImage;
            }

            set
            {
                disableImage = value;
                OnPropertyChanged();
            }
        }

        public bool IsGameResolutionNeedsOptimize
        {
            get
            {
                return isGameResolutionNeedsOptimize;
            }

            set
            {
                isGameResolutionNeedsOptimize = value;
                OnPropertyChanged();
            }
        }

        public bool IsLockFbNeedsOptimize
        {
            get
            {
                return isLockFbNeedsOptimize;
            }

            set
            {
                isLockFbNeedsOptimize = value;
                OnPropertyChanged();
            }
        }

        public bool IsAutoStartWar3
        {
            get
            {
                return isAutoStartWar3;
            }

            set
            {
                isAutoStartWar3 = value;
                OnPropertyChanged();
            }
        }

        public SettingsViewModel()
        {
            GameResolution = RegistryService.ReadResolution();
            CurrentResolution = ScreenResolutionService.GetCurrentResolution();
            var lockfbValue = RegistryService.ReadLockFb();
            if (lockfbValue == 0)
                LockFbStatus = "Optimized";
            else 
                LockFbStatus = "Not optimized";

            IsGameResolutionNeedsOptimize = !(
                GameResolution.Width == CurrentResolution.Width && 
                GameResolution.Height == CurrentResolution.Height);

            IsLockFbNeedsOptimize = (Convert.ToBoolean(lockfbValue));

            IsAutoStartWar3 = true;
        }


    }
}

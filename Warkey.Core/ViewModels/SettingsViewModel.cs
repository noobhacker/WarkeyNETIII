using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warkey.Core.Items;
using Warkey.Core.Services;

namespace Warkey.Core.ViewModels
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
        private bool isStartMinimized;
        private bool isAutoStartWar3;
        private bool isAutoCloseWar3;

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

        public bool IsStartMinimized
        {
            get
            {
                return isStartMinimized;
            }

            set
            {
                isStartMinimized = value;
                OnPropertyChanged();

                Settings.IsStartMinimized = IsStartMinimized;
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

                Settings.IsAutoStartWar3 = IsAutoStartWar3;
            }
        }

        public bool IsAutoCloseWar3
        {
            get
            {
                return isAutoCloseWar3;
            }

            set
            {
                isAutoCloseWar3 = value;
                OnPropertyChanged();
                
                Settings.IsAutoCloseWithWar3 = IsAutoCloseWar3;
            }
        }            

        public SettingsViewModel()
        {
            GameResolution = GameVideoSettingsService.ReadResolution();
            CurrentResolution = ScreenResolutionService.GetCurrentResolution();
            var lockfbValue = GameVideoSettingsService.ReadLockFb();
            if (lockfbValue == 0)
                LockFbStatus = "Optimized";
            else 
                LockFbStatus = "Not optimized";

            IsGameResolutionNeedsOptimize = !(
                GameResolution.Width == CurrentResolution.Width && 
                GameResolution.Height == CurrentResolution.Height);

            IsLockFbNeedsOptimize = (Convert.ToBoolean(lockfbValue));

            IsStartMinimized = Settings.IsStartMinimized;
            isAutoStartWar3 = Settings.IsAutoStartWar3;
            isAutoCloseWar3 = Settings.IsAutoCloseWithWar3;
        }


    }
}

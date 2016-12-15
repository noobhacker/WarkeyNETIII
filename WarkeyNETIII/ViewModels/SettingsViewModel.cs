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

        public SettingsViewModel()
        {
            GameResolution = RegistryService.ReadResolution();
            CurrentResolution = ScreenResolutionService.GetCurrentResolution();
            var lockfbValue = RegistryService.ReadLockFb();
            if (lockfbValue == 0)
                LockFbStatus = "Optimized";
            else 
                LockFbStatus = "Not optimized";
        }


    }
}

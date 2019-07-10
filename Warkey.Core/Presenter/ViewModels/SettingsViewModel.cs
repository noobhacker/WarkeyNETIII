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
        public ScreenResolutionItem CurrentResolution { get; set; }
        public ScreenResolutionItem GameResolution { get; set; }
        public string LockFbStatus { get; set; }
        public bool AutoStartWar3 { get; set; }
        public bool DisableImage { get; set; }
        public bool IsGameResolutionNeedsOptimize { get; set; }
        public bool IsLockFbNeedsOptimize { get; set; }
        public bool IsStartMinimized { get; set; }
        public bool IsAutoStartWar3 { get; set; }
        public bool IsAutoCloseWar3 { get; set; }

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

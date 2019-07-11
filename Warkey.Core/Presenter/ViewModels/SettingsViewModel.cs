using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warkey.Core.Services;
using static Warkey.Infrastructure.Monitor;

namespace Warkey.Core.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public ScreenResolution CurrentResolution { get; set; }
        public ScreenResolution GameResolution { get; set; }
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
           
            IsGameResolutionNeedsOptimize = !(
                GameResolution.Width == CurrentResolution.Width && 
                GameResolution.Height == CurrentResolution.Height);

            IsLockFbNeedsOptimize = Convert.ToBoolean(lockfbValue);

            IsStartMinimized = Settings.IsStartMinimized;
            IsAutoStartWar3 = Settings.IsAutoStartWar3;
            IsAutoCloseWar3 = Settings.IsAutoCloseWithWar3;
        }


    }
}

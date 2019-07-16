using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Warkey.Infrastructure.Monitor;

namespace Warkey.Core.Presenter
{
    [AddINotifyPropertyChangedInterface]
    public class SettingsViewModel
    {
        public ScreenResolution ScreenResolution { get; set; }
        public ScreenResolution GameResolution { get; set; }
        public bool IsGameResolutionNeedsOptimize { get; set; }
        public bool IsLockFbNeedsOptimize { get; set; }
        public bool IsStartMinimized { get; set; }
        public bool IsAutoStartWar3 { get; set; }
        public bool IsAutoCloseWar3 { get; set; }

        public SettingsViewModel()
        {
            ScreenResolution = new ScreenResolution();
            GameResolution = new ScreenResolution();
        }


    }
}

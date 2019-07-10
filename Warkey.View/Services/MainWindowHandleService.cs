using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warkey.Core.Services
{
    public static class MainWindowHandleService
    {
        static string[] processName = { "war3", "War3" };

        public static IntPtr? GetWar3HWND()
        {
            foreach(var item in processName)
            {
                var process = Process.GetProcessesByName(item);
                if (process.Count() != 0)
                    return process[0].MainWindowHandle;
            }

            return null;
        }
    }
}

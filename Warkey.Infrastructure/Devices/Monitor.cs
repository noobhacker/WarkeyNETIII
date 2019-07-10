using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Warkey.Shared;

namespace Warkey.Infrastructure.Devices
{
    public class Monitor
    {
        public ScreenResolution GetCurrentResolution()
        {
            var resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            return new ScreenResolution()
            {
                Width = resolution.Width,
                Height = resolution.Height
            };
        }
    }
}

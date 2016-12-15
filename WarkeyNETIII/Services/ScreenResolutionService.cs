using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarkeyNETIII.Services
{
    public static class ScreenResolutionService
    {
        public static Tuple<int, int> GetCurrentResolution()
        {
            var resolution = Screen.PrimaryScreen.Bounds;
            return new Tuple<int, int>(resolution.Width, resolution.Height);
        }
    }
}

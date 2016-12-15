using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarkeyNETIII.Items;

namespace WarkeyNETIII.Services
{
    public static class ScreenResolutionService
    {
        public static ScreenResolutionItem GetCurrentResolution()
        {
            var resolution = Screen.PrimaryScreen.Bounds;
            return new ScreenResolutionItem()
            {
                Width = resolution.Width,
                Height = resolution.Height
            };
        }
    }
}

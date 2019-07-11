using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Warkey.Infrastructure
{
    public class Monitor
    {
        public ScreenResolution GetCurrentResolution()
        {
            var resolution = Screen.PrimaryScreen.Bounds;
            return new ScreenResolution()
            {
                Width = resolution.Width,
                Height = resolution.Height
            };
        }

        public class ScreenResolution
        {
            public int Width { get; set; }
            public int Height { get; set; }

            public override string ToString()
            {
                return $"{Width} x {Height}";
            }
        }
    }
}

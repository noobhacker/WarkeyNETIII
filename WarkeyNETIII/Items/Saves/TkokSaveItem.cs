using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarkeyNETIII.Items.Saves
{
    public class TkokSaveItem
    {
        public string Version { get; set; }
        public DateTime DateTime { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public string Class { get; set; }
        public int Gold { get; set; }
        public string Password { get; set; }
    }
}

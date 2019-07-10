﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warkey.Core.Items.Saves
{
    public class TkokSaveItem
    {
        public string Version { get; set; }
        public DateTime LastModified { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public string Class { get; set; }
        public int Gold { get; set; }
        public string Password { get; set; }
        public int Exp { get; set; }
    }
}
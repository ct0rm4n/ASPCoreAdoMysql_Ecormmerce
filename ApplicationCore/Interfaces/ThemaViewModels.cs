using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public class ThemaViewModel
    {
        public int ThemaId { get; set; }
        public bool NavBarFixed { get; set; }
        public string NavBarColor { get; set; }
        public string FontFamily { get; set; }
        public string SideBarColor { get; set; }

    }
}

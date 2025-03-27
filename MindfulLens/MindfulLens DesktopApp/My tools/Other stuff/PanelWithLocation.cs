using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.My_tools
{
    public class PanelWithLocation
    {
        public int Location { get; private set; }
        public Panel Panel { get; private set; }
        public PanelWithLocation(int location, Panel panel)
        {
            this.Location = location;
            this.Panel = panel;
        }
    }
}

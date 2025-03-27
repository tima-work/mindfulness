using Logic_Layer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MindfulLens_DesktopApp.My_tools
{
    public abstract class NotEqualRowHeightScroll : ScrollPanel
    {
        protected List<PanelWithLocation> panelWithLocations;
        protected Panel OverflowPanel;
        public NotEqualRowHeightScroll(Size size, Point location, Panel[] content) : base(size, location)
        {
            OverflowPanel = new Panel()
            {
                Width = PanelToSee.Width,
                Height = PanelToSee.Height,
                BackColor = Color.FromArgb(255, 102, 153, 155)
            };
            PanelToSee.Controls.Add(OverflowPanel);

            
        }

        public override void ShowCurrentContent(int value)
        {
            //PanelToSee.Controls.Clear();
            bool overFlowShown = false;
            for (int i = 0; i < panelWithLocations.Count; i++)
            {
                if (panelWithLocations[i].Location - Convert.ToInt32(whole_height * ((ScrollBar.Value * 1.00d) / (ScrollBar.Maximum * 1.00d))) > this.Height)
                    break;
                if (panelWithLocations[i].Location - Convert.ToInt32(whole_height * ((ScrollBar.Value * 1.00d) / (ScrollBar.Maximum * 1.00d))) < panelWithLocations[i].Panel.Height * (-1))
                    continue;
                if (!overFlowShown)
                {
                    OverflowPanel.BringToFront();
                    overFlowShown = true;
                }
                panelWithLocations[i].Panel.Location = new Point(0, panelWithLocations[i].Location - Convert.ToInt32(whole_height * ((ScrollBar.Value * 1.00d) / (ScrollBar.Maximum * 1.00d))));
                panelWithLocations[i].Panel.BringToFront();
            }
        }
    }
}

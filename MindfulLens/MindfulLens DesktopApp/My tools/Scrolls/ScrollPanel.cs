using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.My_tools
{
    public abstract class ScrollPanel : Panel
    {
        protected Panel PanelToSee;
        public VScrollBar ScrollBar { get; protected set; }
        protected System.Windows.Forms.Timer ScrollTimer;
        protected List<Panel> content;
        protected int spacing;
        protected int whole_height;
        protected int previous_scroll_value;
        public ScrollPanel(Size size, Point location)
        {
            this.BackColor = Color.Transparent;
            this.Size = size;
            this.Location = location;


            ScrollBar = new VScrollBar()
            {
                Size = new Size(Convert.ToInt32(Math.Round(size.Width * 0.03d)), Height),
            };
            ScrollBar.Location = new Point(size.Width - ScrollBar.Size.Width, 0);
            ScrollBar.MouseCaptureChanged += ScrollBarMouseCaptureChanged;


            PanelToSee = new Panel()
            {
                BackColor = Color.Transparent,
                Size = new Size(size.Width - ScrollBar.Size.Width * 2, Height),
                Location = new Point(ScrollBar.Size.Width, 0)
            };


            ScrollTimer = new System.Windows.Forms.Timer()
            {
                Interval = 10,
                Enabled = false
            };
            ScrollTimer.Tick += ScrollTimer_Tick;


            this.Controls.Add(PanelToSee);
            this.Controls.Add(ScrollBar);
        }

        private void ScrollBarMouseCaptureChanged(object sender, EventArgs e)
        {
            ScrollTimer.Enabled = !ScrollTimer.Enabled;
        }

        private void ScrollTimer_Tick(object sender, EventArgs e)
        {
            if (previous_scroll_value != ScrollBar.Value)
            {
                previous_scroll_value = ScrollBar.Value;
                ShowCurrentContent(previous_scroll_value / 10);
            }
        }

        public abstract void ShowCurrentContent(int value);

        protected void CheckScroll()
        {
            if (whole_height < this.Height)
                ScrollBar.Visible = false;
            else
                ScrollBar.Visible = true;
        }
    }
}

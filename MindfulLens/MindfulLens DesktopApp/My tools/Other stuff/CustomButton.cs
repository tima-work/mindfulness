using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MindfulLens_DesktopApp.My_tools
{
    public partial class CustomButton : Panel
    {
        private Label text_label;
        private Panel blue_panel;
        private GraphicFeatures graphicFeatures;
        public CustomButton(string text, Size size, Font font, Point location, GraphicFeatures graphicFeatures)
        {
            InitializeComponent();

            this.graphicFeatures = graphicFeatures;

            this.Size = size;
            this.Location = location;
            this.BackColor = Color.FromArgb(255, 40, 40, 40);

            blue_panel = new Panel();
            blue_panel.BackColor = Color.FromArgb(255, 128, 164, 237);
            blue_panel.Size = new Size(size.Width - 4, size.Height - 4);
            blue_panel.Location = new Point(2, 2);

            text_label = new Label();
            text_label.Text = text;
            text_label.Font = font;
            text_label.ForeColor = Color.White;
            text_label.AutoSize = true;
            text_label.BringToFront();

            blue_panel.Controls.Add(text_label);
            text_label.Location = new Point((blue_panel.Width - text_label.Bounds.Width) / 2, (blue_panel.Height - text_label.Bounds.Height) / 2);

            this.Controls.Add(blue_panel);

            blue_panel.MouseEnter += CustomButton_OnMouseEnter;
            blue_panel.MouseLeave += CustomButton_OnMouseLeave;

            text_label.MouseEnter += CustomButton_OnMouseEnter;
            text_label.MouseLeave += CustomButton_OnMouseLeave;
        }

        private void CustomButton_OnMouseEnter(object sender, EventArgs e)
        {
            blue_panel.BackColor = Color.White;
            text_label.ForeColor = Color.FromArgb(255, 128, 164, 237);
            this.graphicFeatures.UnableCursor();
            Cursor = Cursors.Hand;
        }

        private void CustomButton_OnMouseLeave(object sender, EventArgs e)
        {
            blue_panel.BackColor = Color.FromArgb(255, 128, 164, 237);
            text_label.ForeColor = Color.White;
            this.graphicFeatures.EnableCursor();
        }

        public void BindFunction(EventHandler function)
        {
            blue_panel.Click += function;
            text_label.Click += function;
        }
    }
}

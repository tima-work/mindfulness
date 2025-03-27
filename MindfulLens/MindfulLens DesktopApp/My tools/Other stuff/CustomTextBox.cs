using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.My_tools.Other_stuff
{
    public class CustomTextBox : Panel
    {
        public TextBox textBox { get; protected set; }
        protected GraphicFeatures graphicFeatures;
        protected int outlineWidth = 2;
        public CustomTextBox(Color outlineColor, Color backColor, Color textColor, string placeholderText, Size size, Point location, Font font, GraphicFeatures graphicFeatures, bool multiline, bool userVerticlaScroll= false)
        {

            this.BackColor = outlineColor;
            this.Location = location;
            this.Size = size;


            textBox = new TextBox();
            textBox.BorderStyle = BorderStyle.Fixed3D;
            textBox.PlaceholderText = placeholderText;
            textBox.Font = font;
            textBox.Multiline = multiline;
            textBox.Size = new Size(size.Width - outlineWidth * 2, size.Height - outlineWidth * 2);
            textBox.Location = new Point(outlineWidth, outlineWidth);
            textBox.MouseEnter += graphicFeatures.HoverTextbox;
            textBox.MouseLeave += graphicFeatures.UnhoverTextbox;
            textBox.BackColor = backColor;
            textBox.ForeColor = textColor;
            textBox.BorderStyle = BorderStyle.None;

            if (userVerticlaScroll)
                textBox.ScrollBars = ScrollBars.Vertical;


            this.Controls.Add(textBox);

        }
    }
}

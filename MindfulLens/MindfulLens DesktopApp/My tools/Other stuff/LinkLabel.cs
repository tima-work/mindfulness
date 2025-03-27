using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.My_tools.Other_stuff
{
    public class LinkLabel : Panel
    {
        public Label LabelWithLink { get; private set; }

        public LinkLabel(string text)
        {
            LabelWithLink = new Label() 
            {
                MaximumSize = new Size(246, 0),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 15),
                AutoSize = true,
                BorderStyle = BorderStyle.None,
                Text = text
            };  
            
            this.Controls.Add(LabelWithLink);

            LabelWithLink.Height = LabelWithLink.Bounds.Height;

            this.Size = new Size(246, LabelWithLink.Height);
            this.BackColor = Color.Green;
        }
    }
}

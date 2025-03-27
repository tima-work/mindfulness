using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.My_tools.Other_stuff
{
    public class LinksCreator : Panel
    {
        private Func<int> enter_press;
        private Func<int> unfocus;
        public bool IsRemoved { get; private set; }
        public TextBox LinkTextBox { get; private set; }

        public LinksCreator(Func<int> enter_press, Func<int> unfocus)
        {
            this.enter_press = enter_press;
            this.unfocus = unfocus;

            LinkTextBox = new TextBox()
            {
                Font = new Font("Segoe UI", 15),
                BorderStyle = BorderStyle.None,
                Multiline = false,
                PlaceholderText = "Enter link",
                Width = 262
            };

            this.Controls.Add(LinkTextBox);
            this.Size = new Size(LinkTextBox.Width + 4, LinkTextBox.Height + 4);
            this.BackColor = Color.Black;

            LinkTextBox.Location = new Point(2, 2);

            LinkTextBox.Leave += Unfocus;
            LinkTextBox.KeyDown += LinkTextBox_KeyDown;
        }

        private void LinkTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                enter_press();
            }
        }

        private void Unfocus(object sender, EventArgs e)
        {
            unfocus();
            IsRemoved = true;
        }
    }
}

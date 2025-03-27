using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.My_tools
{
    public class CustomComboBox : Panel
    {
        public ComboBox comboBox {  get; private set; }
        public CustomComboBox(Point location, string text, UserControl userControl, GraphicFeatures graphicFeatures, string[] methods)
        {
            BackColor = Color.Black;
            Size = new Size(163, 42);
            Location = location;


            comboBox = new ComboBox();
            comboBox.Font = new Font("Segoe UI", 15.75f);
            comboBox.Size = new Size(159, 38);
            comboBox.Location = new Point(2, 2);
            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.Text = text;
            comboBox.MouseEnter += graphicFeatures.HoverTextbox;
            comboBox.MouseLeave += graphicFeatures.UnhoverTextbox;
            this.Controls.Add(comboBox);

            foreach (string method in methods)
                comboBox.Items.Add(method);


            userControl.Controls.Add(this);
        }
    }
}

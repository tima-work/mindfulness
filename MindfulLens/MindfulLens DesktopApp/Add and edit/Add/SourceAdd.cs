using Logic_Layer.Classes;
using Logic_Layer.Managers;
using MindfulLens_DesktopApp.Add_and_edit.Abstract_add_and_edit;
using MindfulLens_DesktopApp.My_tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.Add_and_edit.Add
{
    public class SourceAdd : SourceAddAndEdit
    {
        public SourceAdd(GraphicFeatures graphicFeatures, Size size, User creator, SourceManager sourceManager) : base("Add", graphicFeatures, size, sourceManager)
        {
            MakeCreatedByUser(creator, DateTime.Now);
        }

        public void PrepareForAdd()
        {
            int label_textbox_spacing = 20;
            NameTextBox.textBox.Text = string.Empty;
            DescriptionTextBox.textBox.Text = string.Empty;
            AuthorTextBox.textBox.Text = string.Empty;
            try
            {
                this.Controls.Remove(linksScroll);
            }
            catch { }
            linksScroll = new LinksScroll(new Size(this.Width - (DescriptionTextBox.Location.X + DescriptionTextBox.Width + label_textbox_spacing * 2), AddLinkLabel.Location.Y - (LinksLabel.Location.Y + LinksLabel.Height + label_textbox_spacing * 2)), new Point(LinksLabel.Location.X, LinksLabel.Location.Y + LinksLabel.Height + label_textbox_spacing), new My_tools.Other_stuff.LinkLabel[] { });
            this.Controls.Add(linksScroll);
        }

        public string[] GetLinks()
        {
            return linksScroll.GetLinks();
        }
    }
}

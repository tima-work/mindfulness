using Logic_Layer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.My_tools
{
    public class LinksScroll : NotEqualRowHeightScroll
    {
        private List<string> links;
        public LinksScroll(Size size, Point location, My_tools.Other_stuff.LinkLabel[] content) : base(size, location, content)
        {
            links = new List<string>();
            ScrollBar.Width = 30;
            PanelToSee.Width = size.Width - ScrollBar.Width;
            PanelToSee.Location = new Point(0, 0);
            ScrollBar.Location = new Point(PanelToSee.Location.X + PanelToSee.Width, 0);
            spacing = 3;
            panelWithLocations = new List<PanelWithLocation>();
            whole_height = spacing;
            this.content = new List<Panel>(content);
            for (int i = 0; i < content.Length; i++)
            {
                panelWithLocations.Add(new PanelWithLocation(whole_height, content[i]));
                whole_height += content[i].Height + spacing;
                links.Add(content[i].LabelWithLink.Text);
            }
            ScrollBar.Maximum = whole_height / 10;
            //int? a = Convert.ToInt32(null);
            TitledPublication a = new CognitivePart("Salam", new User("Bob", "bob@gmail.com", "bob12345", "bob"), "Bias 1", "Bias", "Overcome");
            ShowCurrentContent(0);
            CheckScroll();
        }

        public void AddObject(Panel panel)
        {
            panelWithLocations.Add(new PanelWithLocation(whole_height, panel));
            whole_height += panel.Height + spacing;
            ScrollBar.Maximum = whole_height / 10;
            ShowCurrentContent(ScrollBar.Value);
            CheckScroll();
        }

        public void RemoveObject(Panel panel)
        {
            PanelWithLocation? pwl = panelWithLocations.FirstOrDefault(p => p.Panel == panel);
            if ( pwl != null)
            {
                int place = panelWithLocations.IndexOf(pwl);
                panelWithLocations.RemoveAt(place);
                whole_height = whole_height - panel.Height;
                ScrollBar.Maximum = whole_height / 10;
                ShowCurrentContent(ScrollBar.Value);
                CheckScroll();
            }
        }

        public string[] GetLinks()
        {
            return links.ToArray();
        }
    }
}

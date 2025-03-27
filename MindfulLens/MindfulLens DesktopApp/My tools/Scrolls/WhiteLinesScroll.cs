using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MindfulLens_DesktopApp.My_tools
{
    public class WhiteLinesScroll : NotEqualRowHeightScroll
    {
        private WhiteLine whiteLine;
        public WhiteLinesScroll(Size size, Point location, Panel[] content, int spacing, bool showMoreButton, GraphicFeatures graphicFeatures, EventHandler loadMoreFunction) : base(size, location, content)
        {
            this.spacing = spacing;
            panelWithLocations = new List<PanelWithLocation>();
            whole_height = spacing;
            this.content = new List<Panel>(content);
            if (content.Length > 0 )
            {
                for (int i = 0; i < content.Length - 1; i++)
                {
                    whiteLine = new WhiteLine();
                    panelWithLocations.Add(new PanelWithLocation(whole_height, content[i]));
                    whole_height += content[i].Height + spacing;
                    PanelToSee.Controls.Add(content[i]);
                    panelWithLocations.Add(new PanelWithLocation(whole_height, whiteLine));
                    whole_height += whiteLine.Height + spacing;
                    PanelToSee.Controls.Add(whiteLine);
                }
                panelWithLocations.Add(new PanelWithLocation(whole_height, content[content.Length - 1]));
                whole_height += content[content.Length - 1].Height + spacing;
                PanelToSee.Controls.Add(content[content.Length - 1]);
            }

            if (showMoreButton)
            {
                Panel showMorePanel = new Panel()
                {
                    BackColor = Color.Transparent,
                    Size = new Size(PanelToSee.Width, 100),
                };
                CustomButton showMoreCustomButton = new CustomButton("Load more", new Size(160, 60), new Font("Segoe UI", 15.75f), new Point((showMorePanel.Width - 160) / 2, (showMorePanel.Height - 60) / 2), graphicFeatures);
                showMoreCustomButton.BindFunction(loadMoreFunction);
                showMorePanel.Controls.Add(showMoreCustomButton);
                panelWithLocations.Add(new PanelWithLocation(whole_height, showMorePanel));
                whole_height += showMorePanel.Height;
                PanelToSee.Controls.Add(showMorePanel);
            }

            ScrollBar.Maximum = whole_height / 10;
            ShowCurrentContent(0);
            CheckScroll();
        }

        public void AddObject(GraphicContent publication)
        {
            whiteLine = new WhiteLine();
            panelWithLocations.Add(new PanelWithLocation(whole_height, publication));
            whole_height += publication.Height + spacing;
            panelWithLocations.Add(new PanelWithLocation(whole_height, whiteLine));
            whole_height += whiteLine.Height + spacing;
            ScrollBar.Maximum = whole_height / 10;
            ShowCurrentContent(ScrollBar.Value);
        }

        public void RemoveObject(GraphicContent publication)
        {
            PanelWithLocation pwl = panelWithLocations.FirstOrDefault(p => p.Panel == publication);
            int place = panelWithLocations.IndexOf(pwl);
            if (place != panelWithLocations.Count - 1)
            {
                panelWithLocations.RemoveAt(place);
                panelWithLocations.RemoveAt(place);
            }
            else
            {
                panelWithLocations.RemoveAt(place);
                panelWithLocations.RemoveAt(place - 1);
            }
            whole_height = whole_height - publication.Height;
            whole_height = whole_height - new WhiteLine().Height;
            ScrollBar.Maximum = whole_height / 10;
            ShowCurrentContent(ScrollBar.Value);
        }

    }
}

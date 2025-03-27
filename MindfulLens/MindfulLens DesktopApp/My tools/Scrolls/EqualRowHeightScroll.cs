using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MindfulLens_DesktopApp.My_tools
{
    public class EqualRowHeightScroll : ScrollPanel
    {
        int panels_in_one_scroll;
        protected List<Panel> all_panels;
        private Panel OverflowPanel;
        public EqualRowHeightScroll(Size size, Point location, GraphicTitledPublication[] content, GraphicFeatures graphicFeatures, bool showMoreButton, EventHandler loadMoreFunction) : base(size, location)
        {
            this.content = new List<Panel>(content);


            int quantity = content.Count();

            if (quantity != 0)
            {
                spacing = (PanelToSee.Width - content[0].Width * 3) / 4;
                all_panels = new List<Panel>();
                Panel panel = null;
                Color color;

                OverflowPanel = new Panel()
                {
                    Width = PanelToSee.Width,
                    Height = PanelToSee.Height,
                    BackColor = Color.FromArgb(255, 102, 153, 155)
                };
                PanelToSee.Controls.Add(OverflowPanel);

                for (int i = 0; i < quantity; i++)
                {
                    if (i % 3 == 0)
                    {
                        //color = i % 6 == 0 ? Color.Red : Color.Green;
                        panel = new Panel()
                        {
                            BackColor = Color.Transparent,
                            Size = new Size(PanelToSee.Width, content[0].Height + spacing),
                            Location = new Point(0, (i / 3) * (content[0].Height + spacing))
                        };
                        all_panels.Add(panel);
                        PanelToSee.Controls.Add(panel);
                    }
                    content[i].Location = new Point(i % 3 * (spacing + content[0].Width) + spacing, 0);
                    panel.Controls.Add(content[i]);
                }
                if (showMoreButton)
                {
                    Panel showMorePanel = new Panel()
                    {
                        BackColor = Color.Transparent,
                        Size = new Size(PanelToSee.Width, content[0].Height + spacing),
                        Location = new Point(0, all_panels.Count * all_panels[0].Height)
                    };
                    CustomButton showMoreCustomButton = new CustomButton("Load more", new Size(160, 60), new Font("Segoe UI", 15.75f), new Point((panel.Width - 160) / 2, (panel.Height - 60) / 2), graphicFeatures);
                    showMoreCustomButton.BindFunction(loadMoreFunction);
                    showMorePanel.Controls.Add(showMoreCustomButton);
                    all_panels.Add(showMorePanel);
                    PanelToSee.Controls.Add(showMorePanel);
                }

                whole_height = (all_panels.Count - 1) * (spacing + content[0].Height) + all_panels[all_panels.Count - 1].Height;
                if (whole_height < panels_in_one_scroll * content[0].Height)
                    ScrollBar.Visible = false;

                ScrollBar.Maximum = quantity / 3 * 10;
                if (quantity % 3 != 0)
                    ScrollBar.Maximum += 10;
                //if (whole_height > panels_in_one_scroll * content[0].Height)
                //    ScrollBar.Maximum = ScrollBar.Maximum - (panels_in_one_scroll * 10) - 10;

                if (PanelToSee.Height % (content[0].Height + spacing) == 0)
                    panels_in_one_scroll = PanelToSee.Height / (content[0].Height + spacing);
                else
                    panels_in_one_scroll = PanelToSee.Height / (content[0].Height + spacing) + 1;

                ShowCurrentContent(0);
                CheckScroll();
            }

        }

        public override void ShowCurrentContent(int value)
        {
            //PanelToSee.Controls.Clear();
            //int max;
            //if (all_panels.Count < value + panels_in_one_scroll + 1)
            //    max = all_panels.Count;
            //else
            //    max = value + panels_in_one_scroll + 1;
            //for (int i = value; i < max; i++)
            //{
            //    all_panels[i].Location = new Point(0, i * (spacing + content[0].Height) - Convert.ToInt32(whole_height * ((ScrollBar.Value * 1.00d) / (ScrollBar.Maximum * 1.00d))));
            //    PanelToSee.Controls.Add(all_panels[i]);
            //}
            int max;
            if (all_panels.Count < value + panels_in_one_scroll + 1)
            {
                max = all_panels.Count;
                OverflowPanel.BringToFront();
            }
            else
                max = value + panels_in_one_scroll + 1;
            for (int i = value; i < max; i++)
            {
                all_panels[i].Location = new Point(0, i * (spacing + content[0].Height) - Convert.ToInt32(whole_height * ((ScrollBar.Value * 1.00d) / (ScrollBar.Maximum * 1.00d))));
                all_panels[i].BringToFront();
                //panel33.Controls.Add(all_panels[i]);
            }
        }

        public void AddObject(GraphicTitledPublication publication)
        {
            int count = all_panels.Count;
            if (count == 0)
                AddObjectToNewPanel(publication);
            else
            {
                int children_in_last_panel = all_panels[all_panels.Count - 1].Controls.Count;
                if (children_in_last_panel < 3)
                {
                    publication.Location = new Point((spacing + all_panels[count - 1].Controls[0].Width) * children_in_last_panel + spacing, 0);
                    all_panels[count - 1].Controls.Add(publication);
                }
                else
                    AddObjectToNewPanel(publication);
            }
        }

        private void AddObjectToNewPanel(GraphicTitledPublication publication)
        {
            Panel panel = new Panel()
            {
                BackColor = Color.Transparent,
                Size = new Size(PanelToSee.Width, publication.Height + spacing),
                Location = new Point(0, whole_height)
            };
            whole_height += panel.Height;
            all_panels.Add(panel);
            publication.Location = new Point(spacing, 0);
            panel.Controls.Add(publication);
            ScrollBar.Maximum = all_panels.Count * 10;
            ShowCurrentContent(ScrollBar.Value / 10);
            CheckScroll();
        }
    }
}

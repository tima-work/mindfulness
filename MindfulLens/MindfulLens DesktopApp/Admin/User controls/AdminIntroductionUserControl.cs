using MindfulLens_DesktopApp.My_tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MindfulLens_DesktopApp.User_controls
{
    public partial class AdminIntroductionUserControl : UserControl
    {
        private List<Panel> all_panels;
        int rows_in_panel = 1;
        int spacing;
        int button_height = 100;
        int whole_height;
        int panels_in_one_scroll;
        int previous_scroll_value = 0;
        private Panel OverflowPanel;
        //System.Windows.Forms.Timer timer1, timer2, timer3, timer4;
        public AdminIntroductionUserControl()
        {
            InitializeComponent();
            this.Name = "Introduction";
        }

        //private void panel33_Paint(object sender, PaintEventArgs e)
        //{

        //}

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    //int button_width = 200;
        //    //int button_height = 100;
        //    //int spacing = (panel33.Width - (button_width * 3)) / 4;
        //    //int quantity = Convert.ToInt32(numericUpDown1.Value);
        //    //int panel_quantity;
        //    //int whole_height;
        //    //if (quantity % 3 == 0)
        //    //    whole_height = quantity / 3 * button_height + (quantity / 3 + 1) * spacing;
        //    //else
        //    //{
        //    //    whole_height = (quantity / 3 + 1) * button_height + ((quantity / 3 + 1) + 1) * spacing;
        //    //}
        //    //if (whole_height % limit == 0)
        //    //    panel_quantity = whole_height / limit;
        //    //else
        //    //    panel_quantity = whole_height / limit + 1;
        //    //panels = new Panel[panel_quantity];
        //    //for (int i = 0; i < panel_quantity; i++)
        //    //{
        //    //    Color color;
        //    //    if (i % 2 == 0)
        //    //        color = Color.White;
        //    //    else
        //    //        color = Color.Black;
        //    //    panels[i] = new Panel()
        //    //    {
        //    //        Location = new Point(0, i * limit),
        //    //        BackColor = color,
        //    //        Size = new Size(0, limit)
        //    //    };
        //    //    panel33.Controls.Add(panels[i]);
        //    //    panels[i].BringToFront();
        //    //}

        //    //Button btn;
        //    //int current_limit = (limit / (button_height + spacing) * 3);
        //    //Panel current_panel = panels[0];
        //    //for (int i = 0; i < quantity; i++)
        //    //{
        //    //    btn = new Button()
        //    //    {
        //    //        Text = $"Button {i + 1}",
        //    //        Size = new Size(button_width, button_height),
        //    //        Font = new Font("Segoe UI", 18),
        //    //        Location = new Point((i % 3 + 1) * spacing + i % 3 * button_width, ((i / 3 + 1) * spacing + i / 3 * button_height) % limit),
        //    //        BackColor = Color.Yellow
        //    //    };
        //    //    if (i == current_limit)
        //    //        current_panel = panels[Array.IndexOf(panels, current_panel) + 1];
        //    //    if (i == 620 || i == 621)
        //    //        MessageBox.Show(btn.Location.Y.ToString());
        //    //    current_panel.Controls.Add(btn);
        //    //}


        //    //for (int i = 0; i < panels.Length; i++)
        //    //{
        //    //    if (i != panels.Length - 1)
        //    //        panels[i].Size = new Size(panel33.Width, limit);
        //    //    else
        //    //    {
        //    //        panels[i].Size = new Size(panel33.Width, (panels[i].Controls.Count / 3 + 1) * (button_height + spacing));
        //    //    }
        //    //}
        //    //MessageBox.Show(panels[1].Location.Y.ToString());
        //    //vScrollBar1.Maximum = quantity * 100;



        //    int button_width = 200;
        //    spacing = (panel33.Width - (button_width * 3)) / 4;
        //    int quantity = Convert.ToInt32(numericUpDown1.Value);
        //    all_panels = new List<Panel>();
        //    Panel panel = null;
        //    OverflowPanel = new Panel()
        //    {
        //        Width = panel33.Width,
        //        Height = panel33.Height,
        //        BackColor = Color.FromArgb(255, 102, 153, 155)
        //    };
        //    panel33.Controls.Add(OverflowPanel);
        //    for (int i = 0; i < quantity; i++)
        //    {
        //        if (i % (rows_in_panel * 3) == 0)
        //        {
        //            //Color color = i % (rows_in_panel * 2) == 0 ? Color.Black : Color.White;
        //            panel = new Panel()
        //            {
        //                Size = new Size(panel33.Width, (spacing + button_height) * rows_in_panel),
        //                Location = new Point(0, (spacing + button_height) * rows_in_panel * (i / rows_in_panel)),
        //                //BackColor = color
        //            };
        //            panel33.Controls.Add(panel);
        //            all_panels.Add(panel);
        //        }
        //        panel.Controls.Add(new Button()
        //        {
        //            Text = $"Button {i + 1}",
        //            BackColor = Color.Yellow,
        //            Font = new Font("Segoe UI", 18),
        //            Location = new Point((i % 3) * (spacing + button_width) + spacing, ((i % (rows_in_panel * 3) / 3) * (spacing + button_height) + spacing)),
        //            Size = new Size(button_width, button_height),
        //        });
        //    }
        //    vScrollBar1.Maximum = quantity / 3 * 10;
        //    if (quantity % 3 != 0)
        //        vScrollBar1.Maximum += 10;
        //    vScrollBar1.Maximum = vScrollBar1.Maximum - (panels_in_one_scroll * 10) - 10;

        //    //MessageBox.Show(all_panels[1].Height.ToString());
        //    //all_panels[0].Visible = false;

        //    whole_height = (all_panels.Count - 1) * rows_in_panel * (spacing + button_height) + all_panels[all_panels.Count - 1].Height;

        //    if (panel33.Height % (button_height + spacing) == 0)
        //        panels_in_one_scroll = panel33.Height / (button_height + spacing);
        //    else
        //        panels_in_one_scroll = panel33.Height / (button_height + spacing) + 1;

        //    //MessageBox.Show(panels_in_one_scroll.ToString());

        //    //MakeScrollBar();
        //}

        //private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        //{
        //    //for (int i = 0; i < all_panels.Count; i++)
        //    //{
        //    //    all_panels[i].Location = new Point(0, i * rows_in_panel * (spacing + button_height) - Convert.ToInt32(whole_height * ((vScrollBar1.Value * 1.00d) / (vScrollBar1.Maximum * 1.00d))));
        //    //if (all_panels[i].Location.Y < (spacing + button_height) * rows_in_panel * (-1) || all_panels[i].Location.Y > panel33.Height)
        //    //    all_panels[i].Visible = false;
        //    //else
        //    //    all_panels[i].Visible = true;
        //    //}
        //    //all_panels[i].Location = new Point(0, i * rows_in_panel * (spacing + button_height) - Convert.ToInt32(whole_height * ((vScrollBar1.Value * 1.00d) / (vScrollBar1.Maximum * 1.00d))));
        //    //panel1.Location = new Point(0, Convert.ToInt32(panel1.Height * ((vScrollBar1.Value * 1.00d) / (vScrollBar1.Maximum * 1.00d)) * (-1)));
        //    //MakeScrollBar(vScrollBar1.Value / 10);
        //}

        //private void MakeScrollBar(int place)
        //{
        //    //panel33.Controls.Clear();
        //    //int max;
        //    //if (all_panels.Count < place + panels_in_one_scroll + 1)
        //    //    max = all_panels.Count;
        //    //else
        //    //    max = place + panels_in_one_scroll + 1;
        //    //for (int i = place; i < max; i++)
        //    //{
        //    //    try
        //    //    {
        //    //        all_panels[i].Location = new Point(0, i * rows_in_panel * (spacing + button_height) - Convert.ToInt32(whole_height * ((vScrollBar1.Value * 1.00d) / (vScrollBar1.Maximum * 1.00d))));
        //    //        panel33.Controls.Add(all_panels[i]);
        //    //    }
        //    //    catch
        //    //    {
        //    //        MessageBox.Show($"{place} {max}");
        //    //    }
        //    //}
        //    //MessageBox.Show($"{place} {place + panels_in_one_scroll + 1} {all_panels.Count}");



        //    //for (int i = 0; i < all_panels.Count(); i++)
        //    //    all_panels[i].Location = new Point(0, i * rows_in_panel * (spacing + button_height) - Convert.ToInt32(whole_height * ((vScrollBar1.Value * 1.00d) / (vScrollBar1.Maximum * 1.00d))));

        //    int max;
        //    int value = vScrollBar1.Value / 10;
        //    if (all_panels.Count < value + panels_in_one_scroll + 1)
        //    {
        //        max = all_panels.Count;
        //        OverflowPanel.BringToFront();
        //    }
        //    else
        //        max = value + panels_in_one_scroll + 1;
        //    for (int i = value; i < max; i++)
        //    {
        //        all_panels[i].Location = new Point(0, i * (spacing + button_height) - Convert.ToInt32(whole_height * ((vScrollBar1.Value * 1.00d) / (vScrollBar1.Maximum * 1.00d))));
        //        all_panels[i].BringToFront();
        //        //panel33.Controls.Add(all_panels[i]);
        //    }

        //    //int max = all_panels.Count();
        //    //if (!(max < previous_place + panels_in_one_scroll + 1))
        //    //    max = previous_place + panels_in_one_scroll + 1;
        //    //for (int i = (place / 10); i < max; i++)
        //    //    all_panels[i].Location = new Point(0, i * rows_in_panel * (spacing + button_height) - Convert.ToInt32(whole_height * ((place * 1.00d) / (vScrollBar1.Maximum * 1.00d))));
        //}

        private void ScrollTimer_Tick(object sender, EventArgs e)
        {
            //if (previous_scroll_value != vScrollBar1.Value)
            //{
            //    MakeScrollBar(vScrollBar1.Value / 10);
            //    previous_scroll_value = vScrollBar1.Value;
            //}
        }

        //private void vScrollBar1_MouseCaptureChanged(object sender, EventArgs e)
        //{
        //    ScrollTimer.Enabled = !ScrollTimer.Enabled;
        //}



        //public void AddObject(Button publication)
        //{
        //    int count = all_panels.Count;
        //    if (count == 0)
        //        AddObjectToNewPanel(publication);
        //    else
        //    {
        //        int children_in_last_panel = all_panels[all_panels.Count - 1].Controls.Count;
        //        if (children_in_last_panel < 3)
        //        {
        //            publication.Location = new Point((spacing + all_panels[count - 1].Controls[0].Width) * children_in_last_panel + spacing, spacing);
        //            all_panels[count - 1].Controls.Add(publication);
        //        }
        //        else
        //            AddObjectToNewPanel(publication);
        //    }
        //}

        //private void AddObjectToNewPanel(Button publication)
        //{
        //    Panel panel = new Panel()
        //    {
        //        BackColor = Color.Transparent,
        //        Size = new Size(panel33.Width, publication.Height + spacing),
        //        Location = new Point(0, whole_height)
        //    };
        //    whole_height += panel.Height;
        //    all_panels.Add(panel);
        //    publication.Location = new Point(spacing, spacing);
        //    panel.Controls.Add(publication);
        //    vScrollBar1.Maximum = all_panels.Count * 10;
        //    MakeScrollBar(vScrollBar1.Value / 10);
        //    //CheckScroll();
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    AddObject(new Button()
        //    {
        //        Text = "Some button",
        //        BackColor = Color.Yellow,
        //        Font = new Font("Segoe UI", 18),
        //        Size = new Size(200, button_height),
        //    });
        //}

        //private void label1_Click(object sender, EventArgs e)
        //{

        //}
    }
}

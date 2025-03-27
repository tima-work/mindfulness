using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp
{
    public class GraphicFeatures
    {
        private int chosen_tab, what_to_do, x_coef, y_coef, x_pos, y_pos, counter, helpTabIndex;
        private int tabIndex = 0;
        private int resize_speed = 10;
        private int navbar_speed = 30;
        private double xGap, yGap;
        private bool maximized = false;
        private double[] resize_coefs;
        public Form form { get; private set; }
        private List<UserControl> tabs = new List<UserControl>();
        private List<Panel> tab_panels = new List<Panel>();
        public Panel HeaderPanel { get; private set; }
        public Panel ControlsPanel { get; private set; }
        public PictureBox Logo { get; private set; }
        public Label MinimizeLabel { get; private set; }
        public PictureBox MaximizePicture { get; private set; }
        public Label CloseLabel { get; private set; }
        public Panel NavPanel { get; private set; }
        public Panel ContentPanel { get; private set; }
        public Panel ChosenTabPanel { get; private set; }
        public Panel[] ControlButtonsPanels = new Panel[3];
        public Control[] ControlButtons = new Control[3];
        private EventHandler[] ControlFuncs;
        private Dictionary<UserControl, Point> animation_ucs = new Dictionary<UserControl, Point>();
        private System.Windows.Forms.Timer CursorTimer;
        private System.Windows.Forms.Timer ActionTimer;



        public GraphicFeatures(Form form)
        {
            int window_width = 920;
            int window_height = 598;
            int control_button_size = 60;
            ControlFuncs = new EventHandler[3] {Minimize, Maximize, Close};


            this.form = form;
            this.form.FormBorderStyle = FormBorderStyle.None;
            this.form.Size = new Size(window_width, window_height);
            this.form.StartPosition = FormStartPosition.CenterScreen;
            this.form.Controls.Clear();
            this.form.BackColor = Color.FromArgb(255, 102, 153, 155);
            this.form.MouseDown += GeneralMouseDown;
            this.form.MouseUp += GeneralMouseUp;


            HeaderPanel = new Panel();
            HeaderPanel.Size = new Size(window_width, control_button_size);
            HeaderPanel.Location = new Point(0, 0);
            HeaderPanel.MouseDown += GeneralMouseDown;
            HeaderPanel.MouseUp += GeneralMouseUp;
            this.form.Controls.Add(HeaderPanel);


            ControlsPanel = new Panel();
            ControlsPanel.Size = new Size(control_button_size * 3, control_button_size);
            ControlsPanel.Location = new Point(window_width - control_button_size * 3, 0);
            HeaderPanel.Controls.Add(ControlsPanel);


            MinimizeLabel = new Label();
            MinimizeLabel.Text = "_";
            MinimizeLabel.Font = new Font("Segoe UI", 30);
            MinimizeLabel.ForeColor = Color.White;
            MinimizeLabel.TextAlign = ContentAlignment.MiddleCenter;
            MinimizeLabel.AutoSize = true;


            MaximizePicture = new PictureBox();
            MaximizePicture.BackgroundImage = Properties.Resources.Maximize3;
            MaximizePicture.BackgroundImageLayout = ImageLayout.Zoom;
            MaximizePicture.Height = Convert.ToInt32(control_button_size * 0.5d);
            MaximizePicture.Width = MaximizePicture.Bounds.Width;



            CloseLabel = new Label();
            CloseLabel.Text = "🞪";
            CloseLabel.Font = new Font("Segoe UI", 20);
            CloseLabel.ForeColor = Color.White;
            CloseLabel.TextAlign = ContentAlignment.MiddleCenter;
            CloseLabel.AutoSize = true;


            ControlButtons[0] = MinimizeLabel;
            ControlButtons[1] = MaximizePicture;
            ControlButtons[2] = CloseLabel;


            for (int i = 0; i < 3; i++)
            {
                ControlButtons[i].BackColor = Color.Transparent;
                ControlButtons[i].MouseEnter += ChildHover;
                ControlButtons[i].MouseLeave += ChildUnhover;
                ControlButtons[i].Click += ControlFuncs[i];


                ControlButtonsPanels[i] = new Panel();
                ControlButtonsPanels[i].Size = new Size(control_button_size, control_button_size);
                ControlButtonsPanels[i].BackColor = Color.Transparent;
                ControlButtonsPanels[i].Location = new Point(i * control_button_size, 0);
                ControlButtonsPanels[i].MouseEnter += ParentHover;
                ControlButtonsPanels[i].MouseLeave += ParentUnhover;
                ControlButtonsPanels[i].Click += ControlFuncs[i];


                ControlButtonsPanels[i].Controls.Add(ControlButtons[i]);


                ControlsPanel.Controls.Add(ControlButtonsPanels[i]);
            }


            ControlButtons[0].Location = new Point((control_button_size - MinimizeLabel.Bounds.Width) / 2, control_button_size / (-8));
            ControlButtons[1].Location = new Point((control_button_size - MaximizePicture.Width) / 2, (control_button_size - MaximizePicture.Height) / 2);
            ControlButtons[2].Location = new Point((control_button_size - CloseLabel.Bounds.Width) / 2, (control_button_size - CloseLabel.Bounds.Height) / 2);


            Logo = new PictureBox();
            Logo.BackgroundImage = Properties.Resources.Group_1;
            Logo.BackgroundImageLayout = ImageLayout.Zoom;
            Logo.Height = control_button_size;
            Logo.Width = (Logo.Bounds.Height * Logo.BackgroundImage.Width) / Logo.BackgroundImage.Height;
            Logo.Location = new Point((window_width - Logo.Width) / 2, 0);
            Logo.MouseDown += GeneralMouseDown;
            Logo.MouseUp += GeneralMouseUp;
            HeaderPanel.Controls.Add(Logo);


            NavPanel = new Panel();
            NavPanel.Size = new Size(window_width, control_button_size);
            NavPanel.BackColor = Color.Transparent;
            NavPanel.Location = new Point(0, control_button_size);
            this.form.Controls.Add(NavPanel);


            ContentPanel = new Panel();
            ContentPanel.Size = new Size(window_width, window_height - control_button_size * 2);
            ContentPanel.Location = new Point(0, control_button_size * 2);
            ContentPanel.MouseDown += GeneralMouseDown;
            ContentPanel.MouseUp += GeneralMouseUp;
            this.form.Controls.Add(ContentPanel);


            ChosenTabPanel = new Panel();
            ChosenTabPanel.Size = new Size(10, 4);
            ChosenTabPanel.Location = new Point(0, control_button_size - ChosenTabPanel.Height);
            ChosenTabPanel.BackColor = Color.White;
            this.NavPanel.Controls.Add(ChosenTabPanel);


            CursorTimer = new System.Windows.Forms.Timer();
            CursorTimer.Interval = 100;
            CursorTimer.Enabled = true;
            CursorTimer.Tick += CursorTimer_Tick;


            ActionTimer = new System.Windows.Forms.Timer();
            ActionTimer.Interval = 10;
            ActionTimer.Tick += ActionTimer_Tick;
        }

        private void CursorTimer_Tick1(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GraphicFeatures_MouseLeave(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ParentHover(object sender, EventArgs e)
        {
            PanelHover(sender as Panel);
        }


        public void ParentUnhover(object sender, EventArgs e)
        {
            PanelUnhover(sender as Panel);
        }


        public void ChildHover(object sender, EventArgs e)
        {
            PanelHover((sender as Control).Parent as Panel);
        }


        public void ChildUnhover(object sender, EventArgs e)
        {
            PanelUnhover((sender as Control).Parent as Panel);
        }


        private void PanelHover(Panel panel)
        {
            panel.BackColor = Color.FromArgb(60, 255, 255, 255);
            CursorTimer.Enabled = false;
            form.Cursor = Cursors.Hand;
        }


        private void PanelUnhover(Panel panel)
        {
            panel.BackColor = Color.Transparent;
            CursorTimer.Enabled = true;
        }

        public void MakeNavBar(List<UserControl> tabs)
        {
            this.tabs = tabs;
            Label current_label;
            Panel current_panel;
            int all_label_width = 0;
            int spacing;
            int x_loc = 0;


            for (int i = 0; i < tabs.Count; i++)
            {
                current_label = new Label();
                current_label.Text = tabs[i].Name;
                current_label.AutoSize = true;
                current_label.Font = new Font("Segoe UI", 14);
                current_label.ForeColor = Color.White;
                current_label.BackColor = Color.Transparent;
                current_label.MouseEnter += ChildHover;
                current_label.MouseLeave += ChildUnhover;
                current_label.Click += TabLabelPress;
                current_panel = new Panel();
                current_panel.Controls.Add(current_label);
                NavPanel.Controls.Add(current_panel);
                all_label_width += current_label.Bounds.Width;
                tab_panels.Add(current_panel);
            }


            spacing = (NavPanel.Width - all_label_width) / tabs.Count;


            for (int i = 0; i < tabs.Count; i++)
            {
                current_panel = tab_panels[i];
                current_panel.BackColor = Color.Transparent;
                current_panel.Size = new Size(current_panel.Controls[0].Bounds.Width + spacing, NavPanel.Height);
                current_panel.Location = new Point(x_loc, 0);
                x_loc += current_panel.Width;
                current_panel.MouseEnter += ParentHover;
                current_panel.MouseLeave += ParentUnhover;
                current_panel.Click += TabPanelPress;
                current_panel.Controls[0].Location = new Point(spacing / 2, (current_panel.Height - current_panel.Controls[0].Bounds.Height) / 2);
            }

            FixChosenTabPanel();


            ContentPanel.Controls.Add(tabs[tabIndex]);
        }


        private bool CheckLeft()
        {
            return (Cursor.Position.X >= form.Location.X - 10 && Cursor.Position.X <= form.Location.X + 10);
        }

        private bool CheckRight()
        {
            return (Cursor.Position.X >= form.Location.X + form.Width - 10 && Cursor.Position.X <= form.Location.X + form.Width + 10);
        }

        private bool CheckTop()
        {
            return (Cursor.Position.Y >= form.Location.Y - 10 && Cursor.Position.Y <= form.Location.Y + 10);
        }

        private bool CheckBottom()
        {
            return (Cursor.Position.Y >= form.Location.Y + form.Height - 10 && Cursor.Position.Y <= form.Location.Y + form.Height + 10);
        }


        private void CursorTimer_Tick(object sender, EventArgs e)
        {
            if (maximized)
                form.Cursor = Cursors.Default;
            else if ((CheckLeft() && CheckTop()) || (CheckRight() && CheckBottom()))
                form.Cursor = Cursors.SizeNWSE;
            else if ((CheckLeft() && CheckBottom()) || (CheckRight() && CheckTop()))
                form.Cursor = Cursors.SizeNESW;
            else if (CheckLeft() || CheckRight())
                form.Cursor = Cursors.SizeWE;
            else if (CheckTop() || CheckBottom())
                form.Cursor = Cursors.SizeNS;
            else
                form.Cursor = Cursors.Default;
        }


        private void ActionTimer_Tick(object sender, EventArgs e)
        {
            switch (what_to_do)
            {
                case 0:
                    form.Width = x_coef - Cursor.Position.X;
                    form.Height = y_coef - Cursor.Position.Y;
                    form.Location = Cursor.Position;
                    break;
                case 1:
                    form.Width = Cursor.Position.X - x_coef;
                    form.Height = Cursor.Position.Y - y_coef;
                    form.Location = new Point(x_coef, y_coef);
                    break;
                case 2:
                    form.Width = x_coef - Cursor.Position.X;
                    form.Height = Cursor.Position.Y - y_coef;
                    form.Location = new Point(Cursor.Position.X, y_coef);
                    break;
                case 3:
                    form.Width = Cursor.Position.X - x_coef;
                    form.Height = y_coef - Cursor.Position.Y;
                    form.Location = new Point(x_coef, Cursor.Position.Y);
                    break;
                case 4:
                    form.Width = x_coef - Cursor.Position.X;
                    form.Location = new Point(Cursor.Position.X, form.Location.Y);
                    break;
                case 5:
                    form.Width = Cursor.Position.X - x_coef;
                    break;
                case 6:
                    form.Height = y_coef - Cursor.Position.Y;
                    form.Location = new Point(form.Location.X, Cursor.Position.Y);
                    break;
                case 7:
                    form.Height = Cursor.Position.Y - y_coef;
                    break;
                case 8:
                    form.Location = new Point(Cursor.Position.X - x_coef, Cursor.Position.Y - y_coef);
                    break;
                case 9:
                    if ((form.Width >= Screen.PrimaryScreen.WorkingArea.Width && form.Height >= Screen.PrimaryScreen.WorkingArea.Height) || counter == resize_speed)
                    {
                        form.Width = Screen.PrimaryScreen.WorkingArea.Width;
                        form.Height = Screen.PrimaryScreen.WorkingArea.Height;
                        form.Location = new Point(0, 0);
                        MaximizePicture.BackgroundImage = Properties.Resources.MinimizeIcon__2_;
                        ActionTimer.Enabled = false;
                    }
                    else
                    {
                        form.Location = new Point(Convert.ToInt32(Convert.ToDouble(form.Location.X) - resize_coefs[0]), Convert.ToInt32(Convert.ToDouble(form.Location.Y) - resize_coefs[1]));
                        form.Width += Convert.ToInt32(resize_coefs[0] + resize_coefs[2]);
                        form.Height += Convert.ToInt32(resize_coefs[1] + resize_coefs[3]);
                        counter++;
                    }
                    break;
                case 10:
                    if ((form.Width <= x_coef && form.Height <= y_coef) || counter == resize_speed)
                    {
                        form.Width = x_coef;
                        form.Height = y_coef;
                        form.Location = new Point(x_pos, y_pos);
                        MaximizePicture.BackgroundImage = Properties.Resources.Maximize3;
                        maximized = false;
                        ActionTimer.Enabled = false;
                    }
                    else
                    {
                        form.Location = new Point(Convert.ToInt32(Convert.ToDouble(form.Location.X) + resize_coefs[0]), Convert.ToInt32(Convert.ToDouble(form.Location.Y) + resize_coefs[1]));
                        form.Width -= (Convert.ToInt32(resize_coefs[0] + resize_coefs[2]));
                        form.Height -= (Convert.ToInt32(resize_coefs[1] + resize_coefs[3]));
                        counter++;
                    }
                    break;
                case 11:
                    if (counter < resize_speed)
                    {
                        form.Location = new Point(Convert.ToInt32(Convert.ToDouble(form.Location.X) + resize_coefs[0]), Convert.ToInt32(Convert.ToDouble(form.Location.Y) + resize_coefs[1]));
                        form.Width = Convert.ToInt32(Convert.ToDouble(form.Width) - resize_coefs[2]);
                        form.Height = Convert.ToInt32(Convert.ToDouble(form.Height) - resize_coefs[3]);
                    }
                    else if (counter == resize_speed)
                    {
                        form.Width = x_coef;
                        form.Height = y_coef;
                        form.Location = new Point(x_pos, y_pos);
                    }
                    else if (counter == resize_speed + 1)
                    {
                        ActionTimer.Enabled = false;
                        form.WindowState = FormWindowState.Minimized;
                    }
                    counter++;
                    break;
                case 12:
                    if (counter == resize_speed)
                        Application.Exit();
                    else
                    {
                        form.Size = new Size(x_coef - Convert.ToInt32(resize_coefs[0] * counter), y_coef - Convert.ToInt32(resize_coefs[1] * counter));
                        form.Location = new Point(x_pos + Convert.ToInt32(resize_coefs[2] * counter), y_pos + Convert.ToInt32(resize_coefs[3] * counter));
                        form.Opacity = (resize_speed - counter) / (double)resize_speed;
                        counter++;
                    }
                    break;
                case 13:
                    counter++;
                    foreach (KeyValuePair<UserControl, Point> elem in animation_ucs)
                    {
                        elem.Key.Location = new Point(Convert.ToInt32(elem.Value.X + xGap * counter), Convert.ToInt32(elem.Value.Y + yGap * counter));
                        ChosenTabPanel.Width = Convert.ToInt32(resize_coefs[0] + resize_coefs[2] * counter);
                        ChosenTabPanel.Location = new Point(Convert.ToInt32(resize_coefs[1] + resize_coefs[3] * counter), NavPanel.Height - ChosenTabPanel.Height);
                    }
                    if (counter == navbar_speed)
                    {
                        int dict_length = animation_ucs.Count;
                        for (int i = 0; i < dict_length - 1; i++)
                            ContentPanel.Controls.Remove(animation_ucs.Keys.ElementAt(i));
                        animation_ucs.Keys.ElementAt(dict_length - 1).Location = new Point(0, 0);
                        tabIndex = helpTabIndex;
                        FixChosenTabPanel();
                        ActionTimer.Enabled = false;
                    }
                    break;
            }
        }


        private void GeneralMouseDown(object sender, MouseEventArgs e)
        {
            if (maximized)
                return;
            if (CheckLeft() && CheckTop())
            {
                what_to_do = 0;
                x_coef = form.Location.X + form.Width;
                y_coef = form.Location.Y + form.Height;
            }
            else if (CheckRight() && CheckBottom())
            {
                what_to_do = 1;
                x_coef = form.Location.X;
                y_coef = form.Location.Y;
            }
            else if (CheckLeft() && CheckBottom())
            {
                what_to_do = 2;
                x_coef = form.Location.X + form.Width;
                y_coef = form.Location.Y;
            }
            else if (CheckRight() && CheckTop())
            {
                what_to_do = 3;
                x_coef = form.Location.X;
                y_coef = form.Location.Y + form.Height;
            }
            else if (CheckLeft())
            {
                what_to_do = 4;
                x_coef = form.Location.X + form.Width;
                //y_coef = this.Location.Y;
            }
            else if (CheckRight())
            {
                what_to_do = 5;
                x_coef = form.Location.X;
                //y_coef = this.Location.Y;
            }
            else if (CheckTop())
            {
                what_to_do = 6;
                //x_coef = this.Location.X;
                y_coef = form.Location.Y + form.Height;
            }
            else if (CheckBottom())
            {
                what_to_do = 7;
                //x_coef = this.Location.X;
                y_coef = form.Location.Y;
            }
            else
            {
                what_to_do = 8;
                x_coef = Cursor.Position.X - form.Location.X;
                y_coef = Cursor.Position.Y - form.Location.Y;
            }
            ActionTimer.Enabled = true;
        }


        private void GeneralMouseUp(object sender, MouseEventArgs e)
        {
            ActionTimer.Enabled = false;
        }


        private void Close(object sender, EventArgs e)
        {
            x_coef = form.Width;
            y_coef = form.Height;
            x_pos = form.Location.X;
            y_pos = form.Location.Y;
            counter = 0;
            resize_coefs = new double[4] { form.Width / (resize_speed * 1.00d), form.Height / (resize_speed * 1.00d), (form.Width / (resize_speed * 1.00d)) / 2, (form.Height / (resize_speed * 1.00d)) / 2 };
            what_to_do = 12;
            ActionTimer.Enabled = true;
        }

        private void Minimize(object sender, EventArgs e)
        {
            x_coef = form.Width;
            y_coef = form.Height;
            x_pos = form.Location.X;
            y_pos = form.Location.Y;
            resize_coefs = new double[4] { (form.Width / 2) / (resize_speed * 1.00d), (Screen.PrimaryScreen.WorkingArea.Height - form.Location.Y) / (resize_speed * 1.00d), (form.Width - 1) / (resize_speed * 1.00d), (form.Height - 1) / (resize_speed * 1.00d) };
            counter = 0;
            what_to_do = 11;
            ActionTimer.Enabled = true;
        }


        private void Maximize(object sender, EventArgs e)
        {
            PictureBox pic = new PictureBox();
            pic.BackgroundImage = Properties.Resources.Maximize3;
            switch (maximized)
            {
                case false:
                    x_coef = form.Width;
                    y_coef = form.Height;
                    x_pos = form.Location.X;
                    y_pos = form.Location.Y;
                    resize_coefs = new double[4] { form.Location.X / (resize_speed * 1.00d), form.Location.Y / (resize_speed * 1.00d), (Screen.PrimaryScreen.WorkingArea.Width - (form.Location.X + form.Width)) / (resize_speed * 1.00d), (Screen.PrimaryScreen.WorkingArea.Height - (form.Location.Y + form.Height)) / (resize_speed * 1.00d) };
                    maximized = true;
                    MaximizePicture.BackgroundImage = Properties.Resources.MinimizeIcon__2_;
                    counter = 0;
                    what_to_do = 9;
                    ActionTimer.Enabled = true;
                    break;
                case true:
                    counter = 0;
                    what_to_do = 10;
                    ActionTimer.Enabled = true;
                    break;
            }
        }


        public void GoToSpecificPage(UserControl[] formsToScroll, int axis, int positive)
        {
            counter = 0;


            double[] arr1 = new double[2] { xGap, yGap };
            int[] arr2 = new int[2] { formsToScroll[0].Width, formsToScroll[0].Height };


            arr1[axis] = (formsToScroll.Length - 1) * 1.00d * arr2[axis] / navbar_speed * positive;
            arr1[1 - axis] = 0;


            xGap = arr1[0];
            yGap = arr1[1];


            formsToScroll[0].Location = new Point(0, 0);


            ContentPanel.Controls.Clear();
            animation_ucs.Clear();


            for (int i = 0; i < formsToScroll.Length; i++)
            {
                formsToScroll[i].Size = new Size(formsToScroll[0].Width, formsToScroll[0].Height);
                formsToScroll[i].Location = new Point(i * Math.Abs(Convert.ToInt32(xGap * navbar_speed / (formsToScroll.Length - 1))) * (-1) * positive, i * Math.Abs(Convert.ToInt32(yGap * navbar_speed / (formsToScroll.Length - 1))) * (-1) * positive);
                ContentPanel.Controls.Add(formsToScroll[i]);
                formsToScroll[i].SendToBack();
                animation_ucs.Add(formsToScroll[i], formsToScroll[i].Location);
            }

            what_to_do = 13;
            ActionTimer.Enabled = true;
        }


        public void TabLabelPress(object sender, EventArgs e)
        {
            TabButtonPress((sender as Label).Parent as Panel);
        }


        private void TabPanelPress(object sender, EventArgs e)
        {
            TabButtonPress(sender as Panel);
        }


        public void TabButtonPress(Panel panel)
        {
            helpTabIndex = tab_panels.IndexOf(panel);
            if (helpTabIndex == -1 || helpTabIndex == tabIndex)
                return;
            resize_coefs = new double[4] { ChosenTabPanel.Width, ChosenTabPanel.Location.X, (tab_panels[helpTabIndex].Width - ChosenTabPanel.Width) * 1.00d / navbar_speed, (tab_panels[helpTabIndex].Location.X - ChosenTabPanel.Location.X) * 1.00d / navbar_speed };
            List<UserControl> ucs = new List<UserControl>();
            switch (helpTabIndex > tabIndex)
            {
                case true:
                    for (int i = tabIndex; i < helpTabIndex + 1; i++)
                        ucs.Add(tabs[i]);
                    GoToSpecificPage(ucs.ToArray(), 0, -1);
                    break;
                case false:
                    for (int i = tabIndex; i > helpTabIndex - 1; i--)
                        ucs.Add(tabs[i]);
                    GoToSpecificPage(ucs.ToArray(), 0, 1);
                    break;
            }
        }


        public void FixChosenTabPanel()
        {
            ChosenTabPanel.Width = tab_panels[tabIndex].Width;
            ChosenTabPanel.Location = new Point(tab_panels[tabIndex].Location.X, NavPanel.Height - ChosenTabPanel.Height);
            ChosenTabPanel.BringToFront();
        }


        public void UnableCursor()
        {
            CursorTimer.Enabled = false;
        }


        public void EnableCursor()
        {
            CursorTimer.Enabled = true;
        }


        public void HoverButtonWithoutColor(object sender, EventArgs e)
        {
            CursorTimer.Enabled = false;
            form.Cursor = Cursors.Hand;
        }


        public void UnhoverButtonWithoutColor(object sender, EventArgs e)
        {
            CursorTimer.Enabled = true;
        }


        public void HoverTextbox(object sender, EventArgs e)
        {
            UnableCursor();
        }

        public void UnhoverTextbox(object sender, EventArgs e)
        {
            EnableCursor();
        }
    }
}

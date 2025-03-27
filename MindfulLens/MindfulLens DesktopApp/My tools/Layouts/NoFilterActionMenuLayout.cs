using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MindfulLens_DesktopApp.My_tools
{
    public class NoFilterActionMenuLayout
    {
        private GraphicFeatures graphicFeatures;
        //private Panel SearchBlackPanel;
        //private Panel SearchIconBlackPanel;
        private Label SortByLabel;
        public ScrollPanel layout { get; private set; }
        private UserControl userControl;
        public SearchSet searchSet {  get; private set; }
        public CustomComboBox SortByBlackPanel { get; private set; }
        public CustomButton AddNewButton { get; private set; }
        //private Panel SortByBlackPanel;
        //private Label FilterByLabel;
        //private Panel FilterByBlackPanel;

        public NoFilterActionMenuLayout(GraphicFeatures graphicFeatures, UserControl userControl, EventHandler showAllFunc, EventHandler searchFunc, EventHandler sortFunc, string[] sort_methods)
        {
            this.graphicFeatures = graphicFeatures;
            this.userControl = userControl;


            searchSet = new SearchSet(new Point(48, 23), new Point(268, 12), userControl, graphicFeatures, searchFunc);
            //SearchBlackPanel = new Panel();
            //SearchBlackPanel.BackColor = Color.Black;
            //SearchBlackPanel.Location = new Point(14, 29);
            //SearchBlackPanel.Size = new Size(204, 39);


            //TextBox searchTextBox = new TextBox();
            //searchTextBox.BorderStyle = BorderStyle.Fixed3D;
            //searchTextBox.PlaceholderText = "Search";
            //searchTextBox.Font = new Font("Segoe UI", 15.75f);
            //searchTextBox.Size = new Size(200, 35);
            //searchTextBox.Location = new Point(2, 2);
            //SearchBlackPanel.Controls.Add(searchTextBox);


            //SearchIconBlackPanel = new Panel();
            //SearchIconBlackPanel.BackColor = Color.Black;
            //SearchIconBlackPanel.Size = new Size(60, 60);
            //SearchIconBlackPanel.Location = new Point(234, 18);


            //Panel searchIconPanel = new Panel();
            //searchIconPanel.BackColor = Color.White;
            //searchIconPanel.Size = new Size(56, 56);
            //searchIconPanel.Location = new Point(2, 2);
            //SearchIconBlackPanel.Controls.Add(searchIconPanel);


            //PictureBox searchIcon = new PictureBox();
            //searchIcon.Size = new Size(44, 44);
            //searchIcon.Location = new Point(6, 6);
            //searchIcon.BackgroundImageLayout = ImageLayout.Zoom;
            //searchIcon.BackgroundImage = Properties.Resources.Group_2;
            //searchIconPanel.Controls.Add(searchIcon);


            CustomButton show_all = new CustomButton("Show all", new Size(160, 60), new Font("Segoe UI", 16), new Point(404, 12), graphicFeatures);
            show_all.BindFunction(showAllFunc);


            SortByLabel = new Label();
            SortByLabel.Text = "Sort by: ";
            SortByLabel.AutoSize = true;
            SortByLabel.Font = new Font("Segoe UI", 15.75f);
            SortByLabel.ForeColor = Color.White;
            SortByLabel.Location = new Point(627, 30);


            SortByBlackPanel = new CustomComboBox(new Point(710, 28), "Choose option", userControl, graphicFeatures, sort_methods);
            SortByBlackPanel.comboBox.SelectedIndexChanged += sortFunc;
            //SortByBlackPanel = new Panel();
            //SortByBlackPanel.BackColor = Color.Black;
            //SortByBlackPanel.Size = new Size(163, 42);
            //SortByBlackPanel.Location = new Point(536, 37);


            //ComboBox sortByComboBox = new ComboBox();
            //sortByComboBox.FlatStyle = FlatStyle.Flat;
            //sortByComboBox.Font = new Font("Segoe UI", 15.75f);
            //sortByComboBox.Size = new Size(159, 38);
            //sortByComboBox.Location = new Point(2, 2);
            //sortByComboBox.Text = "Sort by";
            //SortByBlackPanel.Controls.Add(sortByComboBox);


            //FilterByLabel = new Label();
            //FilterByLabel.Text = "Filter by: ";
            //FilterByLabel.AutoSize = true;
            //FilterByLabel.Font = new Font("Segoe UI", 15.75f);
            //FilterByLabel.ForeColor = Color.White;
            //FilterByLabel.Location = new Point(773, 4);


            //CustomComboBox FilterComboBox = new CustomComboBox(new Point(739, 37), "Filter by", userControl, graphicFeatures);
            //FilterByBlackPanel = new Panel();
            //FilterByBlackPanel.BackColor = Color.Black;
            //FilterByBlackPanel.Size = new Size(163, 42);
            //FilterByBlackPanel.Location = new Point(739, 37);


            //ComboBox filterByComboBox = new ComboBox();
            //filterByComboBox.FlatStyle = FlatStyle.Flat;
            //filterByComboBox.Font = new Font("Segoe UI", 15.75f);
            //filterByComboBox.Size = new Size(159, 38);
            //filterByComboBox.Location = new Point(2, 2);
            //filterByComboBox.Text = "Sort by";
            //FilterByBlackPanel.Controls.Add(filterByComboBox);


            AddNewButton = new CustomButton("Add new", new Size(160, 60), new Font("Segoe UI", 15.75f), new Point(740, 398), graphicFeatures);


            //userControl.Controls.Add(SearchBlackPanel);
            //userControl.Controls.Add(SearchIconBlackPanel);
            userControl.Controls.Add(show_all);
            userControl.Controls.Add(SortByLabel);
            userControl.Controls.Add(SortByBlackPanel);
            SortByBlackPanel.BringToFront();
            //userControl.Controls.Add(container);
            //userControl.Controls.Add(FilterByLabel);
            userControl.Controls.Add(AddNewButton);
            //userControl.Controls.Add(FilterByBlackPanel);
        }

        public void FillContent(ScrollPanel layout)
        {
            try
            {
                userControl.Controls.Remove(this.layout);
            }
            catch { }
            this.layout = layout;
            userControl.Controls.Add(this.layout);
        }
    }
}

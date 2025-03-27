using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MindfulLens_DesktopApp.My_tools
{
    public class FullActionMenuLayout
    {
        private GraphicFeatures graphicFeatures;
        //private Panel SearchBlackPanel;
        //private Panel SearchIconBlackPanel;
        private Label SortByLabel;
        //private Panel SortByBlackPanel;
        private Label FilterByLabel;
        //private Panel FilterByBlackPanel;

        public CustomComboBox FilterComboBox { get; private set; }
        public CustomButton AddNewButton { get; private set; }
        public CustomButton showAllButton { get; private set; }
        public CustomComboBox SortByBlackPanel { get; private set; }
        public SearchSet searchSet {  get; private set; }
        private UserControl userControl;
        private int padding = 14;
        //private GraphicTitledPublicationsContainer Container;
        public EqualRowHeightScroll cognitiveContentScroll { get; private set; }

        public FullActionMenuLayout(GraphicFeatures graphicFeatures, UserControl userControl, EventHandler showAllFunc, EventHandler searchFunc, EventHandler sortFunc, EventHandler filterFunc, string[] sort_methods, string[] filter_methods)
        {
            this.graphicFeatures = graphicFeatures;
            this.userControl = userControl;

            searchSet = new SearchSet(new Point(padding, 29), new Point(234, 18), userControl, graphicFeatures, searchFunc);
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


            showAllButton = new CustomButton("Show all", new Size(160, 60), new Font("Segoe UI", 16), new Point(337, 18), graphicFeatures);
            showAllButton.BindFunction(showAllFunc);


            SortByLabel = new Label();
            SortByLabel.Text = "Sort by: ";
            SortByLabel.AutoSize = true;
            SortByLabel.Font = new Font("Segoe UI", 15.75f);
            SortByLabel.ForeColor = Color.White;
            SortByLabel.Location = new Point(575, 4);


            SortByBlackPanel = new CustomComboBox(new Point(536, 37), "Choose option", userControl, graphicFeatures, sort_methods);
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


            FilterByLabel = new Label();
            FilterByLabel.Text = "Filter by: ";
            FilterByLabel.AutoSize = true;
            FilterByLabel.Font = new Font("Segoe UI", 15.75f);
            FilterByLabel.ForeColor = Color.White;
            FilterByLabel.Location = new Point(773, 4);


            FilterComboBox = new CustomComboBox(new Point(739, 37), "Choose option", userControl, graphicFeatures, filter_methods);
            FilterComboBox.comboBox.SelectedIndexChanged += filterFunc;
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
            //MessageBox.Show($"{AddNewButton.Location.Y - (FilterComboBox.Location.Y + FilterComboBox.Height) - 20} {FilterComboBox.Location.Y + FilterComboBox.Height + 10}");


            //Container = new GraphicTitledPublicationsContainer(new Size(userControl.Width - padding * 2, AddNewButton.Location.Y - (FilterComboBox.Location.Y + FilterComboBox.Height)), new Point(padding, FilterComboBox.Location.Y + FilterComboBox.Height));
            //cognitiveContentScroll = new EqualRowHeightScroll(new Size(userControl.Width - padding * 2, AddNewButton.Location.Y - (FilterComboBox.Location.Y + FilterComboBox.Height) - 20), new Point(padding, FilterComboBox.Location.Y + FilterComboBox.Height + 10));

            //userControl.Controls.Add(SearchBlackPanel);
            //userControl.Controls.Add(SearchIconBlackPanel);
            userControl.Controls.Add(showAllButton);
            userControl.Controls.Add(SortByLabel);
            userControl.Controls.Add(SortByBlackPanel);
            userControl.Controls.Add(FilterByLabel);
            //userControl.Controls.Add(Container);
            userControl.Controls.Add(cognitiveContentScroll);
            userControl.Controls.Add(AddNewButton);
            //userControl.Controls.Add(FilterByBlackPanel);
        }

        public void FillContent(GraphicTitledPublication[] content, bool showMoreButton, EventHandler loadMoreFunction)
        {
            //int value = cognitiveContentScroll.ScrollBar.Value / 10;
            userControl.Controls.Remove(cognitiveContentScroll);
            cognitiveContentScroll = new EqualRowHeightScroll(new Size(userControl.Width - padding * 2, AddNewButton.Location.Y - (FilterComboBox.Location.Y + FilterComboBox.Height) - 20), new Point(padding, FilterComboBox.Location.Y + FilterComboBox.Height + 10), content, graphicFeatures, showMoreButton, loadMoreFunction);
            //cognitiveContentScroll.ShowCurrentContent(value);
            userControl.Controls.Add(cognitiveContentScroll);
        }


    }
}

using MindfulLens_DesktopApp.My_tools.Other_stuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.My_tools
{
    public class SearchSet
    {
        public CustomTextBox searchTextBox;
        public SearchSet(Point searchTextBoxLocation, Point SearchIconLocation, UserControl userControl, GraphicFeatures graphicFeatures, EventHandler searchFunc)
        {
            searchTextBox = new CustomTextBox(Color.Black, Color.White, Color.Black, "Search", new Size(204, 39), searchTextBoxLocation, new Font("Segoe UI", 15.75f), graphicFeatures, true);
            //Panel SearchBlackPanel = new Panel();
            //SearchBlackPanel.BackColor = Color.Black;
            //SearchBlackPanel.Location = searchTextBoxLocation;
            //SearchBlackPanel.Size = new Size(204, 39);


            //TextBox searchTextBox = new TextBox();
            //searchTextBox.BorderStyle = BorderStyle.Fixed3D;
            //searchTextBox.PlaceholderText = "Search";
            //searchTextBox.Font = new Font("Segoe UI", 15.75f);
            //searchTextBox.Size = new Size(200, 35);
            //searchTextBox.Location = new Point(2, 2);
            //searchTextBox.MouseEnter += graphicFeatures.HoverTextbox;
            //searchTextBox.MouseLeave += graphicFeatures.UnhoverTextbox;
            //SearchBlackPanel.Controls.Add(searchTextBox);


            Panel SearchIconBlackPanel = new Panel();
            SearchIconBlackPanel.BackColor = Color.Black;
            SearchIconBlackPanel.Size = new Size(60, 60);
            SearchIconBlackPanel.Location = SearchIconLocation;


            Panel searchIconPanel = new Panel();
            searchIconPanel.BackColor = Color.White;
            searchIconPanel.Size = new Size(56, 56);
            searchIconPanel.Location = new Point(2, 2);
            searchIconPanel.MouseEnter += graphicFeatures.HoverButtonWithoutColor;
            searchIconPanel.MouseLeave += graphicFeatures.UnhoverButtonWithoutColor;
            searchIconPanel.Click += searchFunc;
            SearchIconBlackPanel.Controls.Add(searchIconPanel);


            PictureBox searchIcon = new PictureBox();
            searchIcon.Size = new Size(44, 44);
            searchIcon.Location = new Point(6, 6);
            searchIcon.BackgroundImageLayout = ImageLayout.Zoom;
            searchIcon.BackgroundImage = Properties.Resources.Group_2;
            searchIcon.MouseEnter += graphicFeatures.HoverButtonWithoutColor;
            searchIcon.MouseLeave += graphicFeatures.UnhoverButtonWithoutColor;
            searchIcon.Click += searchFunc;
            searchIconPanel.Controls.Add(searchIcon);


            userControl.Controls.Add(searchTextBox);
            userControl.Controls.Add(SearchIconBlackPanel);
        }
    }
}

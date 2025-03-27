using Logic_Layer.Classes;
using Logic_Layer;
using Logic_Layer.Factories;
using Logic_Layer.Managers;
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
    public partial class CustomTheoriesUserControl : UserControl
    {
        private GraphicFeatures graphicFeatures;
        private int load_number;
        private const int load_step = 3;
        public NoFilterActionMenuLayout noFilterActionMenuLayout { get; private set; }
        private CognitivePartManager cognitivePartManager;
        private EventHandler cognitivePartFunction;
        private string? search_query, sort_method, sort_order = null;
        private bool showMoreButton;
        private (string, string)[] sortings;
        private SortFactory sortFactory;
        private string[] sort_methods;
        public CustomTheoriesUserControl(GraphicFeatures graphicFeatures, EventHandler cognitivePartFunc, CognitivePartManager cognitivePartManager)
        {
            InitializeComponent();

            load_number = load_step;
            this.graphicFeatures = graphicFeatures;
            this.cognitivePartManager = cognitivePartManager;
            this.cognitivePartFunction = cognitivePartFunc;

            this.sort_methods = new string[4] { "Alphabet (A-Z)", "Alphabet (Z-A)", "Time (from newest)", "Time (from oldest)" };

            this.sortFactory = new AlphabetAndTimeSortFactory();

            sortings = new (string, string)[4] { ("alphabet", "asc"), ("alphabet", "desc"), ("time", "desc"), ("time", "asc") };

            noFilterActionMenuLayout = new NoFilterActionMenuLayout(graphicFeatures, this, ShowAll, Search, Sort, sort_methods);

            FillInCognitiveParts();
            //NoFilterActionMenuLayout noFilterActionMenuLayout = new NoFilterActionMenuLayout(graphicFeatures, this); 
        }

        public int FillInCognitiveParts()
        {
            int padding = 14;
            CognitivePart[] cognitiveParts = cognitivePartManager.GetCognitiveParts(search_query, sort_method, sort_order, false, "Theory", load_number + 1);
            showMoreButton = CommonData.CheckForShowMoreButton(ref cognitiveParts, load_number);

            GraphicTheoryMethod[] graphicCognitiveParts = new GraphicTheoryMethod[cognitiveParts.Length];
            for (int i = 0; i < cognitiveParts.Length; i++)
            {
                graphicCognitiveParts[i] = new GraphicTheoryMethod(cognitiveParts[i], graphicFeatures, cognitivePartFunction);
            }
            EqualRowHeightScroll equalRowHeightScroll = new EqualRowHeightScroll(new Size(this.Width - padding * 2, noFilterActionMenuLayout.AddNewButton.Location.Y - (noFilterActionMenuLayout.SortByBlackPanel.Location.Y + noFilterActionMenuLayout.SortByBlackPanel.Height) - 20), new Point(padding, noFilterActionMenuLayout.SortByBlackPanel.Location.Y + noFilterActionMenuLayout.SortByBlackPanel.Height + 10), graphicCognitiveParts, graphicFeatures, showMoreButton, LoadMoreFunction);
            noFilterActionMenuLayout.FillContent(equalRowHeightScroll);
            return 1;
        }

        private void LoadMoreFunction(object sender, EventArgs e)
        {
            load_number += load_step;
            int value = noFilterActionMenuLayout.layout.ScrollBar.Value;
            FillInCognitiveParts();
            noFilterActionMenuLayout.layout.ScrollBar.Value = value;
            noFilterActionMenuLayout.layout.ShowCurrentContent(noFilterActionMenuLayout.layout.ScrollBar.Value / 10);
        }

        private void ShowAll(object sender, EventArgs e)
        {
            search_query = null;
            sort_method = null;
            sort_order = null;
            load_number = load_step;
            noFilterActionMenuLayout.searchSet.searchTextBox.textBox.Text = string.Empty;
            noFilterActionMenuLayout.SortByBlackPanel.comboBox.SelectedIndex = -1;
            noFilterActionMenuLayout.SortByBlackPanel.comboBox.SelectedItem = null;
            noFilterActionMenuLayout.SortByBlackPanel.comboBox.Text = "Choose option";
            FillInCognitiveParts();
        }

        private void Search(object sender, EventArgs e)
        {
            search_query = noFilterActionMenuLayout.searchSet.searchTextBox.textBox.Text;
            load_number = load_step;
            FillInCognitiveParts();
        }

        private void Sort(object sender, EventArgs e)
        {
            if (noFilterActionMenuLayout.SortByBlackPanel.comboBox.SelectedIndex != -1)
            {
                (string, string) a = sortings[noFilterActionMenuLayout.SortByBlackPanel.comboBox.SelectedIndex];
                Tuple<string, string>? sort_setup = sortFactory.GetSorter(a.Item1, a.Item2);
                if (sort_setup != null)
                {
                    sort_method = sort_setup.Item1;
                    sort_order = sort_setup.Item2;
                }
                else
                {
                    sort_method = null;
                    sort_order = null;
                }
            }
            load_number = load_step;
            FillInCognitiveParts();
        }
    }
}

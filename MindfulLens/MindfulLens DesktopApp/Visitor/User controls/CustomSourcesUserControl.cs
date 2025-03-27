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
    public partial class CustomSourcesUserControl : UserControl
    {
        private GraphicFeatures graphicFeatures;
        private int load_number;
        private const int load_step = 3;
        public NoFilterActionMenuLayout noFilterActionMenuLayout { get; private set; }
        private SourceManager sourceManager;
        private EventHandler sourceFunction;
        private string? search_query, sort_method, sort_order = null;
        private bool showMoreButton;
        private (string, string)[] sortings;
        private SortFactory sortFactory;
        private string[] sort_methods;
        public CustomSourcesUserControl(GraphicFeatures graphicFeatures, EventHandler sourceFunction, SourceManager sourceManager)
        {
            InitializeComponent();

            load_number = load_step;
            this.graphicFeatures = graphicFeatures;
            this.sourceManager = sourceManager;
            this.sourceFunction = sourceFunction;

            this.sort_methods = new string[4] { "Alphabet (A-Z)", "Alphabet (Z-A)", "Time (from newest)", "Time (from oldest)" };

            this.sortFactory = new AlphabetAndTimeSortFactory();

            sortings = new (string, string)[4] { ("alphabet", "asc"), ("alphabet", "desc"), ("time", "desc"), ("time", "asc") };

            noFilterActionMenuLayout = new NoFilterActionMenuLayout(graphicFeatures, this, ShowAll, Search, Sort, sort_methods);

            FillInSources();
            //NoFilterActionMenuLayout noFilterActionMenuLayout = new NoFilterActionMenuLayout(graphicFeatures, this);
        }

        public int FillInSources()
        {
            int padding = 14;
            Source[] sources = sourceManager.GetSources(search_query, sort_method, sort_order, false, load_number + 1);
            showMoreButton = CommonData.CheckForShowMoreButton(ref sources, load_number);

            GraphicSource[] graphicSources = new GraphicSource[sources.Length];
            for (int i = 0; i < sources.Length; i++)
            {
                graphicSources[i] = new GraphicSource(sources[i], graphicFeatures, sourceFunction);
            }
            EqualRowHeightScroll equalRowHeightScroll = new EqualRowHeightScroll(new Size(this.Width - padding * 2, noFilterActionMenuLayout.AddNewButton.Location.Y - (noFilterActionMenuLayout.SortByBlackPanel.Location.Y + noFilterActionMenuLayout.SortByBlackPanel.Height) - 20), new Point(padding, noFilterActionMenuLayout.SortByBlackPanel.Location.Y + noFilterActionMenuLayout.SortByBlackPanel.Height + 10), graphicSources, graphicFeatures, showMoreButton, LoadMoreFunction);
            noFilterActionMenuLayout.FillContent(equalRowHeightScroll);
            return 1;
            //noFilterActionMenuLayout.FillContent(graphicSources, showMoreButton, LoadMoreFunction);
            //return 1;
        }


        private void LoadMoreFunction(object sender, EventArgs e)
        {
            load_number += load_step;
            int value = noFilterActionMenuLayout.layout.ScrollBar.Value;
            FillInSources();
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
            FillInSources();
        }

        private void Search(object sender, EventArgs e)
        {
            search_query = noFilterActionMenuLayout.searchSet.searchTextBox.textBox.Text;
            load_number = load_step;
            FillInSources();
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
            FillInSources();
        }
    }
}

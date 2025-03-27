using Logic_Layer;
using Logic_Layer.Classes;
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
    public partial class BiasesUserControl : UserControl
    {
        private GraphicFeatures graphicFeatures;
        private int load_number;
        private const int load_step = 3;
        public FullActionMenuLayout fullActionMenuLayout { get; private set; }
        private CognitivePartManager cognitivePartManager;
        private EventHandler cognitivePartFunction;
        private string? search_query, sort_method, sort_order = null;
        private bool? isOfficial = null;
        private bool showMoreButton;
        private (string, string)[] sortings;
        private SortFactory sortFactory;
        private string[] sort_methods;
        private string[] filter_methods;

        public BiasesUserControl(GraphicFeatures graphicFeatures, EventHandler cognitivePartFunction, CognitivePartManager cognitivePartManager)
        {
            InitializeComponent();
            this.Name = "Biases";

            load_number = load_step;
            this.graphicFeatures = graphicFeatures;
            this.cognitivePartManager = cognitivePartManager;
            this.cognitivePartFunction = cognitivePartFunction;

            this.sort_methods = new string[4] { "Alphabet (A-Z)", "Alphabet (Z-A)", "Time (from newest)", "Time (from oldest)" };
            this.filter_methods = new string[2] { "Official", "Custom" };

            this.sortFactory = new AlphabetAndTimeSortFactory();

            sortings = new (string, string)[4] { ("alphabet", "asc"), ("alphabet", "desc"), ("time", "desc"), ("time", "asc") };

            fullActionMenuLayout = new FullActionMenuLayout(graphicFeatures, this, ShowAll, Search, Sort, Filter, sort_methods, filter_methods);

            FillInCognitiveParts();
        }

        public int FillInCognitiveParts()
        {
            CognitivePart[] cognitiveParts = cognitivePartManager.GetCognitiveParts(search_query, sort_method, sort_order, isOfficial, "Bias", load_number + 1);
            showMoreButton = CommonData.CheckForShowMoreButton(ref cognitiveParts, load_number);

            GraphicBias[] graphicCognitiveParts = new GraphicBias[cognitiveParts.Length];
            for (int i = 0; i < cognitiveParts.Length; i++)
            {
                graphicCognitiveParts[i] = new GraphicBias(cognitiveParts[i], graphicFeatures, cognitivePartFunction);
            }
            fullActionMenuLayout.FillContent(graphicCognitiveParts, showMoreButton, LoadMoreFunction);
            return 1;
        }

        private void LoadMoreFunction(object sender, EventArgs e)
        {
            load_number += load_step;
            int value = fullActionMenuLayout.cognitiveContentScroll.ScrollBar.Value;
            FillInCognitiveParts();
            fullActionMenuLayout.cognitiveContentScroll.ScrollBar.Value = value;
            fullActionMenuLayout.cognitiveContentScroll.ShowCurrentContent(fullActionMenuLayout.cognitiveContentScroll.ScrollBar.Value / 10);
        }

        private void ShowAll(object sender, EventArgs e)
        {
            search_query = null;
            sort_method = null;
            sort_order = null;
            isOfficial = null;
            load_number = load_step;
            fullActionMenuLayout.searchSet.searchTextBox.textBox.Text = string.Empty;
            fullActionMenuLayout.SortByBlackPanel.comboBox.SelectedIndex = -1;
            fullActionMenuLayout.SortByBlackPanel.comboBox.SelectedItem = null;
            fullActionMenuLayout.SortByBlackPanel.comboBox.Text = "Choose option";
            fullActionMenuLayout.FilterComboBox.comboBox.SelectedIndex = -1;
            fullActionMenuLayout.FilterComboBox.comboBox.SelectedItem = null;
            fullActionMenuLayout.FilterComboBox.comboBox.Text = "Choose option";
            FillInCognitiveParts();
        }

        private void Search(object sender, EventArgs e)
        {
            search_query = fullActionMenuLayout.searchSet.searchTextBox.textBox.Text;
            load_number = load_step;
            FillInCognitiveParts();
        }

        private void Sort(object sender, EventArgs e)
        {
            if (fullActionMenuLayout.SortByBlackPanel.comboBox.SelectedIndex != -1)
            {
                (string, string) a = sortings[fullActionMenuLayout.SortByBlackPanel.comboBox.SelectedIndex];
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

        private void Filter(object sender, EventArgs e)
        {
            load_number = load_step;
            if (fullActionMenuLayout.FilterComboBox.comboBox.SelectedIndex != -1)
            {
                string filter_way = fullActionMenuLayout.FilterComboBox.comboBox.SelectedItem.ToString();
                switch (filter_way)
                {
                    case "Official":
                        isOfficial = true;
                        break;
                    case "Custom":
                        isOfficial = false;
                        break;
                    default:
                        break;
                }
            }
            FillInCognitiveParts();
        }
    }
}

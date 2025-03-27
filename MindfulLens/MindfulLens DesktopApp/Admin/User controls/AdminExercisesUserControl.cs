using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logic_Layer;
using Logic_Layer.Classes;
using Logic_Layer.Factories;
using Logic_Layer.Managers;
using MindfulLens_DesktopApp.My_tools;

namespace MindfulLens_DesktopApp.User_controls
{
    public partial class AdminExercisesUserControl : UserControl
    {
        private GraphicFeatures graphicFeatures;
        private int load_number;
        private const int load_step = 3;
        public FullActionMenuLayout fullActionMenuLayout { get; private set; }
        private ExerciseManager exerciseManager;
        private EventHandler exerciseFunction;
        private string? search_query, sort_method, sort_order = null;
        private bool? isOfficial = null;
        private bool showMoreButton;
        private (string, string)[] sortings;
        private SortFactory sortFactory;
        private string[] sort_methods;
        private string[] filter_methods;

        public AdminExercisesUserControl(GraphicFeatures graphicFeatures, EventHandler exerciseFunction, ExerciseManager exerciseManager)
        {
            InitializeComponent();
            load_number = load_step;
            this.Name = "Exercises";
            this.graphicFeatures = graphicFeatures;
            this.exerciseManager = exerciseManager;
            this.exerciseFunction = exerciseFunction;

            this.sort_methods = new string[4] { "Alphabet (A-Z)", "Alphabet (Z-A)", "Time (from newest)", "Time (from oldest)" };
            this.filter_methods = new string[2] { "Official", "Custom" };

            this.sortFactory = new AlphabetAndTimeSortFactory();

            sortings = new (string, string)[4] { ("alphabet", "asc"), ("alphabet", "desc"), ("time", "desc"), ("time", "asc")};

            fullActionMenuLayout = new FullActionMenuLayout(graphicFeatures, this, ShowAll, Search, Sort, Filter, sort_methods, filter_methods);

            FillInExercises();

            //Exercise[] exercises = exerciseManager.GetExercises(null, null, null, null, load_number);



            //SearchPictureBox.MouseEnter += graphicFeatures.HoverButtonWithoutColor;
            //SearchPictureBox.MouseLeave += graphicFeatures.UnhoverButtonWithoutColor;

            //SearchPanel.MouseEnter += graphicFeatures.HoverButtonWithoutColor;
            //SearchPanel.MouseLeave += graphicFeatures.UnhoverButtonWithoutColor;

            //SearchBlackPanel.MouseEnter += graphicFeatures.HoverButtonWithoutColor;
            //SearchBlackPanel.MouseLeave += graphicFeatures.UnhoverButtonWithoutColor;
            //this.Controls.Add(new CustomButton("Hello", new Size(150, 50), new Font("Segoe UI", 16), new Point(20, 360)));
        }

        public int FillInExercises()
        {
            Exercise[] exercises = exerciseManager.GetExercises(search_query, sort_method, sort_order, isOfficial, load_number + 1);
            showMoreButton = CommonData.CheckForShowMoreButton(ref exercises, load_number);

            GraphicExercise[] graphicExercises = new GraphicExercise[exercises.Length];
            for (int i = 0; i < exercises.Length; i++)
            {
                graphicExercises[i] = new GraphicExercise(exercises[i], graphicFeatures, exerciseFunction);
            }
            fullActionMenuLayout.FillContent(graphicExercises, showMoreButton, LoadMoreFunction);
            return 1;
        }

        private void LoadMoreFunction(object sender, EventArgs e)
        {
            load_number += load_step;
            int value = fullActionMenuLayout.cognitiveContentScroll.ScrollBar.Value;
            FillInExercises();
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
            FillInExercises();
        }

        private void Search(object sender, EventArgs e)
        {
            search_query = fullActionMenuLayout.searchSet.searchTextBox.textBox.Text;
            load_number = load_step;
            FillInExercises();
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
            FillInExercises();
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
            FillInExercises();
        }
    }
}

using Logic_Layer.Classes;
using Logic_Layer.Custom_exceptions;
using Logic_Layer.Managers;
using MindfulLens_DesktopApp.Add_and_edit.Add;
using MindfulLens_DesktopApp.Add_and_edit.Edit;
using MindfulLens_DesktopApp.Add_and_edit.View;
using MindfulLens_DesktopApp.My_tools;
using MindfulLens_DesktopApp.User_controls;
using MindfulLens_DesktopApp.Visitor.User_controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.Containers.Abstract
{
    public abstract class AbstractExercisesContainer : UserControl
    {
        protected UserControl exercisesUserControl;
        protected ExerciseAdd exerciseAdd;
        protected ExerciseEdit exerciseEdit;
        protected ExerciseView exerciseView;
        protected ReportAdd reportAdd;
        protected User user;
        protected ExerciseManager exerciseManager;
        public Func<int> FillContent { get; protected set; }

        private void InitializeComponent()
        {

        }

        //public AbstractExercisesContainer(Size size, GraphicFeatures graphicFeatures, User loggedUser, ExerciseManager exerciseManager, Func<int> FillContent)
        //{
        //    this.Name = "Exercises";
        //    this.exerciseManager = exerciseManager;
        //    //exercisesUserControl = new AdminExercisesUserControl(graphicFeatures, WatchExercise, exerciseManager);
        //    exerciseAdd = new ExerciseAdd(graphicFeatures, size, loggedUser, exerciseManager);
        //    exerciseEdit = new ExerciseEdit(graphicFeatures, size, exerciseManager);
        //    exerciseView = new ExerciseView(graphicFeatures, size, exerciseManager);
        //    exerciseEdit.BackButton.BindFunction(BackToExercises);
        //    exerciseAdd.BackButton.BindFunction(BackToExercises);
        //    exerciseView.BackButton.BindFunction(BackToExercises);
        //    exerciseAdd.ForwardButton.BindFunction(AddNewExercise);
        //    exerciseEdit.ForwardButton.BindFunction(EditExercise);
        //    this.user = loggedUser;
        //    this.Size = size;
        //    exercisesUserControl.Location = new Point(0, 0);
        //    this.Controls.Add(exercisesUserControl);
        //}

        protected void ProtectedConstructor(Size size, GraphicFeatures graphicFeatures, User loggedUser, ExerciseManager exerciseManager, Func<int> FillContent, ReportManager reportManager)
        {
            this.exerciseManager = exerciseManager;
            this.FillContent = FillContent;
            //exercisesUserControl = new AdminExercisesUserControl(graphicFeatures, WatchExercise, exerciseManager);
            exerciseAdd = new ExerciseAdd(graphicFeatures, size, loggedUser, exerciseManager);
            exerciseEdit = new ExerciseEdit(graphicFeatures, size, exerciseManager, this);
            reportAdd = new ReportAdd(reportManager, graphicFeatures, size);
            exerciseView = new ExerciseView(graphicFeatures, size, exerciseManager, reportAdd, this, loggedUser);
            exerciseEdit.BackButton.BindFunction(BackToExercises);
            exerciseAdd.BackButton.BindFunction(BackToExercises);
            exerciseView.BackButton.BindFunction(BackToExercises);
            exerciseAdd.ForwardButton.BindFunction(AddNewExercise);
            exerciseEdit.ForwardButton.BindFunction(EditExercise);
            reportAdd.BackButton.BindFunction(BackToExercise);
            user = loggedUser;
            Size = size;
            exercisesUserControl.Location = new Point(0, 0);
            Controls.Add(exercisesUserControl);
        }

        protected void GoToAddNewExercise(object sender, EventArgs e)
        {
            Controls.Clear();
            exerciseAdd.PrepareForAdd();
            Controls.Add(exerciseAdd);
        }

        private void BackToExercise(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.Controls.Add(exerciseView);
        }

        protected void WatchExercise(object sender, EventArgs e)
        {
            Label? label = sender as Label;
            TitledPublication? titledPublication = null;
            if (label != null)
                titledPublication = (label.Parent.Parent as GraphicTitledPublication).titledPublication;
            else
                titledPublication = ((sender as Panel).Parent as GraphicTitledPublication).titledPublication;
            if (titledPublication != null)
            {
                Controls.Clear();
                if (titledPublication.Creator.Id == user.Id || (titledPublication as Exercise).IsOfficial && (user.Importancy == Importancy.Admin || user.Importancy == Importancy.Me))
                {
                    exerciseEdit.PrepareForEdit(titledPublication as Exercise);
                    Controls.Add(exerciseEdit);
                }
                else
                {
                    exerciseView.PrepareForViewing(titledPublication as Exercise, user);
                    Controls.Add(exerciseView);
                }
            }
        }

        protected void BackToExercises(object sender, EventArgs e)
        {
            BackToMainPage();
        }

        public int BackToMainPage()
        {
            Controls.Clear();
            Controls.Add(exercisesUserControl);
            return 1;
        }

        public void GoToReportPage()
        {
            Controls.Clear();
            Controls.Add(reportAdd);
        }

        protected void AddNewExercise(object sender, EventArgs e)
        {
            try
            {
                CheckInfo(exerciseAdd.NameTextBox.textBox.Text, exerciseAdd.DescriptionTextBox.textBox.Text);

                Exercise exercise = new Exercise(exerciseAdd.DescriptionTextBox.textBox.Text, user, exerciseAdd.NameTextBox.textBox.Text);
                exerciseManager.AddExercise(exercise);
                MessageBox.Show("Successfully added");
                FillContent();
                BackToMainPage();
            }
            catch (WrongInputException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (DatabaseException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Something went wrong. Please, try again");
                MessageBox.Show(ex.Message);
            }
        }

        protected void EditExercise(object sender, EventArgs e)
        {
            try
            {
                CheckInfo(exerciseEdit.NameTextBox.textBox.Text, exerciseEdit.DescriptionTextBox.textBox.Text);

                Exercise exercise = new Exercise(exerciseEdit.exercise.ID, exerciseEdit.DescriptionTextBox.textBox.Text, exerciseEdit.exercise.CreationDate, exerciseEdit.exercise.Creator, exerciseEdit.NameTextBox.textBox.Text, exerciseEdit.exercise.IsOfficial);
                exerciseManager.UpdateExercise(exercise);
                MessageBox.Show("Successfully updated");
                FillContent();
                BackToMainPage();
            }
            catch (WrongInputException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (DatabaseException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Something went wrong. Please, try again");
                MessageBox.Show(ex.Message);
            }
        }

        protected void CheckInfo(string title, string text)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new WrongInputException("You haven't entered the name");
            else if (string.IsNullOrWhiteSpace(text))
                throw new WrongInputException("You haven't entered the description");
        }
    }
}

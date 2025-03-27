using Logic_Layer.Classes;
using Logic_Layer.Custom_exceptions;
using Logic_Layer.Managers;
using MindfulLens_DesktopApp.Add_and_edit.Abstract_add_and_edit;
using MindfulLens_DesktopApp.Containers.Abstract;
using MindfulLens_DesktopApp.My_tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.Add_and_edit.Edit
{
    public class ExerciseEdit : ExercisesAddAndEdit
    {
        //public CustomButton ReportButton { get; protected set; }
        public CustomButton DeleteButton { get; protected set; }
        public Exercise exercise { get; protected set; }
        private AbstractExercisesContainer exercisesContainer;

        public ExerciseEdit(GraphicFeatures graphicFeatures, Size size, ExerciseManager exerciseManager, AbstractExercisesContainer exercisesContainer) : base("Save", graphicFeatures, size, exerciseManager)
        {
            int spacing = 20;
            DeleteButton = new CustomButton("Delete", new Size(160, 60), new Font("Segoe UI", 16), new Point(BackButton.Location.X + BackButton.Width + spacing, BackButton.Location.Y), graphicFeatures);
            this.Controls.Add(DeleteButton);
            this.exercisesContainer = exercisesContainer;
            DeleteButton.BindFunction(DeleteExercise);
            //ReportButton = new CustomButton("Report", new Size(160, 60), new Font("Segoe UI", 16), new Point(DeleteButton.Location.X + DeleteButton.Width + spacing, BackButton.Location.Y), graphicFeatures);
        }

        public void PrepareForEdit(Exercise exercise)
        {
            FillInExercise(exercise);
        }

        private void FillInExercise(Exercise exercise)
        {
            this.exercise = exercise;
            NameTextBox.textBox.Text = exercise.Title;
            DescriptionTextBox.textBox.Text = exercise.Text;
            MakeCreatedByUser(exercise.Creator, exercise.CreationDate);
        }

        private void DeleteExercise(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Delete", "Are you sure you want to delete this publication?", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    exerciseManager.RemoveExercise(exercise);
                    MessageBox.Show("Successfully removed");
                    exercisesContainer.FillContent();
                    exercisesContainer.BackToMainPage();
                }
                catch (WrongInputException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (DatabaseException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch
                {
                    MessageBox.Show("Something went wrong. Please, try again");
                }
            }
        }

    }
}

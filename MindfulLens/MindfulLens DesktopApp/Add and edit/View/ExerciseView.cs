using Logic_Layer.Classes;
using Logic_Layer.Custom_exceptions;
using Logic_Layer.Managers;
using MindfulLens_DesktopApp.Add_and_edit.Abstract_add_and_edit;
using MindfulLens_DesktopApp.Containers.Abstract;
using MindfulLens_DesktopApp.My_tools;
using MindfulLens_DesktopApp.Visitor.User_controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.Add_and_edit.View
{
    public class ExerciseView : ExercisesAddAndEdit
    {
        private Exercise exercise;
        public CustomButton ReportButton { get; protected set; }
        public CustomButton DeleteButton { get; protected set; }
        private ReportAdd reportAdd;
        private AbstractExercisesContainer exercisesContainer;
        private User loggedUser;
        public ExerciseView(GraphicFeatures graphicFeatures, Size size, ExerciseManager exerciseManager, ReportAdd reportAdd, AbstractExercisesContainer exercisesContainer, User loggedUser) : base("", graphicFeatures, size, exerciseManager)
        {
            int spacing = 20;

            this.reportAdd = reportAdd;
            this.exercisesContainer = exercisesContainer;
            this.loggedUser = loggedUser;


            Color color = Color.FromArgb(255, 241, 175, 253);
            ChangeTextBoxColors(color);
            ChangeTextBoxBorderColor(color);
            ChangeTextBoxReadonly(true);
            ChangeWindowColor(color);
            ChangeLabelForeColor(Color.Black);
            ChangeTextBoxForeColor(Color.Black);
            ForwardButton.Visible = false;


            DeleteButton = new CustomButton("Delete", new Size(160, 60), new Font("Segoe UI", 16), new Point(BackButton.Location.X + BackButton.Width + spacing, BackButton.Location.Y), graphicFeatures);
            ReportButton = new CustomButton("Report", new Size(160, 60), new Font("Segoe UI", 16), new Point(BackButton.Location.X + BackButton.Width + spacing, BackButton.Location.Y), graphicFeatures);
            this.Controls.Add(DeleteButton);
            this.Controls.Add(ReportButton);
            DeleteButton.Visible = false;
            ReportButton.Visible = false;
            DeleteButton.BindFunction(DeleteExercise);
            ReportButton.BindFunction(GoToAddNewReport);
        }

        public void PrepareForViewing(Exercise exercise, User user)
        {
            FillInExercise(exercise);
            ChangeCreatedByUserColor(Color.Black);
            if (user.Importancy == Importancy.Admin || user.Importancy == Importancy.Me)
                MakeButtonsVisible(true);
            else
                MakeButtonsVisible(false);
        }

        private void GoToAddNewReport(object sender, EventArgs e)
        {
            GraphicExercise graphicExercise = new GraphicExercise(exercise, graphicFeatures, null);
            reportAdd.PrepareForReport(graphicExercise, exercisesContainer.BackToMainPage, loggedUser);
            exercisesContainer.GoToReportPage();
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

        private void MakeButtonsVisible(bool IsDeleteVisible)
        {
            DeleteButton.Visible = IsDeleteVisible;
            ReportButton.Visible = !IsDeleteVisible;
        }

        private void ChangeTextBoxColors(Color color)
        {
            NameTextBox.textBox.BackColor = color;
            DescriptionTextBox.textBox.BackColor = color;
        }

        private void ChangeTextBoxReadonly(bool readOnly)
        {
            NameTextBox.textBox.ReadOnly = readOnly;
            DescriptionTextBox.textBox.ReadOnly = readOnly;
        }

        private void ChangeTextBoxBorderColor(Color color)
        {
            NameTextBox.BackColor = color;
            DescriptionTextBox.BackColor = color;
        }

        private void FillInExercise(Exercise exercise)
        {
            this.exercise = exercise;
            NameTextBox.textBox.Text = exercise.Title;
            DescriptionTextBox.textBox.Text = exercise.Text;
            MakeCreatedByUser(exercise.Creator, exercise.CreationDate);
        }

        private void ChangeWindowColor(Color color)
        {
            this.BackColor = color;
        }

        private void ChangeLabelForeColor(Color color)
        {
            NameLabel.ForeColor = color;
            DescriptionLabel.ForeColor = color;
        }

        private void ChangeTextBoxForeColor(Color color)
        {
            NameTextBox.ForeColor = color;
            DescriptionTextBox.ForeColor = color;
        }
    }
}

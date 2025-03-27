using Logic_Layer.Classes;
using Logic_Layer.Custom_exceptions;
using Logic_Layer.Managers;
using MindfulLens_DesktopApp.Add_and_edit.Add;
using MindfulLens_DesktopApp.Add_and_edit.Edit;
using MindfulLens_DesktopApp.Add_and_edit.View;
using MindfulLens_DesktopApp.My_tools;
using MindfulLens_DesktopApp.Visitor.User_controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.Containers.Abstract
{
    public class AbstractSourcesContainer : UserControl
    {
        protected UserControl sourcesUserControl;
        protected SourceAdd sourceAdd;
        protected SourceEdit sourceEdit;
        protected SourceView sourceView;
        protected ReportAdd reportAdd;
        protected User user;
        protected SourceManager sourceManager;
        public Func<int> FillContent { get; protected set; }

        protected void ProtectedConstructor(Size size, GraphicFeatures graphicFeatures, User loggedUser, SourceManager sourceManager, Func<int> FillContent, ReportManager reportManager)
        {
            this.sourceManager = sourceManager;
            this.FillContent = FillContent;
            //exercisesUserControl = new AdminExercisesUserControl(graphicFeatures, WatchExercise, exerciseManager);
            sourceAdd = new SourceAdd(graphicFeatures, size, loggedUser, sourceManager);
            sourceEdit = new SourceEdit(graphicFeatures, size, this, sourceManager);
            reportAdd = new ReportAdd(reportManager, graphicFeatures, size);
            sourceView = new SourceView(graphicFeatures, size, sourceManager, reportAdd, this, loggedUser);
            sourceEdit.BackButton.BindFunction(BackToSources);
            sourceAdd.BackButton.BindFunction(BackToSources);
            sourceView.BackButton.BindFunction(BackToSources);
            sourceAdd.ForwardButton.BindFunction(AddNewSource);
            sourceEdit.ForwardButton.BindFunction(EditSource);
            reportAdd.BackButton.BindFunction(BackToSource);
            user = loggedUser;
            Size = size;
            sourcesUserControl.Location = new Point(0, 0);
            Controls.Add(sourcesUserControl);
        }

        protected void GoToAddNewExercise(object sender, EventArgs e)
        {
            Controls.Clear();
            sourceAdd.PrepareForAdd();
            Controls.Add(sourceAdd);
        }

        private void BackToSource(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.Controls.Add(sourceView);
        }

        protected void WatchSource(object sender, EventArgs e)
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
                if (titledPublication.Creator.Id == user.Id || (titledPublication as Source).IsOfficial && (user.Importancy == Importancy.Admin || user.Importancy == Importancy.Me))
                {
                    sourceEdit.PrepareForEdit(titledPublication as Source);
                    Controls.Add(sourceEdit);
                }
                else
                {
                    sourceView.PrepareForViewing(titledPublication as Source, user);
                    Controls.Add(sourceView);
                }
            }
        }

        protected void BackToSources(object sender, EventArgs e)
        {
            BackToMainPage();
        }

        public int BackToMainPage()
        {
            Controls.Clear();
            Controls.Add(sourcesUserControl);
            return 1;
        }

        public void GoToReportPage()
        {
            Controls.Clear();
            Controls.Add(reportAdd);
        }

        protected void AddNewSource(object sender, EventArgs e)
        {
            try
            {
                CheckInfo(sourceAdd.NameTextBox.textBox.Text, sourceAdd.DescriptionTextBox.textBox.Text, sourceAdd.AuthorTextBox.textBox.Text);

                Source source = new Source(sourceAdd.DescriptionTextBox.textBox.Text, user, sourceAdd.NameTextBox.textBox.Text, sourceAdd.GetLinks(), sourceAdd.AuthorTextBox.textBox.Text);
                sourceManager.AddSource(source);
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

        protected void EditSource(object sender, EventArgs e)
        {
            try
            {
                CheckInfo(sourceAdd.NameTextBox.textBox.Text, sourceAdd.DescriptionTextBox.textBox.Text, sourceAdd.AuthorTextBox.textBox.Text);


                Source source = new Source(sourceEdit.source.ID, sourceEdit.DescriptionTextBox.textBox.Text, sourceEdit.source.CreationDate, sourceEdit.source.Creator, sourceEdit.NameTextBox.textBox.Text, sourceEdit.GetLinks(), sourceEdit.AuthorTextBox.textBox.Text, sourceEdit.source.IsOfficial);
                sourceManager.UpdateSource(source);
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

        protected void CheckInfo(string title, string text, string author)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new WrongInputException("You haven't entered the name");
            if (string.IsNullOrWhiteSpace(text))
                throw new WrongInputException("You haven't entered the description");
            if (string.IsNullOrWhiteSpace(author))
                throw new WrongInputException("You haven't entered the author");
        }

    }
}

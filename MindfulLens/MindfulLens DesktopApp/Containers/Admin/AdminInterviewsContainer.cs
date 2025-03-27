using Logic_Layer.Classes;
using Logic_Layer.Custom_exceptions;
using Logic_Layer.Managers;
using MindfulLens_DesktopApp.Add_and_edit.Add;
using MindfulLens_DesktopApp.Add_and_edit.Edit;
using MindfulLens_DesktopApp.Containers.Abstract;
using MindfulLens_DesktopApp.My_tools;
using MindfulLens_DesktopApp.User_controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MindfulLens_DesktopApp.Containers.Admin
{
    public class AdminInterviewsContainer : UserControl, IContentContainer
    {
        protected InterviewsUserControl interviewsUserControl;
        protected ContentAdd contentAdd;
        protected ContentEdit contentEdit;
        protected User user;
        protected ContentManager contentManager;

        public AdminInterviewsContainer(Size size, GraphicFeatures graphicFeatures, User loggedUser, ContentManager contentManager)
        {
            this.Name = "Interviews";
            this.contentManager = contentManager;
            //exercisesUserControl = new AdminExercisesUserControl(graphicFeatures, WatchExercise, exerciseManager);
            interviewsUserControl = new InterviewsUserControl(graphicFeatures, WatchContent, contentManager);
            contentAdd = new ContentAdd(graphicFeatures, size, loggedUser, this.contentManager);
            contentEdit = new ContentEdit(graphicFeatures, size, this.contentManager, this);
            contentEdit.BackButton.BindFunction(BackToAllContent);
            contentAdd.BackButton.BindFunction(BackToAllContent);
            contentAdd.ForwardButton.BindFunction(AddNewContent);
            contentEdit.ForwardButton.BindFunction(EditContent);
            interviewsUserControl.noFilterActionMenuLayout.AddNewButton.BindFunction(GoToAddNewContent);
            user = loggedUser;
            Size = size;
            interviewsUserControl.Location = new Point(0, 0);
            Controls.Add(interviewsUserControl);
        }

        protected void GoToAddNewContent(object sender, EventArgs e)
        {
            Controls.Clear();
            contentAdd.PrepareForAdd();
            Controls.Add(contentAdd);
        }

        protected void WatchContent(object sender, EventArgs e)
        {
            Label? label = sender as Label;
            Publication? titledPublication = null;
            if (label != null)
                titledPublication = (label.Parent as GraphicContent).publication;
            if (titledPublication != null)
            {
                Controls.Clear();
                contentEdit.PrepareForEdit(titledPublication as Content);
                Controls.Add(contentEdit);
            }
        }

        protected void BackToAllContent(object sender, EventArgs e)
        {
            BackToMainPage();
        }

        public int BackToMainPage()
        {
            Controls.Clear();
            Controls.Add(interviewsUserControl);
            return 1;
        }


        protected void AddNewContent(object sender, EventArgs e)
        {
            try
            {
                CheckInfo(contentAdd.NameTextBox.textBox.Text, contentAdd.DescriptionTextBox.textBox.Text, contentAdd.PhotoPictureBox);

                Content content = new Content(contentAdd.DescriptionTextBox.textBox.Text, user, contentAdd.NameTextBox.textBox.Text, "Interview", contentAdd.image);
                contentManager.AddContent(content);
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

        protected void EditContent(object sender, EventArgs e)
        {
            try
            {
                CheckInfo(contentEdit.NameTextBox.textBox.Text, contentEdit.DescriptionTextBox.textBox.Text, contentEdit.PhotoPictureBox);

                Content content = new Content(contentEdit.content.ID, contentEdit.DescriptionTextBox.textBox.Text, contentEdit.content.CreationDate, contentEdit.content.Creator, contentEdit.NameTextBox.textBox.Text, "News", contentEdit.image);
                contentManager.UpdateContent(content);
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

        protected void CheckInfo(string title, string text, PictureBox pictureBox)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new WrongInputException("You haven't entered the name");
            if (string.IsNullOrWhiteSpace(text))
                throw new WrongInputException("You haven't entered the description");
            if (pictureBox.Image == null)
                throw new WrongInputException("You haven't chosen the photo");
        }

        public int FillContent()
        {
            return interviewsUserControl.FillInContent();
        }
    }
}

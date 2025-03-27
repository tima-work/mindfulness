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
    public abstract class AbstractCognitivePartContainer : UserControl
    {
        protected UserControl cognitivePartsUserControl;
        protected CognitivePartAdd cognitivePartAdd;
        protected CognitivePartEdit cognitivePartEdit;
        protected CognitivePartView cognitivePartView;
        protected ReportAdd reportAdd;
        protected User user;
        protected CognitivePartManager cognitivePartManager;
        protected string className;
        public Func<int> FillContent { get; protected set; }

        private void InitializeComponent()
        {

        }

        protected void ProtectedConstructor(Size size, GraphicFeatures graphicFeatures, User loggedUser, CognitivePartManager cognitivePartManager, Func<int> FillContent, string handlingText, string classname, Color outlineColor, Color BackTextColor, Color TextColor, ReportManager reportManager)
        {
            this.cognitivePartManager = cognitivePartManager;
            this.FillContent = FillContent;
            this.className = classname;
            //exercisesUserControl = new AdminExercisesUserControl(graphicFeatures, WatchExercise, exerciseManager);
            cognitivePartAdd = new CognitivePartAdd(graphicFeatures, size, handlingText, loggedUser, outlineColor, BackTextColor, TextColor, cognitivePartManager, classname);
            cognitivePartEdit = new CognitivePartEdit(graphicFeatures, size, handlingText, outlineColor, BackTextColor, TextColor, cognitivePartManager, this);
            reportAdd = new ReportAdd(reportManager, graphicFeatures, size);
            cognitivePartView = new CognitivePartView(graphicFeatures, size, cognitivePartManager, reportAdd, this, loggedUser, outlineColor, BackTextColor, TextColor, handlingText);
            cognitivePartEdit.BackButton.BindFunction(BackToCognitiveParts);
            cognitivePartAdd.BackButton.BindFunction(BackToCognitiveParts);
            cognitivePartView.BackButton.BindFunction(BackToCognitiveParts);
            cognitivePartAdd.ForwardButton.BindFunction(AddNewCognitivePart);
            cognitivePartEdit.ForwardButton.BindFunction(EditCognitivePart);
            reportAdd.BackButton.BindFunction(BackToCognitivePart);
            user = loggedUser;
            Size = size;
            cognitivePartsUserControl.Location = new Point(0, 0);
            Controls.Add(cognitivePartsUserControl);
        }

        protected void GoToAddNewCognitivePart(object sender, EventArgs e)
        {
            Controls.Clear();
            cognitivePartAdd.PrepareForAdd();
            Controls.Add(cognitivePartAdd);
        }

        private void BackToCognitivePart(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.Controls.Add(cognitivePartView);
        }

        protected void WatchCognitivePart(object sender, EventArgs e)
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
                if (titledPublication.Creator.Id == user.Id || (titledPublication as CognitivePart).IsOfficial && (user.Importancy == Importancy.Admin || user.Importancy == Importancy.Me))
                {
                    cognitivePartEdit.PrepareForEdit(titledPublication as CognitivePart);
                    Controls.Add(cognitivePartEdit);
                }
                else
                {
                    cognitivePartView.PrepareForViewing(titledPublication as CognitivePart, user);
                    Controls.Add(cognitivePartView);
                }
            }
        }

        protected void BackToCognitiveParts(object sender, EventArgs e)
        {
            BackToMainPage();
        }

        public int BackToMainPage()
        {
            Controls.Clear();
            Controls.Add(cognitivePartsUserControl);
            return 1;
        }

        public void GoToReportPage()
        {
            Controls.Clear();
            Controls.Add(reportAdd);
        }

        protected void AddNewCognitivePart(object sender, EventArgs e)
        {
            try
            {
                CheckInfo(cognitivePartAdd.NameTextBox.textBox.Text, cognitivePartAdd.DescriptionTextBox.textBox.Text, cognitivePartAdd.HandlingTextBox.textBox.Text);

                CognitivePart cognitivePart = new CognitivePart(cognitivePartAdd.DescriptionTextBox.textBox.Text, user, cognitivePartAdd.NameTextBox.textBox.Text, className, cognitivePartAdd.HandlingTextBox.textBox.Text);
                cognitivePartManager.AddCognitivePart(cognitivePart);
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

        protected void EditCognitivePart(object sender, EventArgs e)
        {
            try
            {
                CheckInfo(cognitivePartEdit.NameTextBox.textBox.Text, cognitivePartEdit.DescriptionTextBox.textBox.Text, cognitivePartEdit.HandlingTextBox.textBox.Text);

                CognitivePart cognitivePart = new CognitivePart(cognitivePartEdit.cognitivePart.ID, cognitivePartEdit.DescriptionTextBox.textBox.Text, cognitivePartEdit.cognitivePart.CreationDate, cognitivePartEdit.cognitivePart.Creator, cognitivePartEdit.NameTextBox.textBox.Text, cognitivePartEdit.cognitivePart.ClassName, cognitivePartEdit.HandlingTextBox.textBox.Text, cognitivePartEdit.cognitivePart.IsOfficial);
                cognitivePartManager.UpdateCognitivePart(cognitivePart);
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

        protected void CheckInfo(string title, string text, string way_of_handling)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new WrongInputException("You haven't entered the name");
            if (string.IsNullOrWhiteSpace(text))
                throw new WrongInputException("You haven't entered the description");
            if (string.IsNullOrWhiteSpace(way_of_handling))
                throw new WrongInputException("You haven't entered way of handling");
        }
    }
}

using Logic_Layer.Classes;
using Logic_Layer.Custom_exceptions;
using Logic_Layer.Managers;
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
    public class CognitivePartView : CognitivePartAddAndEdit
    {
        private CognitivePart cognitivePart;
        public CustomButton ReportButton { get; protected set; }
        public CustomButton DeleteButton { get; protected set; }
        private ReportAdd reportAdd;
        private AbstractCognitivePartContainer cognitivePartContainer;
        private User loggedUser;
        public CognitivePartView(GraphicFeatures graphicFeatures, Size size, CognitivePartManager cognitivePartManager, ReportAdd reportAdd, AbstractCognitivePartContainer cognitivePartContainer, User loggedUser, Color outlineColor, Color BackTextColor, Color TextColor, string handlingText) : base("", graphicFeatures, size, handlingText, outlineColor, BackTextColor, TextColor, cognitivePartManager)
        {
            int spacing = 20;

            this.reportAdd = reportAdd;
            this.cognitivePartContainer = cognitivePartContainer;
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
            DeleteButton.BindFunction(DeleteCognitivePart);
            ReportButton.BindFunction(GoToAddNewReport);
        }

        public void PrepareForViewing(CognitivePart cognitivePart, User user)
        {
            FillInCognitivePart(cognitivePart);
            ChangeCreatedByUserColor(Color.Black);
            if (user.Importancy == Importancy.Admin || user.Importancy == Importancy.Me)
                MakeButtonsVisible(true);
            else
                MakeButtonsVisible(false);
        }

        private void GoToAddNewReport(object sender, EventArgs e)
        {
            GraphicTitledPublication? titledPublication;
            switch (cognitivePart.ClassName)
            {
                case "Bias":
                    titledPublication = new GraphicBias(cognitivePart, graphicFeatures, null);
                    break;
                case "Theory":
                    titledPublication = new GraphicTheoryMethod(cognitivePart, graphicFeatures, null);
                    break;
                default:
                    titledPublication = null;
                    break;
            }

            if (titledPublication != null)
            {
                reportAdd.PrepareForReport(titledPublication, cognitivePartContainer.BackToMainPage, loggedUser);
                cognitivePartContainer.GoToReportPage();
            }
        }

        private void DeleteCognitivePart(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Delete", "Are you sure you want to delete this publication?", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    cognitivePartManager.DeleteCognitivePart(cognitivePart);
                    MessageBox.Show("Successfully removed");
                    cognitivePartContainer.FillContent();
                    cognitivePartContainer.BackToMainPage();
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
            HandlingTextBox.textBox.BackColor = color;
        }

        private void ChangeTextBoxReadonly(bool readOnly)
        {
            NameTextBox.textBox.ReadOnly = readOnly;
            DescriptionTextBox.textBox.ReadOnly = readOnly;
            HandlingTextBox.textBox.ReadOnly = readOnly;
        }

        private void ChangeTextBoxBorderColor(Color color)
        {
            NameTextBox.BackColor = color;
            DescriptionTextBox.BackColor = color;
            HandlingTextBox.BackColor = color;
        }

        private void FillInCognitivePart(CognitivePart cognitivePart)
        {
            this.cognitivePart = cognitivePart;
            NameTextBox.textBox.Text = cognitivePart.Title;
            DescriptionTextBox.textBox.Text = cognitivePart.Text;
            HandlingTextBox.textBox.Text = cognitivePart.WayOfHandling;
            MakeCreatedByUser(cognitivePart.Creator, cognitivePart.CreationDate);
        }

        private void ChangeWindowColor(Color color)
        {
            this.BackColor = color;
        }

        private void ChangeLabelForeColor(Color color)
        {
            NameLabel.ForeColor = color;
            DescriptionLabel.ForeColor = color;
            HandlingLabel.ForeColor = color;
        }

        private void ChangeTextBoxForeColor(Color color)
        {
            NameTextBox.ForeColor = color;
            DescriptionTextBox.ForeColor = color;
            HandlingTextBox.ForeColor = color;
        }
    }
}

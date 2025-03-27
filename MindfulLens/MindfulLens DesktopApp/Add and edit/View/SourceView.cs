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
    public class SourceView : SourceAddAndEdit
    {
        private Source source;
        public CustomButton ReportButton { get; protected set; }
        public CustomButton DeleteButton { get; protected set; }
        private ReportAdd reportAdd;
        private AbstractSourcesContainer sourcesContainer;
        private User loggedUser;
        public SourceView(GraphicFeatures graphicFeatures, Size size, SourceManager sourceManager, ReportAdd reportAdd, AbstractSourcesContainer sourcesContainer, User loggedUser) : base("", graphicFeatures, size, sourceManager)
        {
            int spacing = 20;

            this.reportAdd = reportAdd;
            this.sourcesContainer = sourcesContainer;
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
            DeleteButton.BindFunction(DeleteSource);
            ReportButton.BindFunction(GoToAddNewReport);
        }

        public void PrepareForViewing(Source source, User user)
        {
            FillInSource(source);
            ChangeCreatedByUserColor(Color.Black);
            if (user.Importancy == Importancy.Admin || user.Importancy == Importancy.Me)
                MakeButtonsVisible(true);
            else
                MakeButtonsVisible(false);
        }

        private void GoToAddNewReport(object sender, EventArgs e)
        {
            GraphicSource graphicSource = new GraphicSource(source, graphicFeatures, null);
            reportAdd.PrepareForReport(graphicSource, sourcesContainer.BackToMainPage, loggedUser);
            sourcesContainer.GoToReportPage();
        }

        private void DeleteSource(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Delete", "Are you sure you want to delete this publication?", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    sourceManager.DeleteSource(source);
                    MessageBox.Show("Successfully removed");
                    sourcesContainer.FillContent();
                    sourcesContainer.BackToMainPage();
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
            AuthorTextBox.textBox.BackColor = color;
        }

        private void ChangeTextBoxReadonly(bool readOnly)
        {
            NameTextBox.textBox.ReadOnly = readOnly;
            DescriptionTextBox.textBox.ReadOnly = readOnly;
            AuthorTextBox.textBox.ReadOnly = readOnly;
        }

        private void ChangeTextBoxBorderColor(Color color)
        {
            NameTextBox.BackColor = color;
            DescriptionTextBox.BackColor = color;
            AuthorTextBox.BackColor = color;
        }

        private void FillInSource(Source source)
        {
            this.source = source;
            NameTextBox.textBox.Text = source.Title;
            DescriptionTextBox.textBox.Text = source.Text;
            AuthorTextBox.textBox.Text = source.Author;
            MakeCreatedByUser(source.Creator, source.CreationDate);
        }

        private void ChangeWindowColor(Color color)
        {
            this.BackColor = color;
        }

        private void ChangeLabelForeColor(Color color)
        {
            NameLabel.ForeColor = color;
            DescriptionLabel.ForeColor = color;
            AuthorLabel.ForeColor = color;
        }

        private void ChangeTextBoxForeColor(Color color)
        {
            NameTextBox.ForeColor = color;
            DescriptionTextBox.ForeColor = color;
            AuthorTextBox.ForeColor = color;
        }
    }
}

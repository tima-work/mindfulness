using Logic_Layer.Classes;
using Logic_Layer.Managers;
using MindfulLens_DesktopApp.My_tools.Other_stuff;
using MindfulLens_DesktopApp.My_tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using MindfulLens_DesktopApp.Add_and_edit;
using Logic_Layer.Custom_exceptions;
using Microsoft.VisualBasic.ApplicationServices;
using MindfulLens_DesktopApp.Add_and_edit.Add;
using System.Resources;
using User = Logic_Layer.Classes.User;

namespace MindfulLens_DesktopApp.Visitor.User_controls
{
    public class ReportAdd : BaseAddAndEdit
    {
        private ReportManager reportManager;
        private GraphicTitledPublication titledPublication;
        private User creator;
        private Func<int> BackToMainMenu;



        protected Label ReasonLabel, DescriptionLabel, ReportedPublicationLabel;
        public CustomTextBox NameTextBox { get; protected set; }
        public CustomTextBox DescriptionTextBox { get; protected set; }
        private int label_textbox_spacing;
        protected ExerciseManager exerciseManager;
        public ReportAdd(ReportManager reportManager, GraphicFeatures graphicFeatures, Size size) : base("Publish", graphicFeatures, size)
        {
            this.reportManager = reportManager;
            Font font = new Font("Segoe UI", 15);
            label_textbox_spacing = 20;

            Color outlineColor = Color.Black;
            Color BackTextColor = Color.White;
            Color TextColor = Color.Black;

            ReasonLabel = new Label()
            {
                Text = "Reason:",
                ForeColor = Color.White,
                Font = font,
                Location = new Point(padding, BackButton.Height + label_textbox_spacing),
                AutoSize = true,
                BorderStyle = BorderStyle.None
            };


            NameTextBox = new CustomTextBox(outlineColor, BackTextColor, TextColor, "", new Size(250, 80), new Point(ReasonLabel.Location.X, ReasonLabel.Location.Y + ReasonLabel.Height + label_textbox_spacing), font, graphicFeatures, true);


            ReportedPublicationLabel = new Label()
            {
                Text = "Reported publication: ",
                Font = font,
                ForeColor = Color.White,
                Location = new Point(ReasonLabel.Location.X, NameTextBox.Location.Y + NameTextBox.Height + label_textbox_spacing),
                AutoSize = true,
                BorderStyle = BorderStyle.None
            };


            //HandlingTextBox = new CustomTextBox(outlineColor, BackTextColor, TextColor, "", new Size(NameTextBox.Width, 125), new Point(ReasonLabel.Location.X, ReportedPublicationLabel.Location.Y + ReportedPublicationLabel.Height + label_textbox_spacing), font, graphicFeatures, true);



            DescriptionLabel = new Label()
            {
                Text = "Description:",
                Font = font,
                ForeColor = Color.White,
                Location = new Point(NameTextBox.Width + NameTextBox.Location.X + 50, ReasonLabel.Location.Y),
                AutoSize = true,
                BorderStyle = BorderStyle.None
            };


            DescriptionTextBox = new CustomTextBox(outlineColor, BackTextColor, TextColor, "", new Size(this.Width - padding * 2 - 50 - NameTextBox.Width, GraphicTitledPublication.height + (ReportedPublicationLabel.Location.Y + ReportedPublicationLabel.Height + label_textbox_spacing) - NameTextBox.Location.Y), new Point(DescriptionLabel.Location.X, NameTextBox.Location.Y), font, graphicFeatures, true);


            this.Controls.Add(ReasonLabel);
            this.Controls.Add(NameTextBox);
            this.Controls.Add(ReportedPublicationLabel);
            //this.Controls.Add(HandlingTextBox);
            this.Controls.Add(DescriptionLabel);
            this.Controls.Add(DescriptionTextBox);

            //BackButton.BindFunction(GoBack);
            ForwardButton.BindFunction(AddNewReport);
        }

        public void PrepareForReport(GraphicTitledPublication reportedPublication, Func<int> BackToMainMenu, User creator)
        {
            try
            {
                this.Controls.Remove(titledPublication);
            }
            catch { }
            titledPublication = reportedPublication;
            this.creator = creator;
            this.BackToMainMenu = BackToMainMenu;
            titledPublication.Location = new Point(ReasonLabel.Location.X, ReportedPublicationLabel.Location.Y + ReportedPublicationLabel.Height + label_textbox_spacing);
            this.Controls.Add(titledPublication);
            MakeCreatedByUser(creator, DateTime.Now);
        }

        private void AddNewReport(object sender, EventArgs e)
        {
            try
            {
                CheckInfo(NameTextBox.textBox.Text, DescriptionTextBox.textBox.Text);

                Report report = new Report(DescriptionTextBox.textBox.Text, NameTextBox.textBox.Text, creator, titledPublication.titledPublication);
                reportManager.AddReport(report);
                MessageBox.Show("Successfully added");
                BackToMainMenu();
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

        private void CheckInfo(string reason, string text)
        {
            if (string.IsNullOrWhiteSpace(reason))
                throw new WrongInputException("You haven't entered reason");
            if (string.IsNullOrWhiteSpace(text)) 
                throw new WrongInputException("You haven't entered description");
        }
    }
}

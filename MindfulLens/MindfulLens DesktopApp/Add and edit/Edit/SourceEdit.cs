using Logic_Layer.Classes;
using Logic_Layer.Custom_exceptions;
using Logic_Layer.Managers;
using MindfulLens_DesktopApp.Add_and_edit.Abstract_add_and_edit;
using MindfulLens_DesktopApp.Containers.Abstract;
using MindfulLens_DesktopApp.My_tools;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.Add_and_edit.Edit
{
    public class SourceEdit : SourceAddAndEdit
    {
        public Source source { get; protected set; }
        private AbstractSourcesContainer sourcesContainer;
        public CustomButton DeleteButton { get; protected set; }

        public SourceEdit(GraphicFeatures graphicFeatures, Size size, AbstractSourcesContainer sourcesContainer, SourceManager sourceManager) : base("Save", graphicFeatures, size, sourceManager)
        {
            this.sourcesContainer = sourcesContainer;

            int spacing = 20;
            DeleteButton = new CustomButton("Delete", new Size(160, 60), new Font("Segoe UI", 16), new Point(BackButton.Location.X + BackButton.Width + spacing, BackButton.Location.Y), graphicFeatures);
            this.Controls.Add(DeleteButton);
            DeleteButton.BindFunction(DeleteSource);
        }

        public void PrepareForEdit(Source source)
        {
            FillInSource(source);
        }

        private void FillInSource(Source source)
        {
            int label_textbox_spacing = 20;
            this.source = source;
            NameTextBox.textBox.Text = source.Title;
            AuthorTextBox.textBox.Text = source.Author;
            DescriptionTextBox.textBox.Text = source.Text;
            List<My_tools.Other_stuff.LinkLabel> linkLabels = new List<My_tools.Other_stuff.LinkLabel>();
            string[] links = source.GetLinks();
            foreach (string link in links)
            {
                linkLabels.Add(new My_tools.Other_stuff.LinkLabel(link));
            }

            try
            {
                this.Controls.Remove(linksScroll);
            }
            catch { }

            linksScroll = new LinksScroll(new Size(this.Width - (DescriptionTextBox.Location.X + DescriptionTextBox.Width + label_textbox_spacing * 2), AddLinkLabel.Location.Y - (LinksLabel.Location.Y + LinksLabel.Height + label_textbox_spacing * 2)), new Point(LinksLabel.Location.X, LinksLabel.Location.Y + LinksLabel.Height + label_textbox_spacing), linkLabels.ToArray());
            this.Controls.Add(linksScroll);
            MakeCreatedByUser(source.Creator, source.CreationDate);
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

        public string[] GetLinks()
        {
            return linksScroll.GetLinks();
        }
    }
}

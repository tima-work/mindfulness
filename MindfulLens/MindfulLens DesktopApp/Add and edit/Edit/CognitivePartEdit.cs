using Logic_Layer.Classes;
using Logic_Layer.Custom_exceptions;
using Logic_Layer.Managers;
using MindfulLens_DesktopApp.Containers.Abstract;
using MindfulLens_DesktopApp.My_tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.Add_and_edit.Edit
{
    public class CognitivePartEdit : CognitivePartAddAndEdit
    {
        public CognitivePart cognitivePart { get; protected set; }
        private AbstractCognitivePartContainer cognitivePartContainer;
        public CustomButton DeleteButton { get; protected set; }

        public CognitivePartEdit(GraphicFeatures graphicFeatures, Size size, string handlingText, Color outlineColor, Color BackTextColor, Color TextColor, CognitivePartManager cognitivePartManager, AbstractCognitivePartContainer cognitivePartContainer) : base("Save", graphicFeatures, size, handlingText, outlineColor, BackTextColor, TextColor, cognitivePartManager)
        {
            int spacing = 20;
            DeleteButton = new CustomButton("Delete", new Size(160, 60), new Font("Segoe UI", 16), new Point(BackButton.Location.X + BackButton.Width + spacing, BackButton.Location.Y), graphicFeatures);
            this.Controls.Add(DeleteButton);
            this.cognitivePartContainer = cognitivePartContainer;
            DeleteButton.BindFunction(DeleteCognitivePart);
        }

        public void PrepareForEdit(CognitivePart cognitivePart)
        {
            FillInCognitivePart(cognitivePart);
        }

        private void FillInCognitivePart(CognitivePart cognitivePart)
        {
            this.cognitivePart = cognitivePart;
            NameTextBox.textBox.Text = cognitivePart.Title;
            DescriptionTextBox.textBox.Text = cognitivePart.Text;
            HandlingTextBox.textBox.Text = cognitivePart.WayOfHandling;
            MakeCreatedByUser(cognitivePart.Creator, cognitivePart.CreationDate);
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
    }
}

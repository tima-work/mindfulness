using Logic_Layer.Classes;
using Logic_Layer.Managers;
using MindfulLens_DesktopApp.Add_and_edit.Abstract_add_and_edit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.Add_and_edit.Add
{
    public class ExerciseAdd : ExercisesAddAndEdit
    {
        public ExerciseAdd(GraphicFeatures graphicFeatures, Size size, User creator, ExerciseManager exerciseManager) : base("Add", graphicFeatures, size, exerciseManager)
        {
            MakeCreatedByUser(creator, DateTime.Now);
        }

        public void PrepareForAdd()
        {
            NameTextBox.textBox.Text = string.Empty;
            DescriptionTextBox.textBox.Text = string.Empty;
        }
    }
}

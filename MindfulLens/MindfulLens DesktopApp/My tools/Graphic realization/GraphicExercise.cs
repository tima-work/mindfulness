using Logic_Layer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.My_tools
{
    public class GraphicExercise : GraphicTitledPublication
    {
        public override Color outlineColor { get; protected set; } = Color.Black;
        public override Color inlineColor { get; protected set; } = Color.FromArgb(255, 241, 175, 253);
        public override Color textColor { get; protected set; } = Color.Black;
        public override Color outlineHoverColor { get; protected set; } = Color.FromArgb(255, 241, 175, 253);
        public override Color inlineHoverColor { get; protected set; } = Color.FromArgb(255, 40, 40, 40);
        public override Color textHoverColor { get; protected set; } = Color.FromArgb(255, 241, 175, 253);
        public override int outlineSize { get; protected set; } = 4;
        public override Font font { get; protected set; } = new Font("Segoe UI", 18);


        public GraphicExercise(Exercise titledPublication, GraphicFeatures graphicFeatures, EventHandler onPress) : base(titledPublication, graphicFeatures, onPress)
        {

        }
    }
}

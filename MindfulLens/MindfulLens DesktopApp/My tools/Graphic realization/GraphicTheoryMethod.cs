using Logic_Layer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.My_tools
{
    public class GraphicTheoryMethod : GraphicTitledPublication
    {
        public override Color outlineColor { get; protected set; } = Color.Black;
        public override Color inlineColor { get; protected set; } = Color.FromArgb(255, 39, 70, 144);
        public override Color textColor { get; protected set; } = Color.White;
        public override Color outlineHoverColor { get; protected set; } = Color.FromArgb(255, 39, 70, 144);
        public override Color inlineHoverColor { get; protected set; } = Color.White;
        public override Color textHoverColor { get; protected set; } = Color.FromArgb(255, 39, 70, 144);
        public override int outlineSize { get; protected set; } = 4;
        public override Font font { get; protected set; } = new Font("Segoe UI", 18);


        public GraphicTheoryMethod(CognitivePart titledPublication, GraphicFeatures graphicFeatures, EventHandler onPress) : base(titledPublication, graphicFeatures, onPress)
        {

        }
    }
}

using Logic_Layer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.My_tools
{
    public abstract class GraphicTitledPublication : Panel
    {
        public abstract Color outlineColor { get; protected set; }
        public abstract Color inlineColor { get; protected set; }
        public abstract Color textColor { get; protected set; }
        public abstract Color outlineHoverColor { get; protected set; }
        public abstract Color inlineHoverColor { get; protected set; }
        public abstract Color textHoverColor { get; protected set; }
        public const int width = 200;
        public const int height = 100;
        public Size size { get; protected set; } = new Size(width, height);
        public abstract int outlineSize {  get; protected set; }
        public abstract Font font { get; protected set; }
        public TitledPublication titledPublication { get; protected set; }
        private Label textLabel;
        private Panel inlinePanel;
        private GraphicFeatures graphicFeatures;
        public GraphicTitledPublication(TitledPublication titledPublication, GraphicFeatures graphicFeatures, EventHandler? onPress)
        {
            this.graphicFeatures = graphicFeatures;
            this.titledPublication = titledPublication;


            //this.outlineColor = outlineColor;
            //this.inlineColor = inlineColor;
            //this.textColor = textColor;
            //this.outlineHoverColor = outlineHoverColor;
            //this.inlineHoverColor = inlineHoverColor;
            //this.textHoverColor = textHoverColor;


            this.BackColor = outlineColor;
            this.Size = size;
            this.MouseEnter += TitledPublicationHover;
            this.MouseLeave += TitledPublicationUnhover;


            inlinePanel = new Panel();
            inlinePanel.BackColor = inlineColor;
            inlinePanel.Size = new Size(size.Width - outlineSize, size.Height - outlineSize);
            inlinePanel.Location = new Point(outlineSize / 2, outlineSize / 2);
            inlinePanel.MouseEnter += TitledPublicationHover;
            inlinePanel.MouseLeave += TitledPublicationUnhover;


            textLabel = new Label();
            textLabel.ForeColor = textColor;
            textLabel.BackColor = Color.Transparent;
            textLabel.AutoSize = false;
            textLabel.Size = new Size(size.Width - outlineSize, size.Height - outlineSize);
            textLabel.Font = font;
            textLabel.Text = titledPublication.Title;
            textLabel.TextAlign = ContentAlignment.MiddleCenter;
            textLabel.Location = new Point(0, 0);
            textLabel.MouseEnter += TitledPublicationHover;
            textLabel.MouseLeave += TitledPublicationUnhover;


            if (onPress != null)
            {
                textLabel.Click += onPress;
                inlinePanel.Click += onPress;
            }


            inlinePanel.Controls.Add(textLabel);
            this.Controls.Add(inlinePanel);
        }

        private void TitledPublicationHover(object sender, EventArgs e)
        {
            this.BackColor = outlineHoverColor;
            inlinePanel.BackColor = inlineHoverColor;
            textLabel.ForeColor = textHoverColor;
            graphicFeatures.UnableCursor();
            this.Cursor = Cursors.Hand;
        }

        private void TitledPublicationUnhover(object sender, EventArgs e)
        {
            this.BackColor = outlineColor;
            inlinePanel.BackColor = inlineColor;
            textLabel.ForeColor = textColor;
            graphicFeatures.EnableCursor();
            this.Cursor = Cursors.Default;
        }
    }
}

using Logic_Layer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.My_tools
{
    public abstract class GraphicContent : Panel
    {
        public Publication publication {  get; protected set; }
        protected Label ContentLabel;
        protected Label RightLabel;
        protected int horizontal_spacing = 10;
        public GraphicContent(Publication publication)
        {
            this.publication = publication;

            this.BackColor = Color.Transparent;
            this.Size = new Size(838, 10);

            ContentLabel = new Label()
            {
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                AutoSize = true,
                Font = new Font("Segoe UI", 15),
                TextAlign = ContentAlignment.MiddleLeft,
                MaximumSize = new Size(Convert.ToInt32(this.Width * 0.8), 0),
                BorderStyle = BorderStyle.None
            };

            RightLabel = new Label()
            {
                Font = new Font("Segoe UI", 15),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                AutoSize = true,
                BorderStyle = BorderStyle.None
            };
        }
    }
}

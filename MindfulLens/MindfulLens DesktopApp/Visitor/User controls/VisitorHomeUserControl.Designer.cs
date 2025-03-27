namespace MindfulLens_DesktopApp.User_controls
{
    partial class VisitorHomeUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisitorHomeUserControl));
            LinkLabel = new Label();
            AdminHomeLabel = new Label();
            SuspendLayout();
            // 
            // LinkLabel
            // 
            LinkLabel.BackColor = Color.Transparent;
            LinkLabel.Cursor = Cursors.Hand;
            LinkLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            LinkLabel.ForeColor = Color.FromArgb(68, 68, 255);
            LinkLabel.Location = new Point(490, 116);
            LinkLabel.Name = "LinkLabel";
            LinkLabel.Size = new Size(216, 30);
            LinkLabel.TabIndex = 3;
            LinkLabel.Text = "http://mindfullens.org";
            LinkLabel.Visible = false;
            LinkLabel.MouseEnter += LinkLabel_MouseEnter;
            LinkLabel.MouseLeave += LinkLabel_MouseLeave;
            // 
            // AdminHomeLabel
            // 
            AdminHomeLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            AdminHomeLabel.ForeColor = Color.White;
            AdminHomeLabel.Location = new Point(110, 35);
            AdminHomeLabel.Name = "AdminHomeLabel";
            AdminHomeLabel.Size = new Size(700, 220);
            AdminHomeLabel.TabIndex = 2;
            AdminHomeLabel.Text = resources.GetString("AdminHomeLabel.Text");
            AdminHomeLabel.TextAlign = ContentAlignment.MiddleCenter;
            AdminHomeLabel.Visible = false;
            // 
            // VisitorHomeUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(102, 153, 155);
            Controls.Add(LinkLabel);
            Controls.Add(AdminHomeLabel);
            Name = "VisitorHomeUserControl";
            Size = new Size(920, 478);
            ResumeLayout(false);
        }

        #endregion

        private Label LinkLabel;
        private Label AdminHomeLabel;
    }
}

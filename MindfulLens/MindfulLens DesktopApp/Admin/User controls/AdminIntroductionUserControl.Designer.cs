namespace MindfulLens_DesktopApp.User_controls
{
    partial class AdminIntroductionUserControl
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
            components = new System.ComponentModel.Container();
            ScrollTimer = new System.Windows.Forms.Timer(components);
            label1 = new Label();
            SuspendLayout();
            // 
            // ScrollTimer
            // 
            ScrollTimer.Interval = 10;
            ScrollTimer.Tick += ScrollTimer_Tick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(241, 207);
            label1.Name = "label1";
            label1.Size = new Size(439, 65);
            label1.TabIndex = 0;
            label1.Text = "Go explore yourself";
            // 
            // AdminIntroductionUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(102, 153, 155);
            Controls.Add(label1);
            Name = "AdminIntroductionUserControl";
            Size = new Size(920, 478);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Panel panel5;
        private Panel panel3;
        private Panel panel2;
        private Panel panel6;
        private Panel panel8;
        private Panel panel7;
        private Panel panel4;
        private Panel panel9;
        private Panel panel10;
        private Panel panel11;
        private Panel panel12;
        private Panel panel13;
        private Panel panel14;
        private Panel panel16;
        private Panel panel17;
        private Panel panel27;
        private Panel panel22;
        private Panel panel18;
        private Panel panel25;
        private Panel panel32;
        private Panel panel24;
        private Panel panel19;
        private Panel panel26;
        private Panel panel31;
        private Panel panel23;
        private Panel panel20;
        private Panel panel28;
        private Panel panel30;
        private Panel panel29;
        private Panel panel21;
        private System.Windows.Forms.Timer ScrollTimer;
        private Label label1;
    }
}

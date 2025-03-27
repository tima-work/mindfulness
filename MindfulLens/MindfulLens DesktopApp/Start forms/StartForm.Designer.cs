namespace MindfulLens_DesktopApp.Start_forms
{
    partial class StartForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            TryAgainButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(180, 63);
            label1.Name = "label1";
            label1.Size = new Size(440, 81);
            label1.TabIndex = 0;
            label1.Text = "You have to be connected to Cisco to be able to access desktop application";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TryAgainButton
            // 
            TryAgainButton.BackColor = Color.FromArgb(128, 164, 237);
            TryAgainButton.FlatStyle = FlatStyle.Flat;
            TryAgainButton.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TryAgainButton.ForeColor = Color.Black;
            TryAgainButton.Location = new Point(319, 159);
            TryAgainButton.Name = "TryAgainButton";
            TryAgainButton.Size = new Size(150, 60);
            TryAgainButton.TabIndex = 1;
            TryAgainButton.Text = "Try again";
            TryAgainButton.UseVisualStyleBackColor = false;
            TryAgainButton.Click += TryAgainButton_Click;
            // 
            // StartForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(102, 153, 155);
            ClientSize = new Size(800, 450);
            Controls.Add(TryAgainButton);
            Controls.Add(label1);
            Name = "StartForm";
            Text = "StartForm";
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Button TryAgainButton;
    }
}
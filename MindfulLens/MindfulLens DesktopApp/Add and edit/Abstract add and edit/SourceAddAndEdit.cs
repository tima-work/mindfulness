using Logic_Layer.Managers;
using MindfulLens_DesktopApp.My_tools;
using MindfulLens_DesktopApp.My_tools.Other_stuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.Add_and_edit.Abstract_add_and_edit
{
    public abstract class SourceAddAndEdit : BaseAddAndEdit
    {
        protected Label NameLabel, AuthorLabel, LinksLabel, DescriptionLabel, AddLinkLabel;
        public CustomTextBox NameTextBox { get; protected set;}
        public CustomTextBox AuthorTextBox { get; protected set;}
        public CustomTextBox DescriptionTextBox { get; protected set; }
        protected LinksScroll linksScroll;
        protected LinksCreator linksCreator;
        protected SourceManager sourceManager;
        protected SourceAddAndEdit(string forwardButtonText, GraphicFeatures graphicFeatures, Size size, SourceManager sourceManager) : base(forwardButtonText, graphicFeatures, size)
        {
            this.sourceManager = sourceManager;

            Font font = new Font("Segoe UI", 15);
            int label_textbox_spacing = 20;
            Color outlineColor = Color.Black;
            Color BackTextColor = Color.FromArgb(255, 196, 255, 178);
            Color TextColor = Color.Black;


            NameLabel = new Label()
            {
                Text = "Name:",
                ForeColor = Color.White,
                Font = font,
                Location = new Point(padding, BackButton.Height + label_textbox_spacing),
                AutoSize = true,
                BorderStyle = BorderStyle.None
            };


            NameTextBox = new CustomTextBox(outlineColor, BackTextColor, TextColor, "", new Size(300, 60), new Point(NameLabel.Location.X, NameLabel.Location.Y + NameLabel.Height + label_textbox_spacing), font, graphicFeatures, true);
            NameTextBox.textBox.ScrollBars = ScrollBars.Vertical;


            AuthorLabel = new Label()
            {
                Text = "Author: ",
                Font = font,
                ForeColor = Color.White,
                Location = new Point(NameTextBox.Location.X + NameTextBox.Width + label_textbox_spacing, NameLabel.Location.Y),
                AutoSize = true,
                BorderStyle = BorderStyle.None
            };


            AuthorTextBox = new CustomTextBox(outlineColor, BackTextColor, TextColor, "", new Size(NameTextBox.Width, NameTextBox.Height), new Point(AuthorLabel.Location.X, NameTextBox.Location.Y), font, graphicFeatures, true);
            AuthorTextBox.textBox.ScrollBars = ScrollBars.Vertical;


            DescriptionLabel = new Label()
            {
                Text = "Description:",
                Font = font,
                ForeColor = Color.White,
                Location = new Point(NameTextBox.Location.X, AuthorTextBox.Location.Y + AuthorTextBox.Height + label_textbox_spacing),
                AutoSize = true,
                BorderStyle = BorderStyle.None
            };


            DescriptionTextBox = new CustomTextBox(outlineColor, BackTextColor, TextColor, "", new Size(AuthorTextBox.Location.X + AuthorTextBox.Width - NameTextBox.Location.X, this.Height - label_textbox_spacing - DescriptionLabel.Location.Y - DescriptionLabel.Height - label_textbox_spacing), new Point(DescriptionLabel.Location.X, DescriptionLabel.Location.Y + DescriptionLabel.Height +label_textbox_spacing), font, graphicFeatures, true);
            DescriptionTextBox.textBox.ScrollBars = ScrollBars.Vertical;


            LinksLabel = new Label()
            {
                Text = "Links: ",
                Font = font,
                ForeColor = Color.White,
                Location = new Point(DescriptionTextBox.Location.X + DescriptionTextBox.Width + label_textbox_spacing, NameLabel.Location.Y),
                AutoSize = true,
                BorderStyle = BorderStyle.None
            };


            AddLinkLabel = new Label()
            {
                Text = "Add new link",
                Font = new Font("Segoe UI", 15, FontStyle.Underline),
                AutoSize = true,
                BorderStyle = BorderStyle.None,
                ForeColor = Color.White,
                Location = new Point(0, 0)
            };


            AddLinkLabel.MouseEnter += AddLinksLabel_Hover;
            AddLinkLabel.MouseLeave += AddLinksLabel_Unhover;
            AddLinkLabel.Click += AddLinksLabel_Click;


            this.Controls.Add(AddLinkLabel);
            AddLinkLabel.Size = new Size(AddLinkLabel.Bounds.Width, AddLinkLabel.Bounds.Height);
            AddLinkLabel.Location = new Point(LinksLabel.Location.X, ForwardButton.Location.Y - label_textbox_spacing - AddLinkLabel.Height);



            linksScroll = new LinksScroll(new Size(this.Width - (DescriptionTextBox.Location.X + DescriptionTextBox.Width + label_textbox_spacing * 2), AddLinkLabel.Location.Y - (LinksLabel.Location.Y + LinksLabel.Height + label_textbox_spacing * 2)), new Point(LinksLabel.Location.X, LinksLabel.Location.Y + LinksLabel.Height + label_textbox_spacing), new My_tools.Other_stuff.LinkLabel[] { });


            this.Controls.Add(NameLabel);
            this.Controls.Add(NameTextBox);
            this.Controls.Add(AuthorLabel);
            this.Controls.Add(AuthorTextBox);
            this.Controls.Add(LinksLabel);
            this.Controls.Add(linksScroll);
            this.Controls.Add(DescriptionLabel);
            this.Controls.Add(DescriptionTextBox);
        }


        private void AddLinksLabel_Hover(object sender, EventArgs e)
        {
            graphicFeatures.UnableCursor();
            Cursor = Cursors.Hand;
        }

        private void AddLinksLabel_Unhover(object sender, EventArgs e)
        {
            graphicFeatures.EnableCursor();
            Cursor = Cursors.Default;
        }

        private void AddLinksLabel_Click(object sender, EventArgs e)
        {
            linksCreator = new LinksCreator(LinksCreatorEnterPress, LinksCreatorUnfocus);
            linksScroll.AddObject(linksCreator);
            linksCreator.LinkTextBox.Focus();
        }

        private int LinksCreatorEnterPress()
        {
            //LinksCreatorUnfocus();
            linksScroll.AddObject(new My_tools.Other_stuff.LinkLabel(linksCreator.LinkTextBox.Text));
            return 0;
        }

        private int LinksCreatorUnfocus()
        {
            linksScroll.RemoveObject(linksCreator);
            return 0;
        }
    }
}

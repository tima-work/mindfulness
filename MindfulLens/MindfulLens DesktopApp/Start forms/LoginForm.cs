using Data_Infrastructure.Repositories;
using Logic_Layer.Classes;
using Logic_Layer.Custom_exceptions;
using Logic_Layer.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MindfulLens_DesktopApp.Start_forms
{
    public partial class LoginForm : Form
    {
        private CognitivePartRepository cognitivePartRepository;
        private ContentRepository contentRepository;
        private ExerciseRepository exerciseRepository;
        private ReportRepository reportRepository;
        private SourceRepository sourceRepository;
        private UserRepository userRepository;
        private ForumRepository forumRepository;
        private ReviewRepository reviewRepository;



        private CognitivePartManager cognitivePartManager;
        private ContentManager contentManager;
        private ExerciseManager exerciseManager;
        private ReportManager reportManager;
        private SourceManager sourceManager;
        private UserManager userManager;
        //private ForumManager forumRepository;
        //private ReviewR reviewRepository;

        private bool passwordHidden = true;
        public LoginForm()
        {
            InitializeComponent();

            userRepository = new UserRepository();
            cognitivePartRepository = new CognitivePartRepository(userRepository);
            contentRepository = new ContentRepository(userRepository);
            exerciseRepository = new ExerciseRepository(userRepository);
            sourceRepository = new SourceRepository(userRepository);
            forumRepository = new ForumRepository(userRepository);
            reviewRepository = new ReviewRepository(userRepository);
            reportRepository = new ReportRepository(userRepository, reviewRepository, forumRepository, sourceRepository, cognitivePartRepository, exerciseRepository);


            userManager = new UserManager(userRepository);
            cognitivePartManager = new CognitivePartManager(cognitivePartRepository);
            contentManager = new ContentManager(contentRepository);
            exerciseManager = new ExerciseManager(exerciseRepository);
            sourceManager = new SourceManager(sourceRepository);
            reportManager = new ReportManager(reportRepository, forumRepository, reviewRepository, cognitivePartRepository, sourceRepository, exerciseRepository);

    }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (passwordHidden)
            {
                pictureBox1.Image = Properties.Resources.Closed_eye;
            }
            else
            {
                pictureBox1.Image = Properties.Resources.Open_eye_2;
            }
            passwordHidden = !passwordHidden;
            textBox2.UseSystemPasswordChar = passwordHidden;
        }

        private void TryAgainButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
                MessageBox.Show("You haven't entered email");
            else if (string.IsNullOrWhiteSpace(textBox2.Text))
                MessageBox.Show("You haven't entered password");
            else
            {
                try
                {
                    User? user = userManager.Login(textBox1.Text, textBox2.Text);
                    Form? form_to_open = null;
                    if (user != null)
                    {
                        if (user.Importancy == Importancy.User)
                            form_to_open = new VisitorForm(userManager, cognitivePartManager, contentManager, exerciseManager, sourceManager, reportManager, user);
                        else
                            form_to_open = new AdminForm(userManager, cognitivePartManager, contentManager, exerciseManager, sourceManager, reportManager, user);

                        this.Hide();
                        form_to_open.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Something was incorrect. Please, try again");
                    }

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

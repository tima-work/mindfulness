using Microsoft.VisualBasic.Logging;
using MindfulLens_DesktopApp.My_tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MindfulLens_DesktopApp.Start_forms
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();

            CheckIfConnected();
        }

        public void CheckIfConnected()
        {
            if ((NetworkInterface.GetIsNetworkAvailable())
                && NetworkInterface.GetAllNetworkInterfaces()
                                   .FirstOrDefault(ni => ni.Description.Contains("Cisco"))?.OperationalStatus == OperationalStatus.Up)
            {
                LoginForm loginForm = new LoginForm();
                this.Hide();
                loginForm.ShowDialog();
                this.Close();
            }
        }

        private void TryAgainButton_Click(object sender, EventArgs e)
        {
            CheckIfConnected();
        }
    }
}

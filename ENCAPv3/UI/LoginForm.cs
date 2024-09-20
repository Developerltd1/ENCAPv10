using BusinessLogic;
using BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMView.UI
{
    public partial class LoginForm : Form
    {

        public LoginForm()
        {
            InitializeComponent();
        }



        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string _uname = System.Configuration.ConfigurationManager.AppSettings["Value1"].ToString();
                string _upass = System.Configuration.ConfigurationManager.AppSettings["Value2"].ToString();

                string uname = tbUsername.Text;
                string upass = tbPassword.Text;

                if (uname == _uname && upass == _upass)
                {
                    LoginModel.username = uname;
                    LoginModel.password = upass;
                    JIMessageBox.InformationMessage("Login Successfully");
                    SettingsForm settingForm = new SettingsForm();//(MainForm)this.Owner; // Assuming MainForm is the owner of LoginForm
                    settingForm.StartSessionTimer();

                    // Show buttons in SettingsForm
                    foreach (Form form in Application.OpenForms)
                    {
                        if (form is SettingsForm settingsForm)
                        {
                            settingsForm.checkIsGuest();
                        }
                    }

                    this.Close(); // Close the form after a successful login

                }
                else
                {
                    JIMessageBox.WarningMessage("Invalid Credientals, Try Again");
                }
            }
            catch (Exception ex)
            {
                JIMessageBox.ErrorMessage(ex.Message);
            }
        }
    }
}

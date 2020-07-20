using System;
using System.Windows.Forms;

namespace Hello.WinFormsApp
{
    public partial class Form1 : Form
    {
        private const string ErrorMessage = "You didn't enter user name!";

        public Form1()
        {
            InitializeComponent();
        }

        private void EnterUserNameButton_Click(object sender, EventArgs e)
        {
            string userName = UserNameInput.Text;

            if (!string.IsNullOrEmpty(userName))
            {
                IntroduceUser(userName);
            }
            else
            {
                UserIntroductionLabel.Text = ErrorMessage;
            }
        }

        public void IntroduceUser(string userName)
        {
            var introducer = new Introducer.Introducer();
            string introductionMessage = introducer.GetIntroductionMessage(userName);
            UserIntroductionLabel.Text = introductionMessage;
        }
    }
}

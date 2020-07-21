using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Hello.XamarinApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private const string ErrorMessage = "You didn't enter user name!";

        public MainPage()
        {
            InitializeComponent();
        }

        private void SubmitUserNameButton_Clicked(object sender, EventArgs e)
        {
            string userName = UserNameEntry.Text;

            if (!string.IsNullOrEmpty(userName))
            {
                IntroduceUser(userName);
            }
            else
            {
                IntroductionLabel.Text = ErrorMessage;
            }
        }

        public void IntroduceUser(string userName)
        {
            var introducer = new Introducer.Introducer();
            string introductionMessage = introducer.GetIntroductionMessage(userName);
            IntroductionLabel.Text = introductionMessage;
        }
    }
}

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BabySiteApp.Models;
using BabySiteApp.Views;

namespace BabySiteApp
{
    public partial class App : Application
    {
        public static bool IsDevEnv => true;
        public static bool IsBabySitter => true;

        public App()
        {
            InitializeComponent();

            MainPage = new SignUp();
        }

        public User CurrentUser { get; set; }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

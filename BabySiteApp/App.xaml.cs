using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BabySiteApp.Models;
using BabySiteApp.Views;
using System.Collections.Generic;
using BabySiteApp.Services;
using BabySiteApp.DTO;

namespace BabySiteApp
{
    public partial class App : Application
    {
        public static bool IsDevEnv => true;
        public static bool IsBabySitter => true;

        public App()
        {
            InitializeComponent();

            MainPage = new TabbedMainPage();
        }

        public User CurrentUser { get; set; }
        public List<string> Cities { get; set; }
        public List<string> Streets { get; set; }
        protected async override void OnStart()
        {
            BabySiteAPIProxy proxy = BabySiteAPIProxy.CreateProxy();
            this.Streets = await proxy.GetStreetsAsync();
            this.Cities = await proxy.GetCitiesAsync();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

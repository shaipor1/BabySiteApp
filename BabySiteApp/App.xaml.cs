﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BabySiteApp.Models;

namespace BabySiteApp
{
    public partial class App : Application
    {
        public static bool IsDevEnv => true;
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
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

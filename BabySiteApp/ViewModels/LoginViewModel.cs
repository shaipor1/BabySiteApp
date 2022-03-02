using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using BabySiteApp.Services;
using BabySiteApp.Models;
using Xamarin.Essentials;
using System.Linq;
using BabySiteApp.Views;

namespace BabySiteApp.ViewModels
{ 
    class LoginViewModel : BaseViewModels
{


        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        public ICommand SubmitCommand { protected set; get; }
        public ICommand GoToSignUp { protected set; get; }
        public LoginViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
            GoToSignUp = new Command(OnTap);
        }

        public async void OnTap()
        {
            SignUp page = new SignUp();
            await App.Current.MainPage.Navigation.PushAsync(page);
        }
        private string serverStatus;
        public string ServerStatus
        {
            get { return serverStatus; }
            set
            {
                serverStatus = value;
                OnPropertyChanged("ServerStatus");
            }
        }

        public async void OnSubmit()
        {
            ServerStatus = "מתחבר לשרת...";
            
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));
            BabySiteAPIProxy proxy = BabySiteAPIProxy.CreateProxy();
            User user = await proxy.LoginAsync(Email, Password);
            if (user == null)
            {
                await App.Current.MainPage.Navigation.PopModalAsync();
                await App.Current.MainPage.DisplayAlert("שגיאה", "התחברות נכשלה, בדוק שם משתמש וסיסמה ונסה שוב", "בסדר");
                
            }
            else
            {
                ServerStatus = "קורא נתונים...";
                App theApp = (App)App.Current;
                theApp.CurrentUser = user;


                if(theApp.CurrentUser.UserTypeId==1)
                {
                    Page p = new NavigationPage(new Views.ParentMainPage());
                    App.Current.MainPage = p;
                }
                else
                {
                    Page p = new NavigationPage(new Views.BabySitterMainPage());
                    App.Current.MainPage = p;
                }
                //Page p = new NavigationPage(new Views.HomePage());
                //App.Current.MainPage = p;
            }


            
        }

        
    }

}
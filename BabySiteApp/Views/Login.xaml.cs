using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabySiteApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BabySiteApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {

            this.Title = "התחברות";
                this.BindingContext = new LoginViewModel();
                InitializeComponent();
            }


            private void Password_Focused(object sender, FocusEventArgs e)
            {
                Entry entry = (Entry)sender;
                entry.IsPassword = true;
            }

        }
        
        
    
}
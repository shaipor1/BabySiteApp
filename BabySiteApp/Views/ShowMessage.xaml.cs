using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabySiteApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BabySiteApp.ViewModels;

namespace BabySiteApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowMessage : ContentPage
    {
        public ShowMessage(Massage obj)
        {
            this.BindingContext = new ShowMessage(obj);
            InitializeComponent();
        }
    }
}
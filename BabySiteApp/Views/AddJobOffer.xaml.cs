using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BabySiteApp.ViewModels;

namespace BabySiteApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddJobOffer : ContentPage
    {
        public AddJobOffer()
        {
            BindingContext = new AddJobOfferViewModel();
            InitializeComponent(); 
        }
    }
}
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
    public partial class ViewJobOffers : ContentPage
    {
        public ViewJobOffers()
        {
            BindingContext = new ViewJobOffersViewModel();
            InitializeComponent();
        }
    }
}
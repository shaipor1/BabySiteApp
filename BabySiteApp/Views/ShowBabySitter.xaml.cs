using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabySiteApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BabySiteApp.Models;

namespace BabySiteApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowBabySitter : ContentPage
    {
        public ShowBabySitter(BabySitter obj)
        {
            this.BindingContext = new ShowBabySitterViewModel(obj);
            InitializeComponent();
        }
    }
}
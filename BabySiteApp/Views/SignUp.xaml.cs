using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabySiteApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BabySiteApp.Views;

namespace BabySiteApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {
        public SignUp()
        {
            SignUpViewModels vm = new SignUpViewModels();
            this.BindingContext = vm;
            vm.SetImageSourceEvent += OnSetImageSource;
            InitializeComponent();
        }
        public void OnSetImageSource(ImageSource imageSource)
        {
            theImage.Source = imageSource;
        }


    }
}
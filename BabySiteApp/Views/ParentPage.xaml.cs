
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
    public partial class ParentPage : ContentPage
    {
        public ParentPage()
        {
           
            ParentPageViewModels vm = new ParentPageViewModels();
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
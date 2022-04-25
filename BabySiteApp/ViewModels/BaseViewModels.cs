using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;

namespace BabySiteApp.ViewModels
{
    public abstract class BaseViewModels:INotifyPropertyChanged
    {

        protected App CurrentApp = (App)Application.Current;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            var args = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this,args);
        }
    }
}

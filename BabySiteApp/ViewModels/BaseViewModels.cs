using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace BabySiteApp.Models
{
    public abstract class BaseViewModels:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            var args = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this,args);
        }
    }
}

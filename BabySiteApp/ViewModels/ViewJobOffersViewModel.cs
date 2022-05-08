using BabySiteApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using BabySiteApp.Views;
using BabySiteApp.ViewModels;
using BabySiteApp.Services;

namespace BabySiteApp.ViewModels
{
    class ViewJobOffersViewModel:BaseViewModels
    {
        #region properties
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                if (isRefreshing != value)
                {
                    isRefreshing = value;
                    OnPropertyChanged("IsRefreshing");
                }
            }
        }
        

        private ObservableCollection<Massage> jobOffers;
        public ObservableCollection<Massage> JobOffers
        {
            get => jobOffers;
            set
            {
                if (jobOffers != value)
                {
                    jobOffers = value;
                    OnPropertyChanged("JobOffers");
                }
            }
        }

        

        #endregion

        #region COMMANDS

        public ICommand RefreshCommand
        {
            get; protected set;
        }

        public ICommand DeleteJobOffer
        {
            get; protected set;
        }
        public ICommand AddJobOffer
        {
            get; protected set;
        }
        #endregion
        public ViewJobOffersViewModel()
        {

            DeleteJobOffer = new Command(OnDelete);
            RefreshCommand = new Command(OnRefresh);
            AddJobOffer = new Command(AddJobPage);
            InitJobOffers();
        }

        private async void InitJobOffers()
        {
            BabySiteAPIProxy proxy = BabySiteAPIProxy.CreateProxy();
            //this.jobOffers = await proxy.GetJobOffers();
          
        }

        private void AddJobPage()
        {
            CurrentApp.MainPage.Navigation.PushAsync(new AddJobOffer() { BindingContext = new AddJobOfferViewModel() });
            //CurrentApp.MainPage.DisplayAlert("מעבר לעמוד בו הורה יכול לפרסם הצעת עבודה", "baby sitter page", "אישור");
        }

        private void OnDelete()
        {
            
        }

        private void OnRefresh(object obj)
        {
            this.IsRefreshing = true;
            InitJobOffers();
        }

     
       
    }
}

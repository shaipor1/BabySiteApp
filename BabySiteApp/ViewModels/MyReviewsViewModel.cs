using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using BabySiteApp.Services;
using BabySiteApp.Models;
using Xamarin.Essentials;
using System.Linq;
using BabySiteApp.Views;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;
using System.Collections.ObjectModel;
using BabySiteApp.DTO;
namespace BabySiteApp.ViewModels
{
    class MyReviewsViewModel:BaseViewModels
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
   

        private List<Review> reviews;

        private ObservableCollection<Review> filteredReviews;
        public ObservableCollection<Review> FilteredReviews
        {
            get => filteredReviews;
            set
            {
                if (filteredReviews != value)
                {
                    filteredReviews = value;
                    OnPropertyChanged("FilteredMessages");
                }
            }
        }
        private Review currntReview;
        public Review CurrentReview
        {
            get => currntReview;
            set
            {
                if (currntReview != value)
                {
                    currntReview = value;
                    OnPropertyChanged("CurrentReview");
                }
            }
        }

        #region COMMANDS

        public ICommand FilterCommand

        {
            get; protected set;
        }
        public ICommand RefreshCommand
        {
            get; protected set;
        }
        public ICommand CallParent
        {
            get; protected set;
        }

        #endregion
        public MyReviewsViewModel()
        {
           
            FilterCommand = new Command(FilterReviews);
            RefreshCommand = new Command(OnRefresh);
            FilteredReviews = new ObservableCollection<Review>();
            InitReviews();
            this.IsRefreshing = false;
        }
        private async void InitReviews()
        {
            BabySiteAPIProxy proxy = BabySiteAPIProxy.CreateProxy();
            this.reviews = await proxy.GetReviewsBabySitter();
            FilteredReviews.Clear();
            FilterReviews();
            this.IsRefreshing = false;
          
        }
        private void OnRefresh(object obj)
        {
            this.IsRefreshing = true;
            InitReviews();
        }
        private void FilterReviews()
        {
            foreach (Review b in this.reviews)
            {
                    if (!this.FilteredReviews.Contains(b))
                        this.FilteredReviews.Add(b);
                
                else
                {
                    if (this.FilteredReviews.Contains(b))
                        this.FilteredReviews.Remove(b);
                }
            }
            IsRefreshing = false;
        }

       

        #endregion
    }
}

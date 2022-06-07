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
    class FindBabySitterViewModel : BaseViewModels
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
        private bool hasCarFilter;
        public bool HasCarFilter
        {
            get => hasCarFilter;

            set
            {
                if (hasCarFilter != value)
                {
                    hasCarFilter = value;
                    OnPropertyChanged("HasCarValue");
                }
            }
        }

        private int minRatingFilter;
        public int MinRatingFilter
        {
            get => minRatingFilter;

            set
            {
                if (minRatingFilter != value)
                {
                    minRatingFilter = value;
                    OnPropertyChanged("MinRatingFilter");
                }
            }
        }

        private int maxSalaryFilter;
        public int MaxSalaryFilter
        {
            get => maxSalaryFilter;

            set
            {
                if (maxSalaryFilter != value)
                {
                    maxSalaryFilter = value;
                    OnPropertyChanged("MaxSalaryFilter");
                }
            }
        }

        private ObservableCollection<BabySitter> filteredBabySitters;
        public ObservableCollection<BabySitter> FilteredBabySitters
        {
            get => filteredBabySitters;
            set
            {
                if (filteredBabySitters != value)
                {
                    filteredBabySitters = value;
                    OnPropertyChanged("FilteredBabySitters");
                }
            }
        }

        private List<BabySitter> babySitters;
        private BabySitter currntBabySitter;
        public BabySitter CurrentBabySitter
        {
            get => currntBabySitter;
            set
            {
                if (currntBabySitter != value)
                {
                    currntBabySitter = value;
                    OnPropertyChanged("CurrentBabySitter");
                }
            }
        }

        #endregion

        #region COMMANDS
        public ICommand FilterCommand

        {
            get; protected set;
        }
        public ICommand RefreshCommand
        {
            get; protected set;
        }
        public ICommand ShowBabySitter
        {
            get; protected set;
        }
        public ICommand CallBabySitter
        {
            get; protected set;
        }
        public ICommand AddJobOfferCommand
        {
            get; protected set;
        }
        #endregion
        public FindBabySitterViewModel()
        {
            ShowBabySitter = new Command<BabySitter>(ShowBabySittersPage);
            FilterCommand = new Command(FilterBabySitters);
            RefreshCommand = new Command(OnRefresh);
            CallBabySitter = new Command<BabySitter>(OnCall);
            AddJobOfferCommand = new Command(AddJobPage);
            FilteredBabySitters = new ObservableCollection<BabySitter>();
            InitBabySitters();
            this.HasCarFilter = false;
            this.MaxSalaryFilter = 100;
            this.MinRatingFilter = 0;
            this.IsRefreshing = false;
        }

        private async void InitBabySitters()
        {
            BabySiteAPIProxy proxy = BabySiteAPIProxy.CreateProxy();
            this.babySitters = await proxy.GetBabySitters();
            FilteredBabySitters.Clear();
            FilterBabySitters();
        }

        private async void AddJobPage()
        {
            AddJobOffer page = new AddJobOffer();
            await App.Current.MainPage.Navigation.PushAsync(page);
          
        }

        private void OnCall(BabySitter b)
        {
            PhoneDialer.Open(b.User.PhoneNumber);
        }

        private void OnRefresh(object obj)
        {
            this.IsRefreshing = true;
            InitBabySitters();
        }

        private void FilterBabySitters()
        {
            foreach(BabySitter b in this.babySitters)
            {
                if ((HasCarFilter && b.HasCar || !HasCarFilter) &&
                    (b.RatingAverage >= MinRatingFilter) &&
                    (b.Salary <= MaxSalaryFilter ))
                {
                    if (!this.FilteredBabySitters.Contains(b))
                        this.FilteredBabySitters.Add(b);
                }
                else
                {
                    if (this.FilteredBabySitters.Contains(b))
                        this.FilteredBabySitters.Remove(b);
                }
            }
            IsRefreshing = false;
        }

        private void ShowBabySittersPage(BabySitter obj)
        {
            CurrentApp.MainPage.Navigation.PushAsync(new ShowBabySitter(obj));
          
        }
    }
}

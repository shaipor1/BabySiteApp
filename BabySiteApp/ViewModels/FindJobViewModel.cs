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
using System.Linq;

namespace BabySiteApp.ViewModels
{
    class FindJobViewModel:BaseViewModels
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
        private bool hasNoDogFilter;
        public bool HasNoDogFilter
        {
            get => hasNoDogFilter;

            set
            {
                if (hasNoDogFilter != value)
                {
                    hasNoDogFilter = value;
                    OnPropertyChanged("HasNoDogValue");
                }
            }
        }

        private int minAgeFilter;
        public int MinAgeFilter
        {
            get => minAgeFilter;

            set
            {
                if (minAgeFilter != value)
                {
                    minAgeFilter = value;
                    OnPropertyChanged("MinAgeFilter");
                }
            }
        }

        private int maxCountFilter;
        public int MaxCountFilter
        {
            get => maxCountFilter;

            set
            {
                if (maxCountFilter != value)
                {
                    maxCountFilter = value;
                    OnPropertyChanged("MaxSalaryFilter");
                }
            }
        }

        private double radiusFilter;
        public double RadiusFilter
        {
            get => radiusFilter;

            set
            {
                if (radiusFilter != value)
                {
                    radiusFilter = value;
                    OnPropertyChanged("RadiusFilter");
                }
            }
        }

        private ObservableCollection<Massage> filteredMessages;
        public ObservableCollection<Massage> FilteredMessages
        {
            get => filteredMessages;
            set
            {
                if (filteredMessages != value)
                {
                    filteredMessages = value;
                    OnPropertyChanged("FilteredMessages");
                }
            }
        }

        private List<Massage> messages;
        private Massage currntMessage;
        public Massage CurrentMessage
        {
            get => currntMessage;
            set
            {
                if (currntMessage != value)
                {
                    currntMessage = value;
                    OnPropertyChanged("CurrentMessage");
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
        public ICommand ShowMessage
        {
            get; protected set;
        }
        public ICommand CallParent
        {
            get; protected set;
        }
       
        #endregion

        public FindJobViewModel()
        {
            ShowMessage = new Command<Massage>(ShowMessagePage);
            FilterCommand = new Command(FilterMessages);
            RefreshCommand = new Command(OnRefresh);
            CallParent = new Command<Massage>(OnCall);
            
            FilteredMessages = new ObservableCollection<Massage>();
            InitMessages();
            this.IsRefreshing = false;
            this.MinAgeFilter = 0;
            this.MaxCountFilter = 10;
            this.HasNoDogFilter = false;
            this.RadiusFilter = 100;
            
        }
        private async void InitMessages()
        {
            BabySiteAPIProxy proxy = BabySiteAPIProxy.CreateProxy();
            this.messages = await proxy.GetMessages();
            FilteredMessages.Clear();
            FilterMessages();
        }


        private void OnCall(Massage m)
        {
            PhoneDialer.Open(m.User.PhoneNumber);
        }

        private void OnRefresh(object obj)
        {
            this.IsRefreshing = true;
            InitMessages();
        }

        private void FilterMessages()
        {
            foreach (Massage b in this.messages)
            {
                double distance = Location.CalculateDistance((double)b.User.Latitude, (double)b.User.Longitude, (double)CurrentApp.CurrentUser.Latitude, (double)CurrentApp.CurrentUser.Longitude, DistanceUnits.Kilometers);

                if (
                    ((HasNoDogFilter && !b.User.Parents.Where( p=>p.UserId==b.UserId ).FirstOrDefault().HasDog ) || !HasNoDogFilter) &&
                    (b.User.Parents.Where(p => p.UserId == b.UserId).FirstOrDefault().ChildrenMinAge >= MinAgeFilter) &&
                    (b.User.Parents.Where(p => p.UserId == b.UserId).FirstOrDefault().ChildrenCount <= MaxCountFilter)&&(distance<=RadiusFilter)
                    )
                {
                    if (!this.FilteredMessages.Contains(b))
                        this.FilteredMessages.Add(b);
                }
                else
                {
                    if (this.FilteredMessages.Contains(b))
                        this.FilteredMessages.Remove(b);
                }
            }
            IsRefreshing = false;
        }

        private void ShowMessagePage(Massage obj)
        {
            CurrentApp.MainPage=new Views.ShowMessage(obj);
            //CurrentMessage = null;
            //CurrentApp.MainPage.Navigation.PushAsync(new ShowBabySitter());
            //CurrentApp.MainPage.DisplayAlert("ללעמוד שמציג פרטים על הבייביסיטר", "baby sitter page", "אישור");
        }
    }
}

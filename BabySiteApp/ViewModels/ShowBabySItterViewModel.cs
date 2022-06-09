
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
    class ShowBabySitterViewModel:BaseViewModels
    {
        #region properties
        #region מקור התמונה
        private string userImgSrc;

        public string UserImgSrc
        {
            get => userImgSrc;
            set
            {
                userImgSrc = value;
                OnPropertyChanged("UserImgSrc");
            }
        }
        #endregion
        #region ServerStatus
        private string serverStatus;
        public string ServerStatus
        {
            get { return serverStatus; }
            set
            {
                serverStatus = value;
                OnPropertyChanged("ServerStatus");
            }
        }
        #endregion
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

        private string firstName;

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        private string lastName;

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        private string gender;

        public string Gender
        {
            get => gender;
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }
        private string phoneNum;

        public string PhoneNum
        {
            get => phoneNum;
            set
            {
                phoneNum = value;
                OnPropertyChanged("PhoneNum");
            }
        }

        private string email;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        private int age;

        public int Age
        {
            get => age;
            set
            {
                age = value;
                OnPropertyChanged("Age");
            }
        }

        private string adress;

        public string Adress
        {
            get => adress;
            set
            {
                adress = value;
                OnPropertyChanged("Adress");
            }
        }

        private int salary;
        public int Salary
        {
            get => salary;
            set
            {
                salary = value;
                OnPropertyChanged("Salary");
            }
        }

        private bool hasCar;
        public bool HasCar
        {
            get => hasCar;
            set
            {
                hasCar = value;
                OnPropertyChanged("HasCar");
            }
        }


        private string ratingAveregePic;

        public string RatingAveregePic
        {
            get => ratingAveregePic;
            set
            {
                ratingAveregePic = value;
                OnPropertyChanged("RatingAveregePic");
            }
        }

        private int rating;
        public int Rating
        {
            get => rating;
            set
            {
                rating = value;
                OnPropertyChanged("Rating");
            }
        }

        private string reviewBody;

        public string ReviewBody
        {
            get => reviewBody;
            set
            {
                reviewBody = value;
                OnPropertyChanged("ReviewBody");
            }
        }

        private bool showStar1;
        public bool ShowStar1
        {
            get => showStar1;
            set
            {
                showStar1 = value;
                OnPropertyChanged("ShowStar1");
            }
        }
        private bool showStar2;
        public bool ShowStar2
        {
            get => showStar2;
            set
            {
                showStar2 = value;
                OnPropertyChanged("ShowStar2");
            }
        }
        private bool showStar3;
        public bool ShowStar3
        {
            get => showStar3;
            set
            {
                showStar3 = value;
                OnPropertyChanged("ShowStar3");
            }
        }

        private bool showStar4;
        public bool ShowStar4
        {
            get => showStar4;
            set
            {
                showStar4 = value;
                OnPropertyChanged("ShowStar4");
            }
        }
        private bool showStar5;
        public bool ShowStar5
        {
            get => showStar5;
            set
            {
                showStar5 = value;
                OnPropertyChanged("ShowStar5");
            }
        }
        private BabySitter babySitter;
       

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
        #endregion

        #region COMMANDS
        public ICommand PostReviewCommand

        {
            get; protected set;
        }
       
        public ICommand CallBabySitterCommand
        {
            get; protected set;
        }
        public ICommand RefreshCommand
        {
            get; protected set;
        }
        #endregion

        public ShowBabySitterViewModel(BabySitter b)
        {
            babySitter = b;
            this.UserImgSrc = b.User.PhotoURL;
            this.FirstName = b.User.FirstName;
            this.LastName = b.User.LastName;
            this.Gender = b.User.Gender;
            this.PhoneNum = b.User.PhoneNumber;
            this.Email = b.User.Email;
            TimeSpan timeSpan = DateTime.Now - b.User.BirthDate;
            this.Age = timeSpan.Days / 365;
            this.Adress = b.User.City + b.User.Street + b.User.House;
            this.salary = b.Salary;
            this.HasCar = b.HasCar;
            if (b.RatingAverage == 1)
                this.ShowStar1 = true;
            else
                this.ShowStar1 = false;
            if (b.RatingAverage == 2)
            {
                this.ShowStar1 = true;
                 this.ShowStar2 = true;
            }
                
            else
                this.ShowStar2 = false;
            if (b.RatingAverage == 3)
            {
                this.ShowStar1 = true;
                this.ShowStar2 = true;

                this.ShowStar3 = true;
            }

            else
                this.ShowStar3 = false;

            if (b.RatingAverage == 4)
            {
                this.ShowStar1 = true;
                this.ShowStar2 = true;
                this.ShowStar3 = true;
                this.ShowStar4 = true;

            }

            else
                this.ShowStar4 = false;
            if (b.RatingAverage == 5)
            {
                this.ShowStar1 = true;
                this.ShowStar2 = true;
                this.ShowStar3 = true;
                this.ShowStar4 = true;
                this.ShowStar5 = true;

            }

            else
                this.ShowStar4 = false;

            this.PostReviewCommand = new Command(PostReview);
            this.CallBabySitterCommand = new Command(OnCall);
            this.ServerStatus = string.Empty;
            FilteredReviews = new ObservableCollection<Review>();
            InitReviews();
            this.IsRefreshing = false;
        }


        private async void InitReviews()
        {
            BabySiteAPIProxy proxy = BabySiteAPIProxy.CreateProxy();
            this.reviews = await proxy.GetReviewsParent();
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
                if (b.BabySitterId==babySitter.BabySitterId && !this.FilteredReviews.Contains(b))
                    this.FilteredReviews.Add(b);

                else
                {
                    if (this.FilteredReviews.Contains(b))
                        this.FilteredReviews.Remove(b);
                }
            }
            IsRefreshing = false;
        }

        private void OnCall()
        {
            PhoneDialer.Open(this.babySitter.User.PhoneNumber);
        }

        private async void PostReview()
        {

            App a = (App)App.Current;

            Review newReview = new Review()
            {
                Rating = this.Rating,
                BabySitterId = babySitter.BabySitterId,
                ParentId=a.CurrentParent.ParentId,
                Decription=this.ReviewBody

            };

            
            BabySiteAPIProxy proxy = BabySiteAPIProxy.CreateProxy();
            int b = await proxy.PostReviewAsync(newReview);
            ServerStatus = "מעלה את ההצעה..";
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));
            if (b!=-1)
            {
                babySitter.RatingAverage = b;
                await App.Current.MainPage.DisplayAlert("בוצע", "הדירוג פורסם בהצלחה", "אישור", FlowDirection.RightToLeft);
                await App.Current.MainPage.Navigation.PopModalAsync();
               

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "היתה תקלה בפרסום ההצעה, נסה שוב", "אישור", FlowDirection.RightToLeft);
            }

        }
    }
}

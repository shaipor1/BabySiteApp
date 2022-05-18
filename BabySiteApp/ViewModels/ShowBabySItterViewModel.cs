
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
                salary = value;
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
        #endregion

        #region COMMANDS
        public ICommand PostReviewCommand

        {
            get; protected set;
        }
       
        public ICommand EmailBabySitterCommand
        {
            get; protected set;
        }
        public ICommand CallBabySitter
        {
            get; protected set;
        }
       
        #endregion

        public ShowBabySitterViewModel(BabySitter b)
        {
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

        }
    }
}

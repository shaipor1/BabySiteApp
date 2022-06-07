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
    class ShowMessageViewModel:BaseViewModels
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
        private string headLine;
        public string HeadLine
        {
            get => HeadLine;
            set
            {
                if (HeadLine != value)
                {
                   headLine = value;
                    OnPropertyChanged("HeadLine");
                }
            }
        }
        private string body;
        public string Body
        {
            get => Body;
            set
            {
                if (Body != value)
                {
                    body = value;
                    OnPropertyChanged("Body");
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

        private int childrenCount;
        public int ChildrenCount
        {
            get => childrenCount;
            set
            {
                childrenCount = value;
                OnPropertyChanged("ChildrenCount");
            }
        }
        private bool hasDog;
        public bool HasDog
        {
            get => hasDog;
            set
            {
                hasDog = value;
                OnPropertyChanged("HasDog");
            }
        }

        private int minAge;
        public int MinAge
        {
            get => minAge;
            set
            {
                minAge = value;
                OnPropertyChanged("MinAge");
            }
        }
        private int maxAge;
        public int MaxAge
        {
            get => maxAge;
            set
            {
                maxAge = value;
                OnPropertyChanged("MaxAge");
            }
        }


        private Massage message;
        #endregion

        #region COMMANDS
      
        public ICommand CallParentCommand
        {
            get; protected set;
        }

        #endregion
        #region constructor
        #endregion

        public ShowMessageViewModel(Massage m)
        {
            message = m;
            this.UserImgSrc = m.User.PhotoURL;
            this.FirstName = m.User.FirstName;
            this.LastName = m.User.LastName;
            this.Gender = m.User.Gender;
            this.PhoneNum = m.User.PhoneNumber;
            this.Email = m.User.Email;
            this.HeadLine = m.HeadLine;
            this.Body = m.Body;
            this.Adress = m.User.City + m.User.Street + m.User.House;
            this.HasDog = m.User.Parents.Where(p => p.UserId == m.UserId).FirstOrDefault().HasDog;
            this.ChildrenCount= m.User.Parents.Where(p => p.UserId == m.UserId).FirstOrDefault().ChildrenCount;
            this.MinAge = m.User.Parents.Where(p => p.UserId == m.UserId).FirstOrDefault().ChildrenMinAge;
            this.MaxAge = m.User.Parents.Where(p => p.UserId == m.UserId).FirstOrDefault().ChildrenMaxAge;

            this.CallParentCommand= new Command(OnCall);


        }

        private void OnCall()
        {
            PhoneDialer.Open(this.message.User.PhoneNumber);
        }
    }
}

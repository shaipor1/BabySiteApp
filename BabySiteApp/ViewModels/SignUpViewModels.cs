using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using BabySiteApp.Services;
using BabySiteApp.Models;
using BabySiteApp.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.Essentials;


namespace BabySiteApp.ViewModels
{
    public static class ERROR_MESSAGES
    {
        public const string REQUIRED_FIELD = "זהו שדה חובה";
        public const string BAD_EMAIL = "מייל לא תקין";
        public const string SHORT_PASS = "סיסמה חייבת להכיל לפחות 6 תווים";
        public const string BAD_PhoneNum = "מספר טלפון לא תקין";
        public const string BAD_UserName= "שם לא תקין";
        public const string BAD_DATE = "המשתמש חייב להיות מעל גיל 12";

    }
    public class SignUpViewModels:BaseViewModels
    {
        #region UserType
        private bool isBabySitter;
        private bool isParent;
        private string userTypeSelection;


        public string UserTypeSelection
        {
            get => userTypeSelection;
            set
            {
                userTypeSelection = value;
                if (userTypeSelection == "Parent")
                    IsBabySitter = false;
                else
                    IsBabySitter = true;
                OnPropertyChanged("UserTypeSelection");
            }
        }
        public bool IsBabySitter
        {
            get => isBabySitter;
            set
            {
                isBabySitter = value;
                OnPropertyChanged("IsBabySitter");
                OnPropertyChanged("IsParent");
            }
        }
        public bool IsParent
        {
            get => !isBabySitter;
            set
            {
                isParent = value;
            }
        }
        #endregion
        #region Name
        private bool showNameError;
        public bool ShowNameError
        {
            get => showNameError;
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowNameError");
            }
        }
        private string nameError;

        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                OnPropertyChanged("NameError");
            }
        }
        private void ValidateFName()
        {
            this.ShowNameError = string.IsNullOrEmpty(FirstName);
        }
        private void ValidateLName()
        {
            this.ShowNameError = string.IsNullOrEmpty(LastName);
        }
        private string firstName;
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                ValidateFName();
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
                ValidateLName();
                OnPropertyChanged("LastName");
            }
        }
        #endregion
        #region PhoneNum
        private bool showPhoneNumError;

        public bool ShowPhoneNumError
        {
            get => showPhoneNumError;
            set
            {
                showPhoneNumError = value;
                OnPropertyChanged("ShowPhoneNumError");
            }
        }

        private string phoneNum;

        public string PhoneNum
        {
            get => phoneNum;
            set
            {
                phoneNum = value;
                ValidatePhoneNum();
                OnPropertyChanged("PhoneNum");
            }
        }

        private string phoneNumError;

        public string PhoneNumError
        {
            get => phoneNumError;
            set
            {
                phoneNumError = value;
                OnPropertyChanged("PhoneNumError");
            }
        }

        private void ValidatePhoneNum()
        {
            this.ShowPhoneNumError = string.IsNullOrEmpty(PhoneNum);
            if (!this.ShowPhoneNumError)
            {
                if (!Regex.IsMatch(this.PhoneNum, @"^[0-9]"))
                {
                    this.ShowPhoneNumError = true;
                    this.PhoneNumError = ERROR_MESSAGES.BAD_PhoneNum;
                }
            }

        }
        #endregion
        #region Email
        private bool showEmailError;

        public bool ShowEmailError
        {
            get => showEmailError;
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }

        private string email;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                ValidateEmail();
                OnPropertyChanged("Email");
            }
        }

        private string emailError;

        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
            }
        }

        private void ValidateEmail()
        {
            this.ShowEmailError = string.IsNullOrEmpty(Email);
            if (!this.ShowEmailError)
            {
                if (!Regex.IsMatch(this.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                {
                    this.ShowEmailError = true;
                    this.EmailError = ERROR_MESSAGES.BAD_EMAIL;
                }
            }

        }
        #endregion
        #region UserName
        private bool showUserNameError;

        public bool ShowUserNameError
        {
            get => showUserNameError;
            set
            {
                showUserNameError = value;
                OnPropertyChanged("ShowUserNameError");
            }
        }

        private string userName;

        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                ValidateUserName();
                OnPropertyChanged("UserName");
            }
        }

        private string userNameError;

        public string UserNameError
        {
            get => userNameError;
            set
            {
                userNameError = value;
                OnPropertyChanged("UserNameError");
            }
        }

        private void ValidateUserName()
        {
            this.ShowUserNameError = string.IsNullOrEmpty(UserName);
        }
      
        #endregion
        #region Password
        private bool showPasswordError;

        public bool ShowPasswordError
        {
            get => showPasswordError;
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPasswordError");
            }
        }

        private string password;

        public string Password
        {
            get => password;
            set
            {
                password = value;
                ValidatePassword();
                OnPropertyChanged("Password");
            }
        }

        private string passwordError;

        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");
            }
        }

        private void ValidatePassword()
        {
            this.ShowPasswordError = string.IsNullOrEmpty(Password);
            if (!this.ShowPasswordError)
            {
                if (this.Password.Length < 6)
                {
                    this.ShowPasswordError = true;
                    this.PasswordError = ERROR_MESSAGES.SHORT_PASS;
                }
            }

        }
        #endregion
        #region Gender
        private string gender;
        public string Gender
        {
            get => gender;
            set
            {
                gender = value;
                ValidateGender();
                OnPropertyChanged("Gender");
            }
        }

        private string genderError;

        public string GenderError
        {
            get => genderError;
            set
            {
                genderError = value;
                OnPropertyChanged("GenderError");
            }
        }

        private void ValidateGender()
        {
            this.ShowGenderError = string.IsNullOrEmpty(Gender);
        }

        private bool showGenderError;
        public bool ShowGenderError
        {
            get => showGenderError;
            set
            {
                showGenderError = value;
                OnPropertyChanged("ShowGenderError");
            }
        }
        #endregion
        #region BirthDate
        private bool showBirthDateError;

        public bool ShowBirthDateError
        {
            get => showBirthDateError;
            set
            {
                showBirthDateError = value;
                OnPropertyChanged("ShowBirthDateError");
            }
        }

        private DateTime birthDate;

        public DateTime BirthDate
        {
            get => birthDate;
            set
            {
                birthDate = value;
                ValidateBirthDate();
                OnPropertyChanged("BirthDate");
            }
        }

        private string birthDateError;

        public string BirthDateError
        {
            get => birthDateError;
            set
            {
                birthDateError = value;
                OnPropertyChanged("BirthDateError");
            }
        }

        private const int MIN_AGE = 12;
        private void ValidateBirthDate()
        {
            TimeSpan ts = DateTime.Now - this.BirthDate;
            this.ShowBirthDateError = ts.TotalDays < (MIN_AGE * 365);
        }
        #endregion
        #region HasCar


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

        #endregion
        #region Salary


        private int salay;
        public int Salary
        {
            get => salay;
            set
            {
                salay = value;

                OnPropertyChanged("Salary");
            }
        }

        #endregion
        #region Age
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

        #endregion
        private ICommand continueCommand;
        public ICommand ContinueCommand
        {
            get=>continueCommand;
            set
            {
                continueCommand = value;
                OnPropertyChanged("ContinueCommand");
            }


        }
        private List<int> ageArray= new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
        public List<int> AgeArray
        {
            get => ageArray;
        }


        public SignUpViewModels()
        {
            UserTypeSelection = "Parent";
            MaxAge = 1;
            MinAge = 1;
            HasCar = false;
            Salary = 75;
            IsBabySitter = false;
            NameError = ERROR_MESSAGES.BAD_UserName;
            ShowNameError = false;
            
            FirstName= string.Empty; 
            LastName= string.Empty;
            PhoneNum= string.Empty; 
            PhoneNumError= ERROR_MESSAGES.BAD_PhoneNum;
            ShowPhoneNumError = false;
            Email= string.Empty;
            EmailError= ERROR_MESSAGES.BAD_EMAIL;
            ShowEmailError = false;
            UserName= string.Empty;
            UserNameError= ERROR_MESSAGES.BAD_UserName;
            ShowUserNameError = false;
            Password= string.Empty;
            PasswordError= ERROR_MESSAGES.SHORT_PASS;
            ShowPasswordError = false;
            Gender= string.Empty; 
            GenderError= ERROR_MESSAGES.REQUIRED_FIELD;
            BirthDate = DateTime.Now;
            BirthDateError= ERROR_MESSAGES.BAD_DATE;
            ShowBirthDateError = false;  
            ContinueCommand = new Command(ShowUserType);
        }

        private void ShowUserType()
        {
            if(UserTypeSelection=="Parent")
            {
                IsBabySitter = false;
            }
            else
            {
                IsBabySitter = true;
            }
        }


    }
}

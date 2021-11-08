using System;
using System.Collections.Generic;
using System.Text;

namespace BabySiteApp.ViewModels
{
    public static class ERROR_MESSAGES
    {
        public const string REQUIRED_FIELD = "זהו שדה חובה";
        public const string BAD_EMAIL = "מייל לא תקין";
        public const string SHORT_PASS = "סיסמה חייבת להכיל לפחות 6 תווים";

    }
    class SignUpViewModels:BaseViewModels
    {
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
        #region email
        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
            }
        }
    }
}

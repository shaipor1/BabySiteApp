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
using BabySiteApp.DTO;


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
        public const string BAD_HOUSE_NUM = "מספר בית לא תקין";
        public const string BAD_CITY = "עיר לא תקינה";
        public const string BAD_STREET = "רחוב לא תקין";

    }
    public class SignUpViewModels:BaseViewModels
    {
        #region cities and streets
        private List<string> allCities;
        private ObservableCollection<string> filteredCities;
        public ObservableCollection<string> FilteredCities
        {
            get
            {
                return this.filteredCities;
            }
            set
            {
                if (this.filteredCities != value)
                {

                    this.filteredCities = value;
                    OnPropertyChanged("FilteredCities");
                }
            }
        }

        private List<string> allStreets;
        private ObservableCollection<string> filteredStreets;
        public ObservableCollection<string> FilteredStreets
        {
            get
            {
                return this.filteredStreets;
            }
            set
            {
                if (this.filteredStreets != value)
                {

                    this.filteredStreets = value;
                    OnPropertyChanged("FilteredStreets");
                }
            }
        }

        #endregion
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
        private void ShowUserType()
        {
            if (UserTypeSelection == "Parent")
            {
                IsBabySitter = false;
            }
            else
            {
                IsBabySitter = true;
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
        private List<int> ageArray = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
        public List<int> AgeArray
        {
            get => ageArray;
        }
        #endregion
        #region command
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
        public ICommand CitySearchCommand { get; }
        #endregion
        #region OnCityChanged
        public void OnCityChanged(string search)
        {
            if (this.City != this.SelectedCityItem)
            {
                this.ShowCities = true;
                this.SelectedCityItem = null;
            }
            //Filter the list of contacts based on the search term
            if (this.allCities == null)
                return;
            if (String.IsNullOrWhiteSpace(search) || String.IsNullOrEmpty(search))
            {
                this.ShowCities = false;
                this.FilteredCities.Clear();
                //foreach (string city in this.allCities)
                //{
                //    if (!this.FilteredCities.Contains(city))
                //        this.FilteredCities.Add(city);
                //}
            }
            else
            {
                foreach (string city in this.allCities)
                {
                    string contactString = city; /*$"{uc.FirstName}|{uc.LastName}|{uc.Email}";*/

                    if (!this.FilteredCities.Contains(city) &&
                        contactString.Contains(search))
                        this.FilteredCities.Add(city);
                    else if (this.FilteredCities.Contains(city) &&
                        !contactString.Contains(search))
                        this.FilteredCities.Remove(city);
                }
            }

            //this.FilteredCities = new ObservableCollection<string>(this.FilteredCities);
        }
        #endregion
        #region City
        private bool showCityError;
        public bool ShowCityError
        {
            get => showCityError;
            set
            {
                showCityError = value;
                OnPropertyChanged("ShowCityError");
            }
        }

        //This property holds the selected city on the collection of cities
        private string selectedCityItem;
        public string SelectedCityItem
        {
            get => selectedCityItem;
            set
            {
                selectedCityItem = value;
                OnPropertyChanged("SelectedCityItem");
            }
        }

        //ShowCities
        private bool showCities;
        public bool ShowCities
        {
            get => showCities;
            set
            {
                showCities = value;
                OnPropertyChanged("ShowCities");
            }
        }

        private string city;
        public string City
        {
            get => city;
            set
            {
                city = value;
                OnCityChanged(value);
                ValidateCity();
                OnPropertyChanged("City");
            }
        }

        private string cityError;
        public string CityError
        {
            get => cityError;
            set
            {
                cityError = value;
                OnPropertyChanged("CityError");
            }
        }

        private void ValidateCity()
        {
            string city = null; 
            this.ShowCityError = string.IsNullOrEmpty(this.City);
            if (!this.ShowCityError)
            {
                if (allCities != null)
                     city = this.allCities.Where(c => c == this.City).FirstOrDefault();
                if (string.IsNullOrEmpty(city))
                {
                    this.ShowCityError = true;
                    this.CityError = ERROR_MESSAGES.BAD_CITY;
                }
            }
            else
                this.CityError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion
        #region Street
        private bool showStreetError;
        public bool ShowStreetError
        {
            get => showStreetError;
            set
            {
                showStreetError = value;
                OnPropertyChanged("ShowStreetError");
            }
        }

        //This property holds the selected street on the collection of streets
        private string selectedStreetItem;
        public string SelectedStreetItem
        {
            get => selectedStreetItem;
            set
            {
                selectedStreetItem = value;
                OnPropertyChanged("SelectedStreetItem");
            }
        }

        //ShowStreets
        private bool showStreets;
        public bool ShowStreets
        {
            get => showStreets;
            set
            {
                showStreets = value;
                OnPropertyChanged("ShowStreets");
            }
        }

        private string street;
        public string Street
        {
            get => street;
            set
            {
                street = value;
                OnStreetChanged(value);
                ValidateStreet();
                OnPropertyChanged("Street");
            }
        }

        private string streetError;
        public string StreetError
        {
            get => streetError;
            set
            {
                streetError = value;
                OnPropertyChanged("StreetError");
            }
        }

        private void ValidateStreet()
        {
            this.ShowStreetError = string.IsNullOrEmpty(this.Street);
            if (!this.ShowStreetError)
            {
                string street = this.allStreets.Where(s => s == this.Street).FirstOrDefault();
                if (string.IsNullOrEmpty(street))
                {
                    this.ShowStreetError = true;
                    this.StreetError = ERROR_MESSAGES.BAD_STREET;
                }
            }
            else
                this.StreetError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region HouseNum
        private bool showHouseNumError;
        public bool ShowHouseNumError
        {
            get => showHouseNumError;
            set
            {
                showHouseNumError = value;
                OnPropertyChanged("ShowHouseNumError");
            }
        }

        private int houseNum;
        public int HouseNum
        {
            get => houseNum;
            set
            {
                houseNum = value;
                ValidateHouseNum();
                OnPropertyChanged("HouseNum");
            }
        }

        private string houseNumError;
        public string HouseNumError
        {
            get => houseNumError;
            set
            {
                houseNumError = value;
                OnPropertyChanged("HouseNumError");
            }
        }

        private void ValidateHouseNum()
        {
            this.ShowHouseNumError = this.HouseNum == 0;
            int i;
            string num = this.HouseNum.ToString();
            if (!this.ShowHouseNumError)
            {
                if (!int.TryParse(num, out i) || int.Parse(num) <= 0/*!Regex.IsMatch(num, @"^[-+]?[0-9]*\.?[0-9]+$")*/)
                {
                    this.ShowHouseNumError = true;
                    this.HouseNumError = ERROR_MESSAGES.BAD_HOUSE_NUM;
                }
            }
            else
                this.HouseNumError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region StringHouseNum
        private bool showStringHouseNumError;
        public bool ShowStringHouseNumError
        {
            get => showStringHouseNumError;
            set
            {
                showStringHouseNumError = value;
                OnPropertyChanged("ShowStringHouseNumError");
            }
        }

        private string stringHouseNum;
        public string StringHouseNum
        {
            get => stringHouseNum;
            set
            {
                stringHouseNum = value;
                ValidateStringHouseNum();
                OnPropertyChanged("StringHouseNum");
            }
        }

        private string stringHouseNumError;
        public string StringHouseNumError
        {
            get => stringHouseNumError;
            set
            {
                stringHouseNumError = value;
                OnPropertyChanged("StringHouseNumError");
            }
        }

        private void ValidateStringHouseNum()
        {
            this.ShowStringHouseNumError = string.IsNullOrEmpty(this.StringHouseNum);
            int i;
            if (!this.ShowStringHouseNumError)
            {
                if (!int.TryParse(this.StringHouseNum, out i) || int.Parse(this.StringHouseNum) <= 0 /*!Regex.IsMatch(this.StringHouseNum, @"^[-+]?[0-9]*\.?[0-9]+$")*/)
                {
                    this.ShowStringHouseNumError = true;
                    this.StringHouseNumError = ERROR_MESSAGES.BAD_HOUSE_NUM;
                }
            }
            else
                this.StringHouseNumError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region OnStreetChanged
        public void OnStreetChanged(string search)
        {
            if (this.Street != this.SelectedStreetItem)
            {
                this.ShowStreets = true;
                this.SelectedStreetItem = null;
            }
            //Filter the list of contacts based on the search term
            if (this.allStreets == null)
                return;
            if (String.IsNullOrWhiteSpace(search) || String.IsNullOrEmpty(search))
            {
                this.ShowStreets = false;
                this.FilteredStreets.Clear();
            }
            else
            {
                foreach (string street in this.allStreets)
                {
                    string contactString = street;

                    if (!this.FilteredStreets.Contains(street) &&
                        contactString.Contains(search))
                        this.FilteredStreets.Add(street);
                    else if (this.FilteredStreets.Contains(street) &&
                        !contactString.Contains(search))
                        this.FilteredStreets.Remove(street);
                }
            }
        }
        #endregion

        #region SelectedCity
        public ICommand SelectedCity => new Command<string>(OnSelectedCity);
        public void OnSelectedCity(string city)
        {
            if (city != null)
            {
                this.ShowCities = false;
                this.City = city;
                //this.FilteredCities.Clear();

                //App theApp = (App)App.Current;
                //AddContactViewModel vm = new AddContactViewModel(uc);
                //vm.ContactUpdatedEvent += OnContactAdded;
                //Page p = new Views.AddContact(vm);
                //await theApp.MainPage.Navigation.PushAsync(p);
                //if (ClearSelection != null)
                //    ClearSelection();
            }
        }

        //public event Action ClearSelection;
        #endregion

        #region SelectedStreet
        public ICommand SelectedStreet => new Command<string>(OnSelectedStreet);
        public void OnSelectedStreet(string street)
        {
            if (street != null)
            {
                this.ShowStreets = false;
                this.Street = street;
            }
        }
        #endregion
        #region constractor
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
            CitySearchCommand = new Command<string>(OnCityChanged);
        }
        #endregion
     


    }
}

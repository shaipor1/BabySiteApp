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
using Xamarin.Forms.Maps;

namespace BabySiteApp.ViewModels
{
    public static class ERROR_MESSAGES
    {
        public const string REQUIRED_FIELD = "זהו שדה חובה";
        public const string BAD_EMAIL = "מייל לא תקין";
        public const string SHORT_PASS = "סיסמה חייבת להכיל לפחות 6 תווים";
        public const string BAD_PhoneNum = "מספר טלפון לא תקין";
        public const string BAD_UserName = "שם לא תקין";
        public const string BAD_DATE = "המשתמש חייב להיות מעל גיל 18";
        public const string BAD_HOUSE_NUM = "מספר בית לא תקין";
        public const string BAD_CITY = "עיר לא תקינה";
        public const string BAD_STREET = "רחוב לא תקין";
        public const string BAD_AGE = "גיל הילדים יכול להיות מספר בין 1 ל 18";
        public const string BAD_COUNT = "מספר הילדים צריך להיות בין 1 ל 10";


    }
    public class SignUpViewModels : BaseViewModels
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

        private List<StreetDTO> allStreets;
        private ObservableCollection<StreetDTO> filteredStreets;
        public ObservableCollection<StreetDTO> FilteredStreets
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
        private const string DEFAULT_PHOTO_SRC = "defaultphoto.png";
        #endregion
        ///The following command handle the pick photo button
        FileResult imageFileResult;
        public event Action<ImageSource> SetImageSourceEvent;
        public ICommand PickImageCommand => new Command(OnPickImage);
        public async void OnPickImage()
        {
            FileResult result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions()
            {
                Title = "בחר תמונה"
            });

            if (result != null)
            {
                this.imageFileResult = result;

                var stream = await result.OpenReadAsync();
                ImageSource imgSource = ImageSource.FromStream(() => stream);
                if (SetImageSourceEvent != null)
                    SetImageSourceEvent(imgSource);
            }
        }

        ///The following command handle the take photo button
        public ICommand CameraImageCommand => new Command(OnCameraImage);
        public async void OnCameraImage()
        {
            var result = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions()
            {
                Title = "צלם תמונה"
            });

            if (result != null)
            {
                this.imageFileResult = result;
                var stream = await result.OpenReadAsync();
                ImageSource imgSource = ImageSource.FromStream(() => stream);
                if (SetImageSourceEvent != null)
                    SetImageSourceEvent(imgSource);
            }
        }
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
                if (!Regex.IsMatch(this.PhoneNum, @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$"))
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
        //private int genderId;
        //public int GenderId
        //{
        //    get => genderId;
        //    set
        //    {
        //        genderId = value;
        //        OnPropertyChanged("GenderId");
        //    }
        //}
        public ObservableCollection<string> GenderArray { get; }
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

        private const int MIN_AGE = 18;
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
        private bool showAgeError;
        public bool ShowAgeError
        {
            get => showAgeError;
            set
            {
                showAgeError = value;
                OnPropertyChanged("ShowAgeError");
            }
        }
        private string ageError;
        public string AgeError
        {
            get => ageError;
            set
            {
                ageError = value;
                OnPropertyChanged("AgeError");
            }
        }
        private void ValidateAge()
        {
            if (string.IsNullOrEmpty(ChildrenCount.ToString()) || MinAge < 1 || MinAge > 18 || MaxAge < 1 || MaxAge > 18||MinAge>MaxAge)
                this.showAgeError = true;
            else
                this.showAgeError = false;
        }
        #endregion
        #region children count
        private bool showChildrenCountError;
        public bool ShowChildrenCountError
        {
            get => showChildrenCountError;
            set
            {
                showChildrenCountError = value;
                OnPropertyChanged("ShowChildrenCountError");
            }
        }
        private string childrenCountError;
        public string ChildrenCountError
        {
            get => childrenCountError;
            set
            {
                childrenCountError = value;
                OnPropertyChanged("ChildrenCountError");
            }
        }
        private void ValidateChildrenCount()
        {
            if (string.IsNullOrEmpty(ChildrenCount.ToString()) || ChildrenCount < 1 || ChildrenCount > 10)
                this.showChildrenCountError = true;
            else
                this.showChildrenCountError = false;
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
        #endregion
        #region HasDog


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

        #endregion
        #region command
        private ICommand continueCommand;
        public ICommand ContinueCommand
        {
            get => continueCommand;
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
            //if (!string.IsNullOrEmpty(this.SelectedCityItem))
            //{
            if (this.City != this.SelectedCityItem)
            {
                this.ShowCities = true;
                this.SelectedCityItem = null;
            }
            //}
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
                if (city != value)
                {
                    city = value;
                    OnCityChanged(value);
                    ValidateCity();
                    OnPropertyChanged("City");
                }
                
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
                if (street != value)
                {
                    street = value;
                    OnStreetChanged(value);
                    ValidateStreet();
                    OnPropertyChanged("Street");
                }
                
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
                StreetDTO street = this.allStreets.Where(s => s.street_name == this.Street).FirstOrDefault();
                if (string.IsNullOrEmpty(street?.street_name))
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

        private string houseNum;
        public string HouseNum
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
            this.ShowHouseNumError = String.IsNullOrEmpty(this.HouseNum);

            if (this.ShowHouseNumError)
                this.HouseNumError = ERROR_MESSAGES.REQUIRED_FIELD;
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
            if (String.IsNullOrEmpty(this.SelectedStreetItem) || this.Street != this.SelectedStreetItem)
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
                foreach (StreetDTO street in this.allStreets)
                {
                    string contactString = street.street_name;
                    if (street.city_name == SelectedCityItem)
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
                List<StreetDTO> streets = this.allStreets.Where(s => s.city_name == city).ToList();
                this.filteredStreets.Clear();
                foreach (StreetDTO s in streets)
                    this.FilteredStreets.Add(s);
            }
        }

        //public event Action ClearSelection;
        #endregion

        #region SelectedStreet
        public ICommand SelectedStreet => new Command<StreetDTO>(OnSelectedStreet);
        public void OnSelectedStreet(StreetDTO street)
        {
            if (street != null)
            {
                this.ShowStreets = false;
                this.street = street.street_name;
                OnPropertyChanged("Street");
                ValidateStreet();
            }
        }
        #endregion
        #region ValidateForm
        private bool ValidateForm()
        {
            //Validate all fields first
            ValidateEmail();
            ValidateUserName();
            ValidatePassword();
            ValidateFName();
            ValidateLName();
            ValidateBirthDate();
            ValidatePhoneNum();
            ValidateCity();
            ValidateStreet();
            ValidateHouseNum();
            if (!IsBabySitter) ValidateAge();
            ValidateChildrenCount();

            //check if any validation failed
            if (ShowEmailError || ShowUserNameError || ShowPasswordError
                || ShowNameError || ShowBirthDateError || ShowPhoneNumError
                || ShowCityError || ShowStreetError || ShowStringHouseNumError||ShowChildrenCountError||ShowAgeError)
                return false;
            return true;
        }
        #endregion
        #region parent sign up
        public async Task<Parent> ParentSignUp()
        {
            BabySiteAPIProxy proxy = BabySiteAPIProxy.CreateProxy();
            User user = await CreateUser();
            user.UserTypeId = 1;
            Parent parent = new Parent()
            {
                ChildrenCount = this.ChildrenCount,
                ChildrenMaxAge = this.MaxAge,
                ChildrenMinAge = this.MinAge,
                HasDog=this.HasDog,
                User = user
            };
            if (!await IsExist(user))
            {
                Parent newParent = await proxy.ParentSignUpAsync(parent);
                if (newParent == null)
                {
                    await App.Current.MainPage.Navigation.PopModalAsync();
                    await App.Current.MainPage.DisplayAlert("שגיאה", "ההרשמה נכשלה", "אישור", FlowDirection.RightToLeft);
                }
                else
                {
                    if (this.imageFileResult != null)
                    {
                        ServerStatus = "מעלה תמונה...";

                        bool success = await proxy.UploadImage(new FileInfo()
                        {
                            Name = this.imageFileResult.FullPath
                        }, $"{newParent.UserId}.jpg");

                        
                    }
                    ServerStatus = "שומר נתונים...";

                    App theApp = (App)App.Current;
                    theApp.CurrentUser = parent.User;
                    theApp.CurrentParent = parent;
                    theApp.CurrentBabySitter = null;

                    Page page = new ParentMainPage();
                    page.Title = "שלום " + theApp.CurrentUser.UserName;
                    theApp.MainPage = new NavigationPage(page) { BarBackgroundColor = Color.FromHex("#81cfe0") };

                    await App.Current.MainPage.DisplayAlert("הרשמה", "ההרשמה בוצעה בהצלחה", "אישור", FlowDirection.RightToLeft);
                }

            }
            else
            {
                if (await IsExist(user))
                    await App.Current.MainPage.DisplayAlert("שגיאה", "האימייל ושם המשתמש שהקלדת כבר קיימים במערכת, בבקשה תבחר אימייל ושם משתמש חדשים ונסה שוב", "אישור", FlowDirection.RightToLeft);

                else if (await proxy.EmailExistAsync(user.Email))
                    await App.Current.MainPage.DisplayAlert("שגיאה", "האימייל שהקלדת כבר קיים במערכת, בבקשה תבחר אימייל חדש ונסה שוב", "אישור", FlowDirection.RightToLeft);

                else
                    await App.Current.MainPage.DisplayAlert("שגיאה", "שם המשתמש שהקלדת כבר קיים במערכת, בבקשה תבחר שם משתמש חדש ונסה שוב", "אישור", FlowDirection.RightToLeft);

                await App.Current.MainPage.Navigation.PopModalAsync();
            }


           



            return parent;

        }
        #endregion
        #region babysitter sign up
        public async Task<BabySitter> BabySitterSignUp()
        {
            BabySiteAPIProxy proxy = BabySiteAPIProxy.CreateProxy();

            User user = await CreateUser();
            user.UserTypeId = 2;
            BabySitter babySitter = new BabySitter()
            {
                RatingAverage = 0,
                HasCar = this.HasCar,
                Salary = this.Salary,
                User = user
            };
            if (!await IsExist(user))
            {
                BabySitter newBabySitter = await proxy.BabysitterSignUpAsync(babySitter);
                if (newBabySitter == null)
                {
                    await App.Current.MainPage.Navigation.PopModalAsync();
                    await App.Current.MainPage.DisplayAlert("שגיאה", "ההרשמה נכשלה", "אישור", FlowDirection.RightToLeft);
                }
                else
                {
                    if (this.imageFileResult != null)
                    {
                        ServerStatus = "מעלה תמונה...";

                        bool success = await proxy.UploadImage(new FileInfo()
                        {
                            Name = this.imageFileResult.FullPath
                        }, $"{newBabySitter.UserId}.jpg");


                    }
                    ServerStatus = "שומר נתונים...";

                    App theApp = (App)App.Current;
                    theApp.CurrentUser = babySitter.User;
                    theApp.CurrentBabySitter=babySitter;
                    theApp.CurrentParent = null;


                    Page page = new BabySitterMainPage();
                    page.Title = "שלום " + theApp.CurrentUser.UserName;
                    theApp.MainPage = new NavigationPage(page) { BarBackgroundColor = Color.FromHex("#81cfe0") };

                    await App.Current.MainPage.DisplayAlert("הרשמה", "ההרשמה בוצעה בהצלחה", "אישור", FlowDirection.RightToLeft);
                }

            }
            else
            {
                if (await IsExist(user))
                    await App.Current.MainPage.DisplayAlert("שגיאה", "האימייל ושם המשתמש שהקלדת כבר קיימים במערכת, בבקשה תבחר אימייל ושם משתמש חדשים ונסה שוב", "אישור", FlowDirection.RightToLeft);

                else if (await proxy.EmailExistAsync(user.Email))
                    await App.Current.MainPage.DisplayAlert("שגיאה", "האימייל שהקלדת כבר קיים במערכת, בבקשה תבחר אימייל חדש ונסה שוב", "אישור", FlowDirection.RightToLeft);

                else
                    await App.Current.MainPage.DisplayAlert("שגיאה", "שם המשתמש שהקלדת כבר קיים במערכת, בבקשה תבחר שם משתמש חדש ונסה שוב", "אישור", FlowDirection.RightToLeft);

                await App.Current.MainPage.Navigation.PopModalAsync();
            }
        
            return babySitter;
        
        }
        #endregion
        #region createUser
        private async Task<User> CreateUser()
        {
            //first extract the position of the address
            Geocoder geoCoder = new Geocoder();

            IEnumerable<Position> approximateLocations = await geoCoder.GetPositionsForAddressAsync($"{this.Street}, {this.HouseNum}, {this.City}");
            Position position = approximateLocations.FirstOrDefault();
            
            
            User user = new User()
            {

                FirstName = this.FirstName,
                LastName = this.LastName,
                PhoneNumber = this.phoneNum,
                Email = this.Email,
                UserName = this.userName,
                UserPswd = this.Password,
                Gender = this.Gender,
                BirthDate = this.BirthDate,
                City = this.City,
                Street = this.Street,
                House = this.HouseNum,
                Longitude = position.Longitude,
                Latitude = position.Latitude
        };
            return user;
         
        }
        #endregion

        #region is exist
        public async Task<bool> IsExist(User user)
        {
            BabySiteAPIProxy proxy = BabySiteAPIProxy.CreateProxy();

            return (await proxy.EmailExistAsync(user.Email) || await proxy.UserNameExistAsync(user.UserName));
        }

        #endregion
        #region SaveData
        public ICommand SaveDataCommand => new Command(SaveData);
        public async void SaveData()
        {
            if (ValidateForm())
            {
                ServerStatus = "מתחבר לשרת...";
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));
                BabySiteAPIProxy proxy = BabySiteAPIProxy.CreateProxy();
                if (IsBabySitter)
                    await BabySitterSignUp();
                else
                    await ParentSignUp();
            }

            else
                await App.Current.MainPage.DisplayAlert("שמירת נתונים", " יש בעיה עם הנתונים בדוק ונסה שוב", "אישור", FlowDirection.RightToLeft);
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
            ShowAgeError = false;
            ShowChildrenCountError = false;
            AgeError = ERROR_MESSAGES.BAD_AGE;
            ChildrenCountError = ERROR_MESSAGES.BAD_COUNT;
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
            allCities = ((App)Application.Current).Cities;
            allStreets = ((App)Application.Current).Streets;
            this.FilteredCities = new ObservableCollection<string>();
            this.FilteredStreets = new ObservableCollection<StreetDTO>();

            GenderArray = new ObservableCollection<string>();
            GenderArray.Add("Male");
            GenderArray.Add("Female");
            GenderArray.Add("Other");
            //Setup default image photo
            this.UserImgSrc = DEFAULT_PHOTO_SRC;
            this.imageFileResult = null; //mark that no picture was chosen
        }
            #endregion
        }


    }



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
using Xamarin.Forms.Maps;

namespace BabySiteApp.ViewModels
{
    class BabySitterPageViewModel:BaseViewModels
    {

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


        #region IsStreetEnabled
        private bool isStreetEnabled;
        public bool IsStreetEnabled
        {
            get => isStreetEnabled;
            set
            {
                isStreetEnabled = value;
                OnPropertyChanged("IsStreetEnabled");
            }
        }
        #endregion

        #region FirstName
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

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                ValidateName();
                OnPropertyChanged("Name");
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

        private void ValidateName()
        {
            this.ShowNameError = string.IsNullOrEmpty(Name);
        }
        #endregion
        #region LastName
        private bool showLastNameError;
        public bool ShowLastNameError
        {
            get => showLastNameError;
            set
            {
                showLastNameError = value;
                OnPropertyChanged("ShowLastNameError");
            }
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                ValidateLastName();
                OnPropertyChanged("LastName");
            }
        }

        private string lastNameError;
        public string LastNameError
        {
            get => lastNameError;
            set
            {
                lastNameError = value;
                OnPropertyChanged("LastNameError");
            }
        }

        private void ValidateLastName()
        {
            this.ShowLastNameError = string.IsNullOrEmpty(LastName);
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
            else
                this.PasswordError = ERROR_MESSAGES.REQUIRED_FIELD;
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
            else
                this.EmailError = ERROR_MESSAGES.REQUIRED_FIELD;
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
            else
                this.PhoneNumError = ERROR_MESSAGES.REQUIRED_FIELD;
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
            this.ShowCityError = string.IsNullOrEmpty(this.City);
            if (!this.ShowCityError)
            {
                string city = this.allCities.Where(c => c == this.City).FirstOrDefault();
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
                StreetDTO street = this.allStreets.Where(s => s.street_name == this.Street).FirstOrDefault();
                if (street == null)
                {
                    this.ShowStreetError = true;
                    this.StreetError = ERROR_MESSAGES.BAD_STREET;
                }
            }
            else
                this.StreetError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion
        #region adress
        private double? longitude;
        public double? Longitude
        {
            get => longitude;
            set
            {
                longitude = value;
                OnPropertyChanged("Longitude");
            }
        }

        private double? latitude;
        public double? Latitude
        {
            get => latitude;
            set
            {
                latitude = value;
                OnPropertyChanged("Latitude");
            }
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
            this.ShowHouseNumError = this.HouseNum == "0";
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
        #region Refresh
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                if (this.isRefreshing != value)
                {
                    this.isRefreshing = value;
                    OnPropertyChanged(nameof(IsRefreshing));
                }
            }
        }
        #endregion
       
        public Command SaveDataCommand { protected set; get; }
        public ICommand ClearCommand { protected set; get; }
        public ICommand HomePageCommand { protected set; get; }


        #region Constructor
        public BabySitterPageViewModel()
        {
            App theApp = (App)App.Current;

            this.allCities = theApp.Cities;
            this.FilteredCities = new ObservableCollection<string>();

            this.allStreets = theApp.Streets;
            this.FilteredStreets = new ObservableCollection<string>();

            User currentUser = theApp.CurrentUser;
            BabySitter currentBabySitter = theApp.CurrentBabySitter;

            
            this.SelectedCityItem = currentUser.City;
            this.Longitude = currentUser.Longitude;
            this.Latitude = currentUser.Latitude;
            this.Email = currentUser.Email;
            this.UserName = currentUser.UserName;
            this.Password = currentUser.UserPswd;
            this.Name = currentUser.FirstName;
            this.LastName = currentUser.LastName;
            this.BirthDate = currentUser.BirthDate;
            this.PhoneNum = currentUser.PhoneNumber;
            this.City = this.SelectedCityItem;
            this.HouseNum = currentUser.House;
            this.HasCar = currentBabySitter.HasCar;
            this.salay = currentBabySitter.Salary;
            this.UserImgSrc = currentUser.PhotoURL;

            this.SelectedStreetItem = currentUser.Street;
            this.Street = this.SelectedStreetItem;
            this.IsStreetEnabled = true;

            //set the path url to the contact photo
            BabySiteAPIProxy proxy = BabySiteAPIProxy.CreateProxy();
            //Create a source with cache busting!



            this.EmailError = ERROR_MESSAGES.BAD_EMAIL;
            this.UserNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.PasswordError = ERROR_MESSAGES.SHORT_PASS;
            this.NameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.LastNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.BirthDateError = ERROR_MESSAGES.BAD_DATE;
            this.PhoneNumError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.CityError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.StreetError = ERROR_MESSAGES.REQUIRED_FIELD;
            //ShowAgeError = false;
            //ShowChildrenCountError = false;
            //AgeError = ERROR_MESSAGES.BAD_AGE;
            //ChildrenCountError = ERROR_MESSAGES.BAD_COUNT;

            this.ShowEmailError = false;
            this.ShowUserNameError = false;
            this.ShowPasswordError = false;
            this.ShowNameError = false;
            this.ShowLastNameError = false;
            this.ShowBirthDateError = false;
            this.ShowPhoneNumError = false;
            this.ShowCityError = false;
            this.ShowStreetError = false;


            this.SaveDataCommand = new Command(() => SaveData());
            ClearCommand = new Command(OnClear);
            HomePageCommand = new Command(OnHome);

            LogOutCommand = new Command(OnLogOut);

        }


        #endregion
        #region ValidateForm
        private bool ValidateForm()
        {
            //Validate all fields first
            ValidatePassword();
            ValidateName();
            ValidateLastName();
            ValidateBirthDate();
            ValidatePhoneNum();
            ValidateCity();
            ValidateStreet();
            //ValidateAge();
            //ValidateChildrenCount();


            //check if any validation failed
            if (ShowPasswordError || ShowNameError || ShowLastNameError
                || ShowBirthDateError || ShowPhoneNumError || ShowCityError
                || ShowStreetError)
                return false;
            return true;
        }
        #endregion

        #region SaveData
        private async void SaveData()
        {
            if (ValidateForm())
            {
                ServerStatus = "מתחבר לשרת...";
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));

                App theApp = (App)App.Current;
                //first extract the position of the address
                Geocoder geoCoder = new Geocoder();

                IEnumerable<Position> approximateLocations = await geoCoder.GetPositionsForAddressAsync($"{this.Street}, {this.HouseNum}, {this.City}");
                Position position = approximateLocations.FirstOrDefault();
                BabySitter newBabySitter = new BabySitter()
                {

                    User = new User()
                    {
                        UserId = theApp.CurrentUser.UserId,
                        Email = this.Email,
                        UserName = this.UserName,
                        UserPswd = this.Password,
                        FirstName = this.Name,
                        LastName = this.LastName,
                        BirthDate = this.BirthDate,
                        PhoneNumber = this.PhoneNum,
                        City = this.City,
                        Street = this.Street,
                        House = this.HouseNum,
                        Gender = theApp.CurrentUser.Gender,
                          Longitude = position.Longitude,
                        Latitude = position.Latitude

                    },
                   
                    BabySitterId = theApp.CurrentBabySitter.BabySitterId,
                   
                    UserId = theApp.CurrentUser.UserId



                };

                BabySiteAPIProxy proxy = BabySiteAPIProxy.CreateProxy();
                BabySitter babySitter = await proxy.UpdateBabySitter(newBabySitter);

                if (babySitter == null)
                {
                    await App.Current.MainPage.Navigation.PopModalAsync();
                    await App.Current.MainPage.DisplayAlert("שגיאה", "העדכון נכשל", "אישור", FlowDirection.RightToLeft);
                }
                else
                {

                    ServerStatus = "שומר נתונים...";

                    theApp.CurrentUser = babySitter.User;
                    await App.Current.MainPage.Navigation.PopModalAsync();

                    Page page;


                    page = new Views.BabySitterMainPage();



                    page.Title = "שלום " + theApp.CurrentUser.UserName;
                    App.Current.MainPage = new NavigationPage(page) { BarBackgroundColor = Color.FromHex("#81cfe0") };
                    await App.Current.MainPage.DisplayAlert("עדכון", "העדכון בוצע בהצלחה", "אישור", FlowDirection.RightToLeft);
                }
            }
            else
                await App.Current.MainPage.DisplayAlert("שמירת נתונים", " יש בעיה עם הנתונים בדוק ונסה שוב", "אישור", FlowDirection.RightToLeft);
        }
        #endregion

        #region UpdateCommand
        public ICommand UpdateCommand => new Command(OnUpdate);
        public ICommand LogOutCommand { protected set; get; }
        #region OnLogOut
        public async void OnLogOut()
        {
            bool answer = await App.Current.MainPage.DisplayAlert("התנתקות", "האם ברצונך להתנתק?", "התנתק", "ביטול", FlowDirection.RightToLeft);
            if (answer)
            {
                App theApp = (App)App.Current;
                theApp.CurrentUser = null;

                Page page = new Login();
                page.Title = "התחברות";
                App.Current.MainPage = new NavigationPage(page) { BarBackgroundColor = Color.FromHex("#81cfe0") };
            }
        }
        #endregion
        public async void OnUpdate()
        {
            Page page = new UpdateUser();
            page.Title = "עדכון פרטים";
            await App.Current.MainPage.Navigation.PushAsync(page);
        }
        #endregion
        #region OnCityChanged
        public void OnCityChanged(string search)
        {
            //if (this.Street != this.selectedStreetItem)
            //{
            //    this.Street = "";
            //    this.ShowStreets = false;
            //    this.FilteredStreets.Clear();
            //    this.IsStreetEnabled = false;
            //}            

            if (this.City != this.SelectedCityItem)
            {
                this.ShowCities = true;
                this.SelectedCityItem = null;

                this.Street = "";
                this.ShowStreets = false;
                this.FilteredStreets.Clear();
                this.IsStreetEnabled = false;
            }
            //Filter the list of cities based on the search term
            if (this.allCities == null)
                return;
            if (String.IsNullOrWhiteSpace(search) || String.IsNullOrEmpty(search))
            {
                this.ShowCities = false;
                this.FilteredCities.Clear();
            }
            else
            {
                foreach (string city in this.allCities)
                {
                    if (!this.FilteredCities.Contains(city) &&
                        city.Contains(search))
                        this.FilteredCities.Add(city);
                    else if (this.FilteredCities.Contains(city) &&
                        !city.Contains(search))
                        this.FilteredCities.Remove(city);
                }
            }
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
            //Filter the list of streets based on the search term
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
                    string streetName = street.street_name;

                    if (!this.FilteredStreets.Contains(streetName) &&
                        streetName.Contains(search) && street.city_name == this.City)
                        this.FilteredStreets.Add(streetName);
                    else if (this.FilteredStreets.Contains(streetName) &&
                        (!streetName.Contains(search) || !(street.city_name == this.City)))
                        this.FilteredStreets.Remove(streetName);
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

                this.IsStreetEnabled = true;
            }
        }
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

        #region RefreshCommand
        public ICommand RefreshCommand => new Command(OnRefresh);

        public void OnRefresh()
        {
            IsRefreshing = true;

            App theApp = (App)App.Current;
            User currentUser = theApp.CurrentUser;

            this.SelectedCityItem = currentUser.City;
            this.SelectedStreetItem = currentUser.Street;

            //this.UserImgSrc = currentUser.PhotoURL;
            this.Password = currentUser.UserPswd;
            this.Name = currentUser.FirstName;
            this.LastName = currentUser.LastName;
            this.BirthDate = currentUser.BirthDate;
            this.PhoneNum = currentUser.PhoneNumber;
            this.City = this.SelectedCityItem;
            this.Street = this.SelectedStreetItem;
            this.HouseNum = currentUser.House;


            IsRefreshing = false;
        }
        #endregion

        #region OnClear
        public void OnClear()
        {
            App theApp = (App)App.Current;
            User currentUser = theApp.CurrentUser;

            this.SelectedCityItem = currentUser.City;
            this.SelectedStreetItem = currentUser.Street;


            this.Password = currentUser.UserPswd;
            this.Name = currentUser.FirstName;
            this.LastName = currentUser.LastName;
            this.BirthDate = currentUser.BirthDate;
            this.PhoneNum = currentUser.PhoneNumber;
            this.City = this.SelectedCityItem;
            this.Street = this.SelectedStreetItem;
            this.HouseNum = currentUser.House;

        }
        #endregion
        #region OnHome
        public async void OnHome()
        {
            App theApp = (App)App.Current;
            Page page = new BabySitterMainPage();
            page.Title = $"שלום {theApp.CurrentUser.UserName}";

            await App.Current.MainPage.Navigation.PopToRootAsync();
            //while (App.Current. != App.Current.MainPage)
            //await App.Current.MainPage.Navigation.PushAsync(page);
        }
        #endregion
    }
}

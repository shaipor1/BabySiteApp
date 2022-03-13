using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using BabySiteApp.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.IO;
using BabySiteApp.DTO;



namespace BabySiteApp.Services
{
    class BabySiteAPIProxy
    {




        private const string CLOUD_URL = "TBD"; //API url when going on the cloud
        private const string CLOUD_PHOTOS_URL = "TBD";
        private const string DEV_ANDROID_EMULATOR_URL = "http://10.0.2.2:54152/BabySiteAPI"; //API url when using emulator on android
        private const string DEV_ANDROID_PHYSICAL_URL = "http://10.100.102.104:54152/BabySiteAPI"; //API url when using physucal device on android
        private const string DEV_WINDOWS_URL = "https://localhost:44331/BabySiteAPI"; //API url when using windoes on development
        private const string DEV_ANDROID_EMULATOR_PHOTOS_URL = "http://10.0.2.2:54152/Images/"; //API url when using emulator on android
        private const string DEV_ANDROID_PHYSICAL_PHOTOS_URL = "http://10.100.102.104:54152/Images/"; //API url when using physucal device on android
        private const string DEV_WINDOWS_PHOTOS_URL = "https://localhost:44331/Images/"; //API url when using windoes on development
        private const string CLOUD_DATA_URL = "TBD";
        private const string DEV_WINDOWS_DATA_URL = "https://localhost:54152/data/"; //API url when using windoes on development
        private const string DEV_ANDROID_EMULATOR_DATA_URL = "http://10.0.2.2:54152/data/"; //API url when using emulator on android
        private const string DEV_ANDROID_PHYSICAL_DATA_URL = "http://10.100.102.104:54152/data/"; //API url when using physucal device on android

        private HttpClient client;
        private string baseUri;
        private string basePhotosUri;
        private static BabySiteAPIProxy proxy = null;
        private string baseDataUri;

        public static BabySiteAPIProxy CreateProxy()
        {
            string baseUri;
            string basePhotosUri;
            string baseDataUri;
            if (App.IsDevEnv)
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                    if (DeviceInfo.DeviceType == DeviceType.Virtual)
                    {
                        baseUri = DEV_ANDROID_EMULATOR_URL;
                        basePhotosUri = DEV_ANDROID_EMULATOR_PHOTOS_URL;
                        baseDataUri = DEV_ANDROID_EMULATOR_DATA_URL;
                    }
                    else
                    {
                        baseUri = DEV_ANDROID_PHYSICAL_URL;
                        basePhotosUri = DEV_ANDROID_PHYSICAL_PHOTOS_URL;
                        baseDataUri = DEV_ANDROID_PHYSICAL_DATA_URL;
                    }
                }
                else
                {
                    baseUri = DEV_WINDOWS_URL;
                    basePhotosUri = DEV_WINDOWS_PHOTOS_URL;
                    baseDataUri = DEV_WINDOWS_DATA_URL;
                }
            }
            else
            {
                baseUri = CLOUD_URL;
                basePhotosUri = CLOUD_PHOTOS_URL;
                baseDataUri = CLOUD_DATA_URL;
            }

            if (proxy == null)
                proxy = new BabySiteAPIProxy(baseUri, basePhotosUri, baseDataUri);
            return proxy;
        }


        private BabySiteAPIProxy(string baseUri, string basePhotosUri, string baseDataUri)
        {
            //Set client handler to support cookies!!
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            //Create client with the handler!
            this.client = new HttpClient(handler, true);
            this.baseUri = baseUri;
            this.basePhotosUri = basePhotosUri;
            this.baseDataUri = baseDataUri;
        }

        public string GetBasePhotoUri() { return this.basePhotosUri; }

        //Login!
        public async Task<User> LoginAsync(string email, string pass)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/Login?email={email}&pass={pass}");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve, //avoid reference loops!
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    User u = JsonSerializer.Deserialize<User>(content, options);
                    return u;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<BabySitter> BabysitterSignUpAsync(BabySitter babySitter)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
                    PropertyNameCaseInsensitive = true
                };
                string jsonObject = JsonSerializer.Serialize<BabySitter>(babySitter, options);
                StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/SignUpBabySitter", content);
                if (response.IsSuccessStatusCode)
                {
                    jsonObject = await response.Content.ReadAsStringAsync();
                    BabySitter a = JsonSerializer.Deserialize<BabySitter>(jsonObject, options);
                    return a;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<Parent> ParentSignUpAsync(Parent parent)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
                    PropertyNameCaseInsensitive = true
                };
                string jsonObject = JsonSerializer.Serialize<Parent>(parent, options);
                StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/SignUpBabySitter", content);
                if (response.IsSuccessStatusCode)
                {
                    jsonObject = await response.Content.ReadAsStringAsync();
                    Parent a = JsonSerializer.Deserialize<Parent>(jsonObject, options);
                    return a;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        //************** Streets and Cities JSOn file **********************************
        #region GetCitiesNameList
        private List<string> GetCitiesNameList(List<CityDTO> cities)
        {
            List<string> citiesName = new List<string>();

            foreach (CityDTO city in cities)
            {
                citiesName.Add(city.name);
            }
            citiesName.Remove(citiesName[0]);

            return citiesName;
        }
        #endregion

        #region GetCitiesAsync
        public async Task<List<string>> GetCitiesAsync()
        {
            ///royts/israel-cities/master/israel-cities.json
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseDataUri}/cities.json");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve, //avoid reference loops!
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    List<CityDTO> cities = JsonSerializer.Deserialize<List<CityDTO>>(content, options);

                    return GetCitiesNameList(cities);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region GetStreetsNameList
        private List<string> GetStreetsNameList(List<StreetDTO> streets/*, string city*/)
        {
            List<string> streetsName = new List<string>();

            foreach (StreetDTO street in streets)
            {
                streetsName.Add(street.street_name);
            }

            return streetsName;
        }
        #endregion

        #region GetStreetsAsync
        public async Task<List<string>> GetStreetsAsync(/*string city*/)
        {
            //?resource_id=d4901968-dad3-4845-a9b0-a57d027f11ab&limit=1500
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseDataUri}/streets.json?666");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();

                    List<StreetDTO> streets = JsonSerializer.Deserialize<List<StreetDTO>>(content, options);
                    return GetStreetsNameList(streets/*, city*/);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion





    }
}
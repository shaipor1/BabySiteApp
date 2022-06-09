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

namespace BabySiteApp.ViewModels
{
    public class AddJobOfferViewModel : BaseViewModels
    {
        #region properties
        private bool showHeadLineError;
        public bool ShowHeadLineError
        {
            get => showHeadLineError;
            set
            {
                showHeadLineError = value;
                OnPropertyChanged("ShowHeadLineError");
            }
        }
        private string headLineError;

        public string HeadLineError
        {
            get => headLineError;
            set
            {
                headLineError = value;
                OnPropertyChanged("HeadLineError");
            }
        }
        private void ValidateHeadLine()
        {
            this.ShowHeadLineError = string.IsNullOrEmpty(MessageHeadLine);
        }

        private string messageHeadLine;
        public string MessageHeadLine
        {
            get => messageHeadLine;
            set
            {
                if(messageHeadLine!=value)
                {
                    messageHeadLine = value;
                    ValidateHeadLine();
                    OnPropertyChanged("MessageHeadLine");
                }
            }
        }
        //
        private bool showBodyError;
        public bool ShowBodyError
        {
            get => showBodyError;
            set
            {
                showBodyError = value;
                OnPropertyChanged("ShowBodyError");
            }
        }
        private string bodyError;

        public string BodyError
        {
            get => bodyError;
            set
            {
                bodyError = value;
                OnPropertyChanged("BodyError");
            }
        }
        private void ValidateBody()
        {
            this.ShowBodyError = string.IsNullOrEmpty(MessageBody);
        }
        private string messageBody;
        public string MessageBody
        {
            get => messageBody;
            set
            {
                if (messageBody != value)
                {
                    messageBody = value;
                    ValidateBody();
                    OnPropertyChanged("MessageBody");
                }
            }
        }
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
        #endregion

        #region commands
        public ICommand PostMessageCommand=> new Command(PostMessage);

        #endregion
        public AddJobOfferViewModel()
        {
            
            MessageBody = string.Empty;
            MessageHeadLine = string.Empty;
            ServerStatus = string.Empty;
        }
        private async void PostMessage()
        {
            App a = (App)App.Current;
            if(!ShowBodyError&&!ShowHeadLineError)
            { 
            Massage newMassage = new Massage()
            {
                MassageTypeId = 1,
                HeadLine = this.MessageHeadLine,
                Body = this.MessageBody,
                UserId = a.CurrentUser.UserId
              

            };
            BabySiteAPIProxy proxy = BabySiteAPIProxy.CreateProxy();
            bool b= await proxy.PostMessageAsync(newMassage);
            ServerStatus = "מעלה את ההצעה..";
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));
            if (b)
            {
                Page page = new ViewJobOffers();
                await App.Current.MainPage.DisplayAlert("בוצע", "ההצעה פורסמה בהצלחה", "אישור", FlowDirection.RightToLeft);
                await App.Current.MainPage.Navigation.PopAsync();
                await App.Current.MainPage.Navigation.PushAsync(new ViewJobOffers() { BindingContext = new ViewJobOffersViewModel() });
                await App.Current.MainPage.Navigation.PopModalAsync();


            }
            else
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "היתה תקלה בפרסום ההצעה, נסה שוב", "אישור", FlowDirection.RightToLeft);
            }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "היתה תקלה בפרסום ההצעה, נסה שוב", "אישור", FlowDirection.RightToLeft);
            }

        }
    }
}
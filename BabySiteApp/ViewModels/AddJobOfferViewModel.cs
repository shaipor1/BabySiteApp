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
        private string messageHeadLine;
        public string MessageHeadLine
        {
            get => messageHeadLine;
            set
            {
                if(messageHeadLine!=value)
                {
                    messageHeadLine = value;
                    OnPropertyChanged("MessageHeadLine");
                }
            }
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
        public ICommand PostMessageCommand;

        #endregion
        public AddJobOfferViewModel()
        {
           PostMessageCommand = new Command(PostMessage);
            MessageBody = string.Empty;
            MessageHeadLine = string.Empty;
            ServerStatus = string.Empty;
        }
        private async void PostMessage()
        {
            App a = (App)App.Current;

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
            if(b)
            {
                Page page = new ViewJobOffers();

                await App.Current.MainPage.DisplayAlert("בוצע", "ההצעה פורסמה בהצלחה", "אישור", FlowDirection.RightToLeft);
                 await a.MainPage.Navigation.PushAsync(new ViewJobOffers() { BindingContext = new ViewJobOffersViewModel() });

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "היתה תקלה בפרסום ההצעה, נסה שוב", "אישור", FlowDirection.RightToLeft);
            }

        }
    }
}
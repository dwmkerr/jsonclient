using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apex.MVVM;
using JsonClient;

namespace JsonWPFClient
{
    public class RequestViewModel : ViewModel
    {
        public RequestViewModel()
        {
            DoRequestCommand = new Command(DoDoRequestCommand);
        }
        
        /// <summary>
        /// The NotifyingProperty for the Uri property.
        /// </summary>
        private readonly NotifyingProperty UriProperty =
          new NotifyingProperty("Uri", typeof(string), default(string));

        /// <summary>
        /// Gets or sets Uri.
        /// </summary>
        /// <value>The value of Uri.</value>
        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        
        /// <summary>
        /// The NotifyingProperty for the Verb property.
        /// </summary>
        private readonly NotifyingProperty VerbProperty =
          new NotifyingProperty("Verb", typeof(string), default(string));

        /// <summary>
        /// Gets or sets Verb.
        /// </summary>
        /// <value>The value of Verb.</value>
        public string Verb
        {
            get { return (string)GetValue(VerbProperty); }
            set { SetValue(VerbProperty, value); }
        }

        
        /// <summary>
        /// The NotifyingProperty for the Status property.
        /// </summary>
        private readonly NotifyingProperty StatusCodeProperty =
          new NotifyingProperty("StatusCode", typeof(int), default(int));

        /// <summary>
        /// Gets or sets Status.
        /// </summary>
        /// <value>The value of Status.</value>
        public int StatusCode
        {
            get { return (int)GetValue(StatusCodeProperty); }
            set { SetValue(StatusCodeProperty, value); }
        }

        
        /// <summary>
        /// The NotifyingProperty for the StatusCodeDescription property.
        /// </summary>
        private readonly NotifyingProperty StatusCodeDescriptionProperty =
          new NotifyingProperty("StatusCodeDescription", typeof(string), default(string));

        /// <summary>
        /// Gets or sets StatusCodeDescription.
        /// </summary>
        /// <value>The value of StatusCodeDescription.</value>
        public string StatusCodeDescription
        {
            get { return (string)GetValue(StatusCodeDescriptionProperty); }
            set { SetValue(StatusCodeDescriptionProperty, value); }
        }
        
        /// <summary>
        /// The NotifyingProperty for the Response property.
        /// </summary>
        private readonly NotifyingProperty ResponseProperty =
          new NotifyingProperty("Response", typeof(string), default(string));

        /// <summary>
        /// Gets or sets Response.
        /// </summary>
        /// <value>The value of Response.</value>
        public JsonResult Response
        {
            get { return (JsonResult)GetValue(ResponseProperty); }
            set { SetValue(ResponseProperty, value); }
        }

        
        /// <summary>
        /// The NotifyingProperty for the ResponseCode property.
        /// </summary>
        private readonly NotifyingProperty ResponseCodeProperty =
          new NotifyingProperty("ResponseCode", typeof(int), default(int));

        
        /// <summary>
        /// The NotifyingProperty for the IsBusy property.
        /// </summary>
        private readonly NotifyingProperty IsBusyProperty =
          new NotifyingProperty("IsBusy", typeof(bool), default(bool));

        /// <summary>
        /// Gets or sets IsBusy.
        /// </summary>
        /// <value>The value of IsBusy.</value>
        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }


        /// <summary>
        /// Performs the DoRequest command.
        /// </summary>
        /// <param name="parameter">The DoRequest command parameter.</param>
        private async void DoDoRequestCommand(object parameter)
        {
            IsBusy = true;

            switch (Verb)
            {
                case "GET":
                    {
                        Response = await JsonClient.JsonClient.GetAsync(Uri);
                        break;
                    }
                case "POST":
                    {
                        Response = await JsonClient.JsonClient.GetAsync(Uri);
                        break;
                    }
                case "PUT":
                    {
                        Response = await JsonClient.JsonClient.GetAsync(Uri);
                        break;
                    }
                case "DELETE":
                    {
                        Response = await JsonClient.JsonClient.GetAsync(Uri);
                        break;
                    }
            }

            StatusCode = (int)Response.Response.StatusCode;
            StatusCodeDescription = Response.Response.StatusDescription;

            IsBusy = false;
        }

        /// <summary>
        /// Gets the DoRequest command.
        /// </summary>
        /// <value>The value of .</value>
        public Command DoRequestCommand
        {
            get;
            private set;
        }
    }
}

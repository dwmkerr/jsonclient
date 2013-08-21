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
        /// The NotifyingProperty for the Result property.
        /// </summary>
        private readonly NotifyingProperty ResultProperty =
          new NotifyingProperty("Result", typeof(string), default(string));

        /// <summary>
        /// Gets or sets Dynamic.
        /// </summary>
        /// <value>The value of Dynamic.</value>
        public JsonResult Result
        {
            get { return (JsonResult)GetValue(ResultProperty); }
            set { SetValue(ResultProperty, value); }
        }

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

            var client = new JsonWebClient();

            //  Get the headers as a dictionary.
            var headers = new Dictionary<string, string>();
            if(Headers != null)
            {
                var lines = Headers.Split('\n');
                foreach(var line in lines)
                {
                    int colon = line.IndexOf(':');
                    if(colon == -1) continue;
                    client.Headers[line.Substring(0, colon)] = line.Substring(colon + 1);
                }
            }
            

            switch (Verb)
            {
                case "GET":
                    {
                        Result = await client.GetAsync(Uri);
                        break;
                    }
                case "POST":
                    {
                        Result = await client.PostAsync(Uri, JsonContent);
                        break;
                    }
                case "PUT":
                    {
                        Result = await client.PutAsync(Uri, JsonContent);
                        break;
                    }
                case "DELETE":
                    {
                        Result = await client.DeleteAsync(Uri);
                        break;
                    }
            }

            if(Result.Response != null)
            {
                StatusCode = (int)Result.Response.StatusCode;
                StatusCodeDescription = Result.Response.StatusDescription;
            }
            else
            {
                StatusCodeDescription = Result.Error.GetType().ToString();
            }

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

        
        /// <summary>
        /// The NotifyingProperty for the JsonContent property.
        /// </summary>
        private readonly NotifyingProperty JsonContentProperty =
          new NotifyingProperty("JsonContent", typeof(string), default(string));

        /// <summary>
        /// Gets or sets JsonContent.
        /// </summary>
        /// <value>The value of JsonContent.</value>
        public string JsonContent
        {
            get { return (string)GetValue(JsonContentProperty); }
            set { SetValue(JsonContentProperty, value); }
        }

        
        /// <summary>
        /// The NotifyingProperty for the Headers property.
        /// </summary>
        private readonly NotifyingProperty HeadersProperty =
          new NotifyingProperty("Headers", typeof(string), default(string));

        /// <summary>
        /// Gets or sets Headers.
        /// </summary>
        /// <value>The value of Headers.</value>
        public string Headers
        {
            get { return (string)GetValue(HeadersProperty); }
            set { SetValue(HeadersProperty, value); }
        }
    }
}

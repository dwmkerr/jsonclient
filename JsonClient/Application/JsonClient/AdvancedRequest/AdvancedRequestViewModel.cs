using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apex.MVVM;

namespace JsonWPFClient.AdvancedRequest
{
    [ViewModel]
    public class AdvancedRequestViewModel : ViewModel
    {
        public AdvancedRequestViewModel()
        {
            //  Create the SendRequest Command.
            SendRequestCommand = new Command(DoSendRequestCommand);
        }
        
        /// <summary>
        /// The NotifyingProperty for the Url property.
        /// </summary>
        private readonly NotifyingProperty UrlProperty =
          new NotifyingProperty("Url", typeof(string), default(string));

        /// <summary>
        /// Gets or sets Url.
        /// </summary>
        /// <value>The value of Url.</value>
        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }
        
        /// <summary>
        /// The NotifyingProperty for the Content property.
        /// </summary>
        private readonly NotifyingProperty ContentProperty =
          new NotifyingProperty("Content", typeof(string), default(string));

        /// <summary>
        /// Gets or sets Content.
        /// </summary>
        /// <value>The value of Content.</value>
        public string Content
        {
            get { return (string)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
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

        /// <summary>
        /// Performs the SendRequest command.
        /// </summary>
        /// <param name="parameter">The SendRequest command parameter.</param>
        private void DoSendRequestCommand(object parameter)
        {
            var verb = parameter.ToString();
            var request = new RequestViewModel { Uri = Url, Verb = verb, JsonContent = Content, Headers = Headers};

            if (RequestManager != null)
                RequestManager.AddRequest(request);
            request.DoRequestCommand.DoExecute();


            //  Save the command for the last url.
            Properties.Settings.Default.LastAdvancedRequestUrl = Url;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Gets the SendRequest command.
        /// </summary>
        /// <value>The value of .</value>
        public Command SendRequestCommand
        {
            get;
            private set;
        }

        public IRequestManager RequestManager { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Apex.MVVM;

namespace JsonWPFClient
{
    [ViewModel]
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            SendRequestCommand = new Command(DoSendRequestCommand);

            if (Apex.Design.DesignTime.IsDesignTime)
                CreateDesignTimeData();
        }

        private void CreateDesignTimeData()
        {
            Requests.Add(new RequestViewModel
                             {
                                 Uri = "http://example:3000/users",
                                 Verb = "GET"
                             });

        }

        /// <summary>
        /// The NotifyingProperty for the CurrentUrl property.
        /// </summary>
        private readonly NotifyingProperty CurrentUrlProperty =
            new NotifyingProperty("CurrentUrl", typeof (string), default(string));

        /// <summary>
        /// Gets or sets CurrentUrl.
        /// </summary>
        /// <value>The value of CurrentUrl.</value>
        public string CurrentUrl
        {
            get { return (string) GetValue(CurrentUrlProperty); }
            set { SetValue(CurrentUrlProperty, value); }
        }

        /// <summary>
        /// The NotifyingProperty for the CurrentContent property.
        /// </summary>
        private readonly NotifyingProperty CurrentContentProperty =
            new NotifyingProperty("CurrentContent", typeof (string), default(string));

        /// <summary>
        /// Gets or sets CurrentContent.
        /// </summary>
        /// <value>The value of CurrentContent.</value>
        public string CurrentContent
        {
            get { return (string) GetValue(CurrentContentProperty); }
            set { SetValue(CurrentContentProperty, value); }
        }


        /// <summary>
        /// The Requests observable collection.
        /// </summary>
        private readonly ObservableCollection<RequestViewModel> RequestsProperty =
            new ObservableCollection<RequestViewModel>();

        /// <summary>
        /// Gets the Requests observable collection.
        /// </summary>
        /// <value>The Requests observable collection.</value>
        public ObservableCollection<RequestViewModel> Requests
        {
            get { return RequestsProperty; }
        }

        
        /// <summary>
        /// The NotifyingProperty for the SelectedRequest property.
        /// </summary>
        private readonly NotifyingProperty SelectedRequestProperty =
          new NotifyingProperty("SelectedRequest", typeof(RequestViewModel), default(RequestViewModel));

        /// <summary>
        /// Gets or sets SelectedRequest.
        /// </summary>
        /// <value>The value of SelectedRequest.</value>
        public RequestViewModel SelectedRequest
        {
            get { return (RequestViewModel)GetValue(SelectedRequestProperty); }
            set { SetValue(SelectedRequestProperty, value); }
        }


        /// <summary>
        /// Performs the SendRequest command.
        /// </summary>
        /// <param name="parameter">The SendRequest command parameter.</param>
        private void DoSendRequestCommand(object parameter)
        {
            var verb = parameter.ToString();
            var request = new RequestViewModel {Uri = CurrentUrl, Verb = verb, JsonContent = CurrentContent};
            Requests.Add(request);
            request.DoRequestCommand.DoExecute();
            SelectedRequest = request;

            //  Save the command for the last url.
            Properties.Settings.Default.LastUrl = CurrentUrl;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Gets the SendRequest command.
        /// </summary>
        /// <value>The value of .</value>
        public Command SendRequestCommand { get; private set; }
}
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Apex.MVVM;

namespace JsonWPFClient
{
    [ViewModel]
    class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            GetCommand = new Command(DoGetCommand);
            PostCommand = new Command(DoPostCommand);
            PutCommand = new Command(DoPutCommand);
            DeleteCommand =new Command(DoDeleteCommand);

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
          new NotifyingProperty("CurrentUrl", typeof(string), default(string));

        /// <summary>
        /// Gets or sets CurrentUrl.
        /// </summary>
        /// <value>The value of CurrentUrl.</value>
        public string CurrentUrl
        {
            get { return (string)GetValue(CurrentUrlProperty); }
            set { SetValue(CurrentUrlProperty, value); }
        }
                
        /// <summary>
        /// The NotifyingProperty for the CurrentContent property.
        /// </summary>
        private readonly NotifyingProperty CurrentContentProperty =
          new NotifyingProperty("CurrentContent", typeof(string), default(string));

        /// <summary>
        /// Gets or sets CurrentContent.
        /// </summary>
        /// <value>The value of CurrentContent.</value>
        public string CurrentContent
        {
            get { return (string)GetValue(CurrentContentProperty); }
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
        /// Performs the Get command.
        /// </summary>
        /// <param name="parameter">The Get command parameter.</param>
        private void DoGetCommand(object parameter)
        {
            var request = new RequestViewModel {Uri = CurrentUrl, Verb = "GET"};
            Requests.Add(request);
            request.DoRequestCommand.DoExecute();
        }

        /// <summary>
        /// Gets the Get command.
        /// </summary>
        /// <value>The value of .</value>
        public Command GetCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Performs the Post command.
        /// </summary>
        /// <param name="parameter">The Post command parameter.</param>
        private void DoPostCommand(object parameter)
        {
            var request = new RequestViewModel { Uri = CurrentUrl, Verb = "POST" };
            Requests.Add(request);
            request.DoRequestCommand.DoExecute();
        }

        /// <summary>
        /// Gets the Post command.
        /// </summary>
        /// <value>The value of .</value>
        public Command PostCommand
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Performs the Put command.
        /// </summary>
        /// <param name="parameter">The Put command parameter.</param>
        private void DoPutCommand(object parameter)
        {
            var request = new RequestViewModel { Uri = CurrentUrl, Verb = "PUT" };
            Requests.Add(request);
            request.DoRequestCommand.DoExecute();
        }

        /// <summary>
        /// Gets the Put command.
        /// </summary>
        /// <value>The value of .</value>
        public Command PutCommand
        {
            get;
            private set;
        }
            
        /// <summary>
        /// Performs the Delete command.
        /// </summary>
        /// <param name="parameter">The Delete command parameter.</param>
        private void DoDeleteCommand(object parameter)
        {
            var request = new RequestViewModel { Uri = CurrentUrl, Verb = "DELETE" };
            Requests.Add(request);
            request.DoRequestCommand.DoExecute();
        }

        /// <summary>
        /// Gets the Delete command.
        /// </summary>
        /// <value>The value of .</value>
        public Command DeleteCommand
        {
            get;
            private set;
        }
    }
}

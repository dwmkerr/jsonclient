using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Apex.MVVM;
using JsonClient;

namespace JsonWPFClient.AdvancedRequest
{
    /// <summary>
    /// Interaction logic for AdvancedRequestView.xaml
    /// </summary>
    [View(typeof(AdvancedRequestViewModel))]
    public partial class AdvancedRequestView : UserControl
    {
        public AdvancedRequestView()
        {
            InitializeComponent();
        }

        public AdvancedRequestViewModel ViewModel { get { return (AdvancedRequestViewModel) DataContext; } }

        private void HyperlinkAddBasicAuthHeader_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new CreateBasicAuth.BuildBasicAuthWindow();
            if (window.ShowDialog() == true)
            {
                var newHeader = HeaderHelper.BuildBasicAuthHeader(window.Username, window.Password) + System.Environment.NewLine;
                if (string.IsNullOrEmpty(ViewModel.Headers))
                    ViewModel.Headers = newHeader;
                else
                    ViewModel.Headers = ViewModel.Headers.Insert(0, newHeader);
            }
        }
    }
}

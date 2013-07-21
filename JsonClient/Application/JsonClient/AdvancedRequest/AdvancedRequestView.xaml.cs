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
    }
}

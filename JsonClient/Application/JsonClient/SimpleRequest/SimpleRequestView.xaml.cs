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

namespace JsonWPFClient.SimpleRequest
{
    /// <summary>
    /// Interaction logic for AdvancedRequestView.xaml
    /// </summary>
    [View(typeof(SimpleRequestViewModel))]
    public partial class SimpleRequestView : UserControl
    {
        public SimpleRequestView()
        {
            InitializeComponent();
        }

        public SimpleRequestViewModel ViewModel
        {
            get { return (SimpleRequestViewModel) DataContext; }
        }
    }
}

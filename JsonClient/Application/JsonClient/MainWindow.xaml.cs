using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JsonWPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.LastUrl) == false)
                ViewModel.CurrentUrl = Properties.Settings.Default.LastUrl;
            advancedRequestView.ViewModel.RequestManager = ViewModel;
        }

        public MainViewModel ViewModel { get { return (MainViewModel) DataContext; } }
    }
}

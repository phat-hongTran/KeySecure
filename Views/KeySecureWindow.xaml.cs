using KeySecure.ViewModels;
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
using System.Windows.Shapes;

namespace KeySecure
{
    /// <summary>
    /// Interaction logic for SecureKeyWindow.xaml
    /// </summary>
    public partial class SecureKeyWindow : Window
    {
        private KeySecureViewModel viewModel;
        //private EncryptResultViewModel viewModelEncryptResult;
        public SecureKeyWindow()
        {
            InitializeComponent();

            viewModel = new KeySecureViewModel();
            
            DataContext = viewModel;
            //DataContext = viewModelEncryptResult;
        }
    }
}

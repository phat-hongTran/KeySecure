﻿using System;
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
using KeySecure.ViewModels;
namespace KeySecure.Views
{
    /// <summary>
    /// Interaction logic for EncryptResultWindow.xaml
    /// </summary>
    public partial class EncryptResultWindow : Window
    {
        //private EncryptResultViewModel viewModel;
        public EncryptResultWindow(EncryptResultViewModel viewModel)
        {
            InitializeComponent();
            //viewModel = new EncryptResultViewModel();
            DataContext = viewModel;
        }
    }
}

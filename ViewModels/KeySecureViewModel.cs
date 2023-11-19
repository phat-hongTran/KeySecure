using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using KeySecure.Views;
using System.Security.Cryptography;
using System.Security;


namespace KeySecure.ViewModels
{
    public class KeySecureViewModel : ViewModelBase
    {
        #region PROPERTIES
        #region Update Title
        private string title;
        private const string encryptTitle = "ENCRYPTION";
        private const string decryptTitle = "DECRYPTION";

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        private bool isDecrypt;

        public bool IsDecrypt
        {
            get { return isDecrypt; }
            set
            {
                isDecrypt = value;
                RaisePropertyChanged(nameof(IsDecrypt));
                UpdateTitle(value);
                UpdateHint(value);
            }
        }


        #endregion
        #region Update Hint in PasswordBox
        private string _PassWordBoxHint;
        private const string encryptHint = "Input your password here!";
        private const string decryptHint = "Input your encrypt string here!";

        public string PassWordBoxHint
        {
            get { return _PassWordBoxHint; }
            set
            {
                _PassWordBoxHint = value;
                RaisePropertyChanged(nameof(PassWordBoxHint));
            }
        }
        #endregion
        #region Show Secure Key TextBox
        private Visibility _textBox1Visibility;
        private Visibility _textBox2Visibility;
        private string _colorBtnAdd = "Gray";

        public Visibility TextBox1Visibility
        {
            get { return _textBox1Visibility; }
            set
            {
                _textBox1Visibility = value;
                RaisePropertyChanged(nameof(TextBox1Visibility));
            }
        }
        public Visibility TextBox2Visibility
        {
            get { return _textBox2Visibility; }
            set
            {
                _textBox2Visibility = value;
                RaisePropertyChanged(nameof(TextBox2Visibility));
            }
        }
        public string ColorAdd
        {
            get { return _colorBtnAdd; }
            set
            {
                _colorBtnAdd = value;
                RaisePropertyChanged(nameof(ColorAdd));
            }
        }
        #endregion
        #region Logic Encryption
        private string _password;
        private string _inputText1;
        private string _inputText2;
        private string _inputText3;
        private string _encryptedText;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }
        public string InputText1
        {
            get { return _inputText1; }
            set
            {
                _inputText1 = value;
                RaisePropertyChanged(nameof(InputText1));
            }
        }
        public string InputText2
        {
            get { return _inputText2; }
            set
            {
                _inputText2 = value;
                RaisePropertyChanged(nameof(InputText2));
            }
        }
        public string InputText3
        {
            get { return _inputText3; }
            set
            {
                _inputText3 = value;
                RaisePropertyChanged(nameof(InputText3));
            }
        }
        public string EncryptedText
        {
            get { return _encryptedText; }
            set
            {
                _encryptedText = value;
                RaisePropertyChanged(nameof(EncryptedText));
            }
        }
        #endregion
        #endregion
        #region METHODS
        #region Update Title
        private void UpdateTitle(bool isDecrypt)
        {
            Title = isDecrypt ? decryptTitle : encryptTitle;
        }
        #endregion
        #region Update Hint in PasswordBox
        private void UpdateHint(bool isDecrypt)
        {
            PassWordBoxHint = isDecrypt ? decryptHint : encryptHint;
        }
        #endregion
        #region Show Secure Key TextBox
        private void ToggleVisibility(object parameter)
        {
            if (TextBox1Visibility == Visibility.Collapsed)
            {
                TextBox1Visibility = Visibility.Visible;
            }
            else if (TextBox2Visibility == Visibility.Collapsed)
            {
                TextBox2Visibility = Visibility.Visible;
                ColorAdd = "Gray";
            }
        }
        #endregion
        #region Logic Encryption
        private void Encrypt()
        {
            string encryptedText = EncryptString(_password, InputText1, InputText2, InputText3);
            EncryptedText = encryptedText;
            //Binding to Result Window
            EncryptResultViewModel resultViewModel = new EncryptResultViewModel();
            resultViewModel.EncryptedText = encryptedText;
            EncryptResultWindow encryptResultWindow = new EncryptResultWindow();
            encryptResultWindow.DataContext = resultViewModel;
            encryptResultWindow.Show();
        }

        MD5 md = MD5.Create();
        private string EncryptString(string mainPw, string input1, string input2, string input3)
        {
            string concatenatedString = mainPw + input1 + input2 + input3;
            byte[] inputString = System.Text.Encoding.ASCII.GetBytes(concatenatedString);
            byte[] hash = md.ComputeHash(inputString);
            StringBuilder encryptedString = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                encryptedString.Append(hash[i].ToString("X2"));
            }
            return encryptedString.ToString();
        }
        #endregion
        #endregion
        #region COMMANDS
        #region Show Secure Key TextBox
        public ICommand ToggleVisibilityCommand { get; }
        #endregion
        #region Logic Encryption
        public ICommand EncryptCommand { get; }
        #endregion
        #endregion
        #region Constructor
        public KeySecureViewModel()
        {
            //Change content
            IsDecrypt = false;
            ColorAdd = "ForestGreen";

            //Visibility Add Secure Key textbox
            ToggleVisibilityCommand = new RelayCommand<object>(ToggleVisibility);
            TextBox1Visibility = Visibility.Collapsed;
            TextBox2Visibility = Visibility.Collapsed;

            //Show Result
            EncryptCommand = new RelayCommand(Encrypt);
        }
        #endregion
    }
}

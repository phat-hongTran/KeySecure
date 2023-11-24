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
using System.Windows.Forms;
using Clipboard = System.Windows.Forms.Clipboard;
using System.IO;
using MessageBox = System.Windows.Forms.MessageBox;

namespace KeySecure.ViewModels
{
    public class KeySecureViewModel : ViewModelBase
    {
        //PROPERTIES
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
                UpdateButtonVisibility();
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
        #region Update button name Encrypt-Decrypt
        private string _buttonTitle;
        private const string encryptButton = "Encrypt";
        private const string decryptButton = "Decrypt";

        public string ButtonTitle
        {
            get { return _buttonTitle; }
            set
            {
                _buttonTitle = value;
                RaisePropertyChanged(nameof(ButtonTitle));
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
        private string _decryptedText;

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
        public string DecryptedText
        {
            get { return _decryptedText; }
            set
            {
                _decryptedText = value;
                RaisePropertyChanged(nameof(DecryptedText));
            }
        }
        #endregion
        #region Copy to Clipboard
        private string _copyTextClipboard;
        public string CopyTextClipboard
        {
            get { return _copyTextClipboard; }
            set
            {
                _copyTextClipboard = value;
                RaisePropertyChanged(nameof(CopyTextClipboard));
            }
        }
        #endregion
        #region Show Encrypt - Decrypt button

        private bool isEncryptionEnabled;
        public bool IsEncryptionEnabled
        {
            get { return isEncryptionEnabled; }
            set
            {
                isEncryptionEnabled = value;
                RaisePropertyChanged(nameof(IsEncryptionEnabled));
                UpdateButtonVisibility();
            }
        }
        private bool isEncryptButtonVisible;
        public bool IsEncryptButtonVisible
        {
            get { return isEncryptButtonVisible; }
            set
            {
                isEncryptButtonVisible = value;
                RaisePropertyChanged(nameof(IsEncryptButtonVisible));
            }
        }

        private bool isDecryptButtonVisible;
        public bool IsDecryptButtonVisible
        {
            get { return isDecryptButtonVisible; }
            set
            {
                isDecryptButtonVisible = value;
                RaisePropertyChanged(nameof(IsDecryptButtonVisible));
            }
        }

        #endregion

        //METHODS
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
        #region Update Button Title
        private void UpdateButtonTitle(bool isDecrypt)
        {
            ButtonTitle = isDecrypt ? decryptButton : encryptButton;
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

            string encryptedText = EncryptString(Password, InputText1, InputText2, InputText3);
            EncryptedText = encryptedText;
            EncryptResultViewModel resultViewModel = new EncryptResultViewModel();
            resultViewModel.EncryptedText = encryptedText;
            EncryptResultWindow encryptResultWindow = new EncryptResultWindow();
            encryptResultWindow.DataContext = resultViewModel;
            encryptResultWindow.Show();
        }

        public static string EncryptString(string mainPassword, string input1, string input2, string input3)
        {
            string hash = input1 + input2 + input3;
            string concateString = mainPassword + hash;
            byte[] data = UTF8Encoding.UTF8.GetBytes(concateString);

            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF32Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    string enCryptmainPassword = Convert.ToBase64String(results, 0, results.Length);

                    return enCryptmainPassword;
                }
            }
        }
        #endregion
        #region Copy to Clipboard
        private void CopyText()
        {
            Clipboard.SetText(EncryptedText);
        }
        #endregion
        #region Logic Decrypt
        private void Decrypt()
        {
            string decryptedText = DecryptString(Password, InputText1, InputText2, InputText3);
            DecryptedText = decryptedText;
            //Binding to Result Window
            DecryptResultViewModel resultViewModel = new DecryptResultViewModel();
            resultViewModel.DecryptedText = decryptedText;
            DecryptResultWindow decryptResultWindow = new DecryptResultWindow();
            decryptResultWindow.DataContext = resultViewModel;
            decryptResultWindow.Show();
        }
        public static string DecryptString(string mainEncrString, string input1, string input2, string input3)
        {
            try
            {
                string hash = input1 + input2 + input3;
                byte[] data = Convert.FromBase64String(mainEncrString);

                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateDecryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        string decryptStringResult = UTF8Encoding.UTF8.GetString(results);

                        return decryptStringResult;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        #endregion
        #region Show Encrypt - Decrypt button
        private void UpdateButtonVisibility()
        {
            if (!IsDecrypt)
            {
                IsEncryptButtonVisible = true;
                IsDecryptButtonVisible = false;
            }
            else
            {
                IsEncryptButtonVisible = false;
                IsDecryptButtonVisible = true;
            }
        }
        #endregion
        //COMMANDS
        #region Show Secure Key TextBox
        public ICommand ToggleVisibilityCommand { get; }
        #endregion
        #region Logic Encryption
        public ICommand EncryptCommand { get; }
        //public ICommand DecryptCommand { get; }
        #endregion
        #region Copy to Clipboard 
        public ICommand CopyToClipBoardCommand { get; }
        #endregion
        #region Logic Decrypt
        public ICommand DecryptCommand { get; }
        #endregion
        //Constructor
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
            DecryptCommand = new RelayCommand(Decrypt);
            EncryptCommand = new RelayCommand(Encrypt);

            //Copy To Clipboar
            CopyToClipBoardCommand = new RelayCommand(CopyText);
            //Show Button Encrypt Decrypt
        }
        #endregion
    }
}

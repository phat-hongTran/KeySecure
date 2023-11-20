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
                UpdateButtonTitle(value);
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
            string encryptedText = EncryptString(_password, InputText1, InputText2, InputText3);
            EncryptedText = encryptedText;
            //Binding to Result Window
            EncryptResultViewModel resultViewModel = new EncryptResultViewModel();
            resultViewModel.EncryptedText = encryptedText;
            EncryptResultWindow encryptResultWindow = new EncryptResultWindow();
            encryptResultWindow.DataContext = resultViewModel;
            encryptResultWindow.Show();
        }

        public static string EncryptString(string mainPw, string input1, string input2, string input3)
        {
            string concatenatedString = mainPw + input1 + input2 + input3;
            string EncryptionKey = input1 + input2 + input3;
            byte[] clearBytes = Encoding.Unicode.GetBytes(concatenatedString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    concatenatedString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return concatenatedString.ToString();
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
            string decryptedText = DecryptString(_password, InputText1, InputText2, InputText3);
            DecryptedText = decryptedText;
            //Binding to Result Window
            DecryptResultViewModel resultViewModel = new DecryptResultViewModel();
            resultViewModel.DecryptedText = decryptedText;
            DecryptResultWindow decryptResultWindow = new DecryptResultWindow();
            decryptResultWindow.DataContext = resultViewModel;
            decryptResultWindow.Show();
        }
        public static string DecryptString(string mainEncr, string input1, string input2, string input3)
        {
            //string concatenatedEcrString = mainEncr + input1 + input2 + input3;
            string EncryptionKey = input1 + input2 + input3; ;
            mainEncr = mainEncr.Replace(" ", "+");
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(mainEncr);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        mainEncr = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.Application.Exit();
            }
            return mainEncr.ToString();
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
            if (IsDecrypt == true)
            {
                EncryptCommand = new RelayCommand(Decrypt);
            }
            else if (IsDecrypt == false)
            {
                EncryptCommand = new RelayCommand(Encrypt);
            }
            //Copy To Clipboar
            CopyToClipBoardCommand = new RelayCommand(CopyText);
        }
        #endregion
    }
}

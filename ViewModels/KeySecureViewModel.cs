using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace KeySecure.ViewModels
{
    public class KeySecureViewModel : ViewModelBase
    {
        public KeySecureViewModel()
        {
            //Change content
            IsDecrypt = false;
            //Add textbox
            SercureKeyCollection = new ObservableCollection<string>();
            AddSecureKey = new RelayCommand<object>(CommandAddSecureKey);
        }
        #region Update Titel
        private string title;
		private const string encryptTitle = "ENCRYPTION";
        private const string decryptTitle = "DECRYPTION";

        public string Title
		{
			get { return title; }
			set { 
				title = value;
				RaisePropertyChanged(nameof(Title));
			}
		}

		private bool isDecrypt;

		public bool IsDecrypt
		{
			get { return isDecrypt; }
			set {
                isDecrypt = value; 
				RaisePropertyChanged(nameof(IsDecrypt));
				UpdateTitle(value);
                UpdateHint(value);
			}
		}

        private void UpdateTitle(bool isDecrypt)
        {
			Title = isDecrypt ? decryptTitle : encryptTitle;
        }
        #endregion
        #region Update Hint in main PasswordBox
        private string _PassWordBoxHint;
        private const string encryptHint= "Input your password here!";
        private const string decryptHint= "Input your encrypt string here!";

        public string PassWordBoxHint
        {
            get { return _PassWordBoxHint; }
            set 
            { 
                _PassWordBoxHint = value;
                RaisePropertyChanged(nameof(PassWordBoxHint));
            }
        }
        private void UpdateHint (bool isDecrypt)
        {
            PassWordBoxHint = isDecrypt ? decryptHint : encryptHint;
        }
        #endregion
        #region Add Textbox for Secure Key
        public ObservableCollection<string> SercureKeyCollection { get; set; }
        public ICommand AddSecureKey
        {
            get;
            private set;
        }
        private void CommandAddSecureKey(object parameter)
        {
            if (SercureKeyCollection.Count < 2)
            {
                SercureKeyCollection.Add("Add secure key");
            }
        }
        #endregion
    }
}

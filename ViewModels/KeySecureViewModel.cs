using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeySecure.ViewModels
{
    public class KeySecureViewModel : ViewModelBase
    {
		private string title;
		private const string encryptTitle = "Encryption Page";
        private const string decryptTitle = "Decryption Page";

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

        public KeySecureViewModel()
        {
			IsDecrypt = false;
        }

        #region Update Hint in main textbox
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
    }
}

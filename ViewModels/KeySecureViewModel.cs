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
    }
}

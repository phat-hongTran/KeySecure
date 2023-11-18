using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeySecure.ViewModels
{
    public class EncryptResultViewModel: ViewModelBase
    {

        #region Show Result Dailog
        private string _encryptedText;
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

    }
}

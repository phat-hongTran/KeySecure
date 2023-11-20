using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeySecure.ViewModels
{
    public class DecryptResultViewModel : ViewModelBase
    {
        //Properties

        private string _decryptedText;
        public string DecryptedText
        {
            get { return _decryptedText; }
            set
            {
                _decryptedText = value;
                RaisePropertyChanged(nameof(DecryptedText));
            }
        }
        

    }
}

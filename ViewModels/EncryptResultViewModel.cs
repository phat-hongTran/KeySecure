using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Clipboard = System.Windows.Forms.Clipboard;
using GalaSoft.MvvmLight.Command;

namespace KeySecure.ViewModels
{
    public class EncryptResultViewModel : ViewModelBase
    {
        //PROPERTIES
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
        #endregion
        #region Copy to Clipboard
        private string _contentCopyButton;

        public string ContentCopyButton
        {
            get { return _contentCopyButton; }
            set
            {
                _contentCopyButton = value;
                RaisePropertyChanged(nameof(ContentCopyButton));
            }
        }

        private string _contentBeforeCopy = "Copy";
        private string _contentAfterCopy = "OK";
        private string _copyTextClipboard;

        public string ContentBeforeCopy
        {
            get { return _contentBeforeCopy; }
            set
            {
                _contentBeforeCopy = value;
                RaisePropertyChanged(nameof(ContentBeforeCopy));
            }
        }
        public string ContentAfterCopy
        {
            get { return _contentAfterCopy; }
            set
            {
                _contentAfterCopy = value;
                RaisePropertyChanged(nameof(ContentAfterCopy));
            }
        }
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
        #region Copy to Clipboard
        private void CopyText()
        {
            Clipboard.SetText(EncryptedText);
            ContentCopyButton = ContentAfterCopy;
        }
        #endregion
        //COMMANDS
        #region Copy to Clipboard 
        public ICommand CopyToClipBoardCommand { get; }
        #endregion
        //CONSTRUCTOR
        //Copy To Clipboar
        public EncryptResultViewModel()
        {
            ContentCopyButton = ContentBeforeCopy;
            CopyToClipBoardCommand = new RelayCommand(CopyText);

        }
    }
}

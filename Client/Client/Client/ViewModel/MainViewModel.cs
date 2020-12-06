using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Client clientCom;
        private bool isConnected = false;
        #region PROPERTIES
        public string ChatName { get; set; }
        public string Message { get; set; }
        public ObservableCollection<string> ReceivedMessages { get; set; }
        public RelayCommand ConnectButtionClickCmd { get; set; }
        public RelayCommand SendButtonClickCmd { get; set; }
        #endregion

        public MainViewModel()
        {
            Message = "";
            ReceivedMessages = new ObservableCollection<string>();

            ConnectButtionClickCmd = new RelayCommand(() =>
            {
                isConnected = true;
                clientCom = new Client("127.0.0.1", 10100, new Action<string>(NewMessageReceived), ClientDissconnected);
            },
            () =>
            {
                return (!isConnected);
            });
        }

        private void ClientDissconnected()
        {
            isConnected = false;
            //Updates the button visiblity fires the event or whatever
            CommandManager.InvalidateRequerySuggestion();
        }

        private void NewMessageReceived(string message)
        {
            App.Current.Dispatcher.Invoke(() => {
                ReceivedMessages.Add(message);
            });
        }

    }
}

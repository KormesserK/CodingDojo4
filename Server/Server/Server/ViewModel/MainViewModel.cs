using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using DataHandling;
using Server.Communication;

namespace Server.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        //never use classname with same name as Program name 
        private ServerServe _server;
        private const int _port = 10100;
        private const string _ip = "127.0.0.1";
        private bool _isConnected = false;
        private DataHandler _dHandler;

        #region PROPERTIES 
        public GalaSoft.MvvmLight.CommandWpf.RelayCommand StartBtnClickCmd { get; set; }
        public GalaSoft.MvvmLight.CommandWpf.RelayCommand StopBtnClickCmd { get; set; }
        public GalaSoft.MvvmLight.CommandWpf.RelayCommand DropClientBtnClickCmd { get; set; }
        public GalaSoft.MvvmLight.CommandWpf.RelayCommand SaveToLogBtnClickCmd { get; set; }
        public ObservableCollection<string> Users { get; set; }
        public ObservableCollection<string> Messages { get; set; }

        public string SelectedUser { get; set; }

        public int NumberOfREceivedMessages
        {
            get
            {
                return Messages.Count;
            }
        }
        #endregion
        #region Bonus Task Properties
        public ObservableCollection<string> LogFiles
        {
            get
            {
                return new ObservableCollection<string>(_dHandler.QueryFilesFromFolder());
            }
        }

        public ObservableCollection<string> LogMessages { get; set; }
        public string SelectedLogFile { get; set; }

        public GalaSoft.MvvmLight.CommandWpf.RelayCommand OpenLogFileBtnClickCmd { get; set; }
        public GalaSoft.MvvmLight.CommandWpf.RelayCommand DropLogFileBtnClickCmd { get; set; }
        #endregion 

        public MainViewModel ()
        {
            _dHandler = new DataHandler();
            Messages = new ObservableCollection<string>();
            Users = new ObservableCollection<string>();
            LogMessages = new ObservableCollection<string>();

            //set command for start button
            StartBtnClickCmd = new RelayCommand(
                () =>
                {
                    _server = new ServerServe(_ip, _port, UpdateGuiWithNewMessage);
                    _server.StartAccepting();
                    _isConnected = true;
                },
                () => { return !_isConnected; });

            //set command for stop button
            StopBtnClickCmd = new RelayCommand(
                //action for execute
                () =>
                {
                    _server.StopAccepting();
                    _isConnected = false;
                },
                //can execute
                () => { return _isConnected; });

            //init Command for Drop button with CanExecute statement
            DropClientBtnClickCmd = new RelayCommand(() =>
            {
                _server.DisconnectSpecificClient(SelectedUser);
                Users.Remove(SelectedUser); // remove from GUI listbox
            },
                () => { return (SelectedUser != null); });

            //init Command for SaveToLogFIle button with CanExecute statement
            SaveToLogBtnClickCmd = new RelayCommand(
                () =>
                {
                    _dHandler.Save(Messages.ToList());
                    RaisePropertyChanged("LogFiles"); //to update the list in the log section
                },
                () => { return Messages.Count >= 1; }
                );

            //init Command for OpenLogFile button with CanExecute statement
            OpenLogFileBtnClickCmd = new RelayCommand(
                () =>
                {
                    LogMessages = new ObservableCollection<string>(_dHandler.Load(SelectedLogFile));
                    RaisePropertyChanged("LogMessages");
                },
                () => { return SelectedLogFile != null; }
                );
            //init Command for Drop Log files button with CanExecute statement
            DropLogFileBtnClickCmd = new RelayCommand(
                () =>
                {
                    _dHandler.Delete(SelectedLogFile);
                    RaisePropertyChanged("LogFiles"); //to update the list in the log section},
                },
                () => { return SelectedLogFile != null; });
        }

        public void UpdateGuiWithNewMessage(string message)
        {
            //switch thread to GUI thread to write to GUI
            App.Current.Dispatcher.Invoke(() =>
            {
                string name = message.Split(':')[0];
                if (!Users.Contains(name))
                {//not in list => add it
                    Users.Add(name);
                }
                if (message.Contains("@quit"))
                {
                    _server.DisconnectSpecificClient(name);
                }
                //write message
                Messages.Add(message);
                //do this to inform the GUI about the update of the received message counter!
                RaisePropertyChanged("NoOfReceivedMessages");
            });
        }

    }
}

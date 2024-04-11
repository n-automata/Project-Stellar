using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerGUI.MVVM.ViewModel
{


    public class HomeViewModel : INotifyPropertyChanged
    {
        private string _numUsersConnected;
        public string NumUsersConnected
        {
            get { return _numUsersConnected; }
            set
            {
                _numUsersConnected = value;
                OnPropertyChanged(nameof(NumUsersConnected));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

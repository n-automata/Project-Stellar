using ServerGUI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerGUI.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {

        public HomeViewModel HomeVM { get; set; }

        private object currentView;

        public object CurrentView
        {
            get { return currentView; }
            set { currentView = value;
                OnPropertyChanged();
            }
        }




        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            CurrentView = HomeVM;
        }


    }
}

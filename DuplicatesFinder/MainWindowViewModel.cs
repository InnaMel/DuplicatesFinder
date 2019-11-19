using DuplicatesFinder.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace DuplicatesFinder
{
    public class MainWindowViewModel : BindingItems
    {

        public RelayCommand ButtonMinimazing { get; set; }
        public RelayCommand ButtonMaximazing { get; set; }
        public RelayCommand ButtonClosing { get; set; }
        public Action CloseMainWindow { get; set; }

        WindowState currentWindowState;
        public WindowState CurrentWindowState
        {
            get { return currentWindowState; }
            set { SetField(ref currentWindowState, value); }
        }

        public MainWindowViewModel()
        {
            ButtonMinimazing = new RelayCommand(() => CurrentWindowState = WindowState.Minimized);
            ButtonMaximazing = new RelayCommand(() => CurrentWindowState = WindowState.Maximized);
            ButtonClosing = new RelayCommand(() => CloseMainWindow());
        }

    }
}

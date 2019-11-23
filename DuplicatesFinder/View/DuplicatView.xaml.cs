using DuplicatesFinder.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DuplicatesFinder.View
{
    /// <summary>
    /// interaction logic for DuplicatView.xaml
    /// </summary>
    public partial class DuplicatView : UserControl
    {
        bool ifHasWritten = false;
        DoubleAnimation UKHeightAnimation;

        /// <summary>
        /// constructor for initializing 
        /// </summary>
        public DuplicatView()
        {
            InitializeComponent();

            UKHeightAnimation = new DoubleAnimation();
        }

        DuplicatesViewModel DuplicatesViewModelObject { get; set; }

        /// <summary>
        /// the first action when View is load
        /// </summary>
        private void WindowHasLoaded(object j, RoutedEventArgs e)
        {
            forEnterPath.Focus();
            forEnterPath.Text = "Enter the path for searching for duplicates";
            DuplicatesViewModelObject = new ViewModel.DuplicatesViewModel();
            DuplicatesViewModelObject.EnteredPathFromUser = forEnterPath.Text;

            this.DataContext = DuplicatesViewModelObject;
            DuplicatesViewModelObject.GetAnimation += StartUKHeightAnimation;
        }


        /// <summary>
        /// working with textbox using x:Name. Clearing text when keydown in that field.
        /// </summary>
        private void Clear_Text(Object j, RoutedEventArgs e)
        {
            if (ifHasWritten == false)
            {
                forEnterPath.Clear();
                ifHasWritten = true;
            }
        }

        public void StartUKHeightAnimation()
        {
            UKHeightAnimation.From = DuplicatViewControl.ActualHeight;
            UKHeightAnimation.To = 220;
            UKHeightAnimation.Duration = TimeSpan.FromSeconds(3);
            DuplicatViewControl.BeginAnimation(UserControl.HeightProperty, UKHeightAnimation);
        }
    }
}

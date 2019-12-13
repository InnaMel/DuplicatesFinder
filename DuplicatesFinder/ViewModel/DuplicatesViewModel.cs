using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using DuplicatesFinder.Model;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.IO;
using System.Windows.Media;

namespace DuplicatesFinder.ViewModel
{
    public class DuplicatesViewModel : BindingItems
    {
        public bool IsChecked { get; set; }

        //public RelayCommand ButtonMinimazing { get; set; }
        //public RelayCommand ButtonMaximazing { get; set; }
        //public RelayCommand ButtonClosing { get; set; }
        //public Action CloseMainWindow { get; set; }

        //WindowState currentWindowState;
        //public WindowState CurrentWindowState
        //{
        //    get { return currentWindowState; }
        //    set { SetField(ref currentWindowState, value); }
        //}

        public DuplicatesViewModel()
        {
            //ButtonMinimazing = new RelayCommand(() => CurrentWindowState = WindowState.Minimized);
            //ButtonMaximazing = new RelayCommand(() => CurrentWindowState = WindowState.Maximized);
            //ButtonClosing = new RelayCommand(() => CloseMainWindow());

            DuplicatesList = new ObservableCollection<ObservListWithNameForTemplate>();
            LogicalDriveNames = new List<string>();
            FindAllLogicalDriveNames();
            AllCheckedBoxesForCheck = new List<CheckBoxesModel>();
            CheckedUI = new CheckBoxesModel();

            ShowAllCheckedBoxes();
            //GetAllChecked();
        }


        private void ShowAllCheckedBoxes()
        {
            //foreach (var item in LogicalDriveNames)
            //{
            //    AllCheckedBoxes.Add(new ListCheckBoxesModel(item, false));
            //};

            AllCheckedBoxesForCheck = LogicalDriveNames.Select(name => new CheckBoxesModel(name, false)).ToList();
        }

        public CheckBoxesModel CheckedUI { get; set; }
        public List<CheckBoxesModel> AllCheckedBoxesForCheck { get; set; }
        public void GetChecked()
        {
            var CheckedList = AllCheckedBoxesForCheck.Where(x => x.IsCheckedCheckBox).ToList();
            if (CheckedList.Count != 0)
            {
                CheckedUI = CheckedList[0];
            }
        }

        private double opacityEnteredText = 0.4;
        public double OpacityEnteredText
        {
            get { return opacityEnteredText; }
            set
            {
                if (opacityEnteredText != value)
                {
                    opacityEnteredText = value;
                    RaisePropertyChanged("OpacityEnteredText");
                }
            }
        }

        private System.Windows.Media.SolidColorBrush colorEnteredText = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#7a7a7a"));
        public System.Windows.Media.SolidColorBrush ColorEnteredText
        {
            get { return colorEnteredText; }
            set
            {
                if (colorEnteredText != value)
                {
                    colorEnteredText = value;
                    RaisePropertyChanged("ColorEnteredText");
                }
            }
        }

        private string enteredPathFromUser;
        /// <summary>
        /// using method "RaisePropertyChanged" as implementation Interface "INotifyPropertyChanged"
        /// </summary>
        public string EnteredPathFromUser
        {
            get
            {
                return enteredPathFromUser;
            }
            set
            {
                if (enteredPathFromUser != value)
                {
                    enteredPathFromUser = value;
                    if (enteredPathFromUser != "Enter the path for searching for duplicates")
                    {
                        OpacityEnteredText = 1;
                        ColorEnteredText = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#f03a02"));
                    }
                    RaisePropertyChanged("EnteredPathFromUser");
                }
            }
        }

        public List<string> LogicalDriveNames { get; set; }

        public void FindAllLogicalDriveNames()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            LogicalDriveNames = allDrives.Select(x => x.Name).ToList();
        }


        /// <summary>
        /// it will be says view that DuplicatesList has new pointer
        /// </summary>
        private ObservableCollection<ObservListWithNameForTemplate> duplicatesList;
        public ObservableCollection<ObservListWithNameForTemplate> DuplicatesList
        {
            get { return duplicatesList; }
            set
            {
                if (duplicatesList != value)
                {
                    duplicatesList = value;
                    RaisePropertyChanged("DuplicatesList");
                }
            }
        }

        /// <summary>
        /// Looking up the all duplicates and assign them to "ObservableCollection"
        /// </summary>
        public async void GetAllListsDuplicatesForForm(string userPath)
        {
            await Task.Run(() =>
            {
                List<List<DuplicatesModel>> AllListsDuplicates = new List<List<DuplicatesModel>>();
                ObservableCollection<ObservListWithNameForTemplate> AllObservsDuplicatesForForm = new ObservableCollection<ObservListWithNameForTemplate>();

                if (HelperMetods.CheckDirectoryExists(userPath))
                {
                    AllListsDuplicates = HelperMetods.GetDublicate(HelperMetods.GetAllFiles(userPath));

                    foreach (List<DuplicatesModel> item in AllListsDuplicates)
                    {
                        AllObservsDuplicatesForForm.Add(HelperMetods.ConvertToCosilyDisplay(item));
                    }
                }
                else
                    MessageBox.Show("Can't find such path", "ERROR PATH", MessageBoxButton.OK, MessageBoxImage.Error);

                //foreach (var item in AllObservsDuplicatesForForm)
                //{
                //    DuplicatesList.Add(item);
                //}
                DuplicatesList = AllObservsDuplicatesForForm;
            });
        }

        public event Action GetAnimation;

        /// <summary>
        /// Employing class "RelayCommand" for using Command for the button (at searching duplicates)
        /// </summary>
        private ICommand onClick;

        public ICommand OnClick
        {
            get
            {
                return onClick ?? (onClick = new RelayCommand(() =>
                {
                    GetChecked();
                    if (CheckedUI.Data != null)
                        EnteredPathFromUser = CheckedUI.Data;
                    //GetAnimation();
                    GetAllListsDuplicatesForForm(EnteredPathFromUser);
                }
                ));
            }
        }


        /// <summary>
        /// Employing class "RelayCommand" for using Command for the directorys button (for opening dialog for choose folder)
        /// </summary>
        private ICommand onClickDirectory;
        public ICommand OnClickDirectory
        {
            get
            {
                return onClickDirectory ?? (onClickDirectory = new RelayCommand(() =>
                {
                    Ookii.Dialogs.Wpf.VistaFolderBrowserDialog myFBD = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
                    myFBD.ShowDialog();
                    EnteredPathFromUser = myFBD.SelectedPath;
                }
                ));
            }
        }

    }
}

using ClubArcada.BusinessObjects;
using ClubArcada.BusinessObjects.DataClasses;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ClubArcada.PokerTimer.Win.Dialogs
{
    public partial class SelectStructureDlg : Window, INotifyPropertyChanged
    {
        # region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void PropertyChange(Property property)
        {
            PropertyChanged.Raise(this, property.ToString());
        }

        private enum Property
        {
            NotSet = 0,
            IsEditEnabled,
            IsBusy
        }

        private void RefreshValues()
        {
            PropertyChange(Property.IsEditEnabled);
            PropertyChange(Property.IsBusy);
        }

        # endregion

        # region Properties

        private bool _isEditEnabled;
        public bool IsEditEnabled
        {
            get
            {
                return _isEditEnabled;
            }
            set
            {
                _isEditEnabled = true;
                RefreshValues();
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                RefreshValues();
            }
        }

        public Structure SelectedStructure { get; set; }

        # endregion

        public SelectStructureDlg()
        {
            DataContext = this;
            InitializeComponent();
            BindData();
        }

        private void BindData()
        {
            var structList = new List<Structure>();

            var worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                structList = BusinessObjects.Data.StructureData.GetList();
            };

            worker.RunWorkerCompleted += delegate
            {
                lbxStructures.ItemsSource = structList;
                IsBusy = false;
            };

            IsBusy = true;
            worker.RunWorkerAsync();
        }

        # region Events

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsEditEnabled = (sender as ListBox).SelectedItem.IsNotNull();

            RefreshValues();
        }

        private void btnClose_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSelect_click(object sender, RoutedEventArgs e)
        {
            SelectedStructure = lbxStructures.SelectedItem as Structure;
            this.Close();
        }

        # endregion
    }
}

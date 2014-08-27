using ClubArcada.BusinessObjects;
using ClubArcada.BusinessObjects.DataClasses;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using System.Linq;
using System.Collections.Generic;

namespace ClubArcada.PokerTimer.Win
{
    public partial class MainWindow : Window, INotifyPropertyChanged
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
            Tournament,
            CurrentLevel,
            PlayerList,
            PlayerCountActive,
            PlayerCount,
            InfoCtrlLeft04Type,
            InfoCtrlLeft04Value,
            AvgStack,
            IsDark
        }

        private void RefreshValues()
        {
            PropertyChange(Property.InfoCtrlLeft04Type);
            PropertyChange(Property.InfoCtrlLeft04Value);
            PropertyChange(Property.AvgStack);
            
        }

        # endregion

        # region Properties

        private Tournament _tournament;
        public Tournament Tournament
        {
            get
            {
                return _tournament;
            }
            set
            {
                _tournament = value;
                PropertyChange(Property.Tournament);
            }
        }

        public ObservableCollection<TournamentResult> PlayerList {get; set;}

        private int _currentLevel;
        public int CurrentLevel 
        { 
            get 
            { 
                return _currentLevel; 
            }
            set
            {
                _currentLevel = value;
                PropertyChange(Property.CurrentLevel);
            }
        }

        public int BonusStackCount { get { return PlayerList.IsNotNull() ? PlayerList.Sum(p => p.BonusStackTotal) : 0; } private set { } }

        public int PlayerCount { get { return PlayerList.IsNotNull() ? PlayerList.Count : 0; } private set { } }
        public int PlayerCountActive { get { return PlayerList.IsNotNull() ? PlayerList.Where(p => !p.DateDeleted.HasValue).Count() : 0; } private set { } }
        public int ReEntryCount { get { return PlayerList.IsNotNull() ? PlayerList.Where(p => p.DateReEntry.HasValue).Count() : 0; } private set { } }

        public int ReBuyCount { get { return PlayerList.IsNotNull() ? PlayerList.Sum(p => p.ReBuyCount) : 0; } }
        public int AddOnCount { get { return PlayerList.IsNotNull() ? PlayerList.Sum(p => p.AddOnCount) : 0; } }
        public int TotalChips { get { return PlayerCount != 0 ? PlayerCount * Tournament.TournamentDetail.BuyInStack + ReBuyCount * Tournament.TournamentDetail.ReBuyStack + AddOnCount * Tournament.TournamentDetail.AddOnStack : 0; } private set { } }

        public int AvgStack { get { return PlayerCount != 0 ? (TotalChips + BonusStackCount) / PlayerCountActive : 0; } private set { } }
        public int TotalBank { get { return PlayerCount * Tournament.TournamentDetail.BuyInPrize; } private set { } }
        
        public eInfoCtrlType InfoCtrlLeft04Type
        {
            get
            {
                if (Tournament.TournamentDetail.ReEntryCount.HasValue && Tournament.TournamentDetail.ReEntryCount > 0)
                    return eInfoCtrlType.ReEntryCount;
                else return eInfoCtrlType.NotSet;
            }
            set { }
        }
        public string InfoCtrlLeft04Value
        {
            get
            {
                if (Tournament.TournamentDetail.ReEntryCount.HasValue && Tournament.TournamentDetail.ReEntryCount > 0)
                    return ReEntryCount.ToString();
                else
                    return string.Empty;
            }
            set { }
        }

        # endregion

        public MainWindow()
        {
            BindData();
            InitializeComponent();
            DataContext = this;
            

            this.KeyUp += MainWindow_KeyUp;
            RefreshValues();
        }

        public void BindData()
        {
            PlayerList = new ObservableCollection<TournamentResult>();
            PlayerList.CollectionChanged += PlayerList_CollectionChanged;

            var dlgTourSelect = new Dialogs.SelectTournamentDlg();
            dlgTourSelect.ShowDialog();

            Tournament = dlgTourSelect.SelectedTournament;

            Tournament.LoadTournamentDetails();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void PlayerList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            PropertyChange(Property.PlayerCount);
            PropertyChange(Property.PlayerCountActive);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            
        }

        private void MainWindow_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.F3)
            {
                var playerDlg = new Dialogs.PlayerListDialog(PlayerList);
                playerDlg.ShowDialog();
                RefreshValues();
            }

            if (e.Key == System.Windows.Input.Key.F4)
            {
                var settingsDlg = new Dialogs.SettingsDlg(PlayerList, Tournament);
                settingsDlg.ShowDialog();
                RefreshValues();
            }
        }

        private void btnPlayList_Click(object sender, RoutedEventArgs e)
        {
            var playerDlg = new Dialogs.PlayerListDialog(PlayerList);
            playerDlg.ShowDialog();
            RefreshValues();
        }

        private void btnAddPlayer_Click(object sender, RoutedEventArgs e)
        {
            var settingsDlg = new Dialogs.SettingsDlg(PlayerList, Tournament.GetCopy());
            settingsDlg.ShowDialog();
            RefreshValues();
        }

        
    }
}

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
            IsDark,
            ChipsTotal,
            ReBuyCount,
            AddOnCount,
            MoneyPool,
            PrizePool,
            RakeMoney,
            LeagueMoney
        }

        public void RefreshValues()
        {
            PropertyChange(Property.InfoCtrlLeft04Type);
            PropertyChange(Property.InfoCtrlLeft04Value);
            PropertyChange(Property.AvgStack);
            PropertyChange(Property.ChipsTotal);
            PropertyChange(Property.ReBuyCount);
            PropertyChange(Property.AddOnCount);
            PropertyChange(Property.MoneyPool);
            PropertyChange(Property.PrizePool);
            PropertyChange(Property.RakeMoney);
            PropertyChange(Property.LeagueMoney);
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

        public ObservableCollection<TournamentResult> PlayerList { get; set; }

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

        public int ReBuyCount { get { return PlayerList.IsNotNull() ? PlayerList.Sum(p => p.ReBuyCount) : 0; } private set { } }
        public int AddOnCount { get { return PlayerList.IsNotNull() ? PlayerList.Sum(p => p.AddOnCount) : 0; } private set { } }
        public int ChipsTotal
        {
            get
            {
                if (PlayerCount == 0)
                    return 0;

                var buyIns = PlayerCount * Tournament.TournamentDetail.BuyInStack;
                var reBuys = ReBuyCount * Tournament.TournamentDetail.ReBuyStack;
                var addOns = AddOnCount * Tournament.TournamentDetail.AddOnStack;

                return buyIns + reBuys + addOns + BonusStackCount;
            }
            private set
            {
            }
        }

        public int AvgStack { get { return PlayerCount != 0 ? ChipsTotal / PlayerCountActive : 0; } private set { } }
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

        public Double MoneyPool { get { return PlayerList.Count * Tournament.TournamentDetail.BuyInPrize + ReBuyCount * Tournament.TournamentDetail.ReBuyPrize + AddOnCount * Tournament.TournamentDetail.AddOnPrize; } private set { } }
        public Double LeagueMoney { get { return (MoneyPool * 0.10).GetRounded(); } private set { } }
        public Double RakeMoney { get { return (MoneyPool * 0.15).GetRounded(); } private set { } }
        public Double PrizePool { get { return (MoneyPool - (LeagueMoney + RakeMoney)).GetRounded(); } private set { } }

        public Double Dotation { get { return Math.Abs(Tournament.TournamentDetail.GTD.HasValue && Tournament.TournamentDetail.GTD.Value > PrizePool ? PrizePool - Tournament.TournamentDetail.GTD.Value : 0); } }

        # endregion

        # region Tables

        public List<Tables> TableList { get; set; }

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
            if (e.Key == System.Windows.Input.Key.F1)
            {
                var settingsDlg = new Dialogs.TournamentSettingsDlg();
                settingsDlg.ShowDialog();
                RefreshValues();
            }

            if (e.Key == System.Windows.Input.Key.F3)
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

        private void btnTournamentSettings_Click(object sender, RoutedEventArgs e)
        {
            var settingsDlg = new Dialogs.TournamentSettingsDlg();
            settingsDlg.ShowDialog();
            RefreshValues();
        }

        public Double[] GetPercents()
        {
            var enumlist = Enum.GetValues(typeof(ePositionSeqs)).Cast<ePositionSeqs>().Select(p => (int)p).ToList();
            var selectedEnum = (ePositionSeqs)enumlist.Where(p => p <= PlayerList.Count).Max();
            var positionEnum = (ePositions)Enum.Parse(typeof(ePositions), selectedEnum.ToString());
            var percents = positionEnum.GetEnumDescription().Split('_').Select(p => Double.Parse(p)).ToArray();

            return percents;
        }

        public int[] GetPlaces()
        {
            int[] paymentList = new int[20];
            var percents = GetPercents();

            paymentList[0] = (int)GetNumValViaPercents(PrizePool, percents[0]);
            paymentList[1] = (int)GetNumValViaPercents(PrizePool, percents[1]);
            paymentList[2] = (int)GetNumValViaPercents(PrizePool, percents[2]);
            paymentList[3] = (int)GetNumValViaPercents(PrizePool, percents[3]);
            paymentList[4] = (int)GetNumValViaPercents(PrizePool, percents[4]);
            paymentList[5] = (int)GetNumValViaPercents(PrizePool, percents[5]);
            paymentList[6] = (int)GetNumValViaPercents(PrizePool, percents[6]);
            paymentList[7] = (int)GetNumValViaPercents(PrizePool, percents[7]);
            paymentList[8] = (int)GetNumValViaPercents(PrizePool, percents[8]);
            paymentList[9] = (int)GetNumValViaPercents(PrizePool, percents[9]);
            paymentList[10] = (int)GetNumValViaPercents(PrizePool, percents[10]);
            paymentList[11] = (int)GetNumValViaPercents(PrizePool, percents[11]);
            paymentList[12] = (int)GetNumValViaPercents(PrizePool, percents[12]);
            paymentList[13] = (int)GetNumValViaPercents(PrizePool, percents[13]);
            paymentList[14] = (int)GetNumValViaPercents(PrizePool, percents[14]);
            paymentList[15] = (int)GetNumValViaPercents(PrizePool, percents[15]);
            paymentList[16] = (int)GetNumValViaPercents(PrizePool, percents[16]);
            paymentList[17] = (int)GetNumValViaPercents(PrizePool, percents[17]);
            paymentList[18] = (int)GetNumValViaPercents(PrizePool, percents[18]);
            paymentList[19] = (int)GetNumValViaPercents(PrizePool, percents[19]);

            return paymentList;
        }

        public static Double GetNumValViaPercents(Double origin, Double percent)
        {
            if (percent == 0 || origin == 0)
                return 0;

            var val = Math.Round(origin * ((Double)percent / (Double)100), 0);

            return val - (val % 5);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClubArcada.BusinessObjects;
using ClubArcada.BusinessObjects.DataClasses;

namespace ClubArcada.PokerTimer.Win.Dialogs
{
    public partial class ReBuyDlg : Window, INotifyPropertyChanged
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
            Sum,
            Stack
        }

        private void RefreshValues()
        {
            PropertyChange(Property.Stack);
            PropertyChange(Property.Sum);
        }

        # endregion

        public bool IsRebuyEnabled { get { return Tournament.GameTypeEnum.In(eGameType.FreeRoll, eGameType.RebuyLimited, eGameType.RebuyUnlimited); } }
        public bool IsAddOnEnabled { get { return Tournament.GameTypeEnum.In(eGameType.FreeRoll, eGameType.RebuyLimited, eGameType.RebuyUnlimited); } }

        public bool IsDoubleChanceEnabled { get { return Tournament.GameTypeEnum.In( eGameType.DoubleChance, eGameType.TripleChance); } }
        public bool IsTripleChanceEnabled { get { return Tournament.GameTypeEnum.In( eGameType.TripleChance); } }

        private bool _isSingleRebuy;
        public bool IsSingleRebuy
        {
            get
            {
                return _isSingleRebuy;
            }
            set
            {
                _isSingleRebuy = value;
                RefreshValues();
            }
        }

        private bool _isDoubleRebuy;
        public bool IsDoubleRebuy
        {
            get
            {
                return _isDoubleRebuy;
            }
            set
            {
                _isDoubleRebuy = value;
                RefreshValues();
            }
        }

        private bool _isAddOn;
        public bool IsAddOn
        {
            get
            {
                return _isAddOn;
            }
            set
            {
                _isAddOn = value;
                RefreshValues();
            }
        }
        
        public int Sum
        {
            get
            {
                int rebuyPrize = Tournament.TournamentDetail.ReBuyPrize;
                int addOnPrize = Tournament.TournamentDetail.AddOnPrize;

                if (IsRebuyEnabled)
                {
                    return (IsSingleRebuy ? rebuyPrize : 2 * rebuyPrize) + (IsAddOn ? addOnPrize : 0);
                }
                else if (IsDoubleChanceEnabled)
                {
                    return 999;
                }
                else
                {
                    return 0;
                }
            }
        }


        public TournamentResult TournamentResult { get; set; }

        private Tournament _tournament;
        private Tournament Tournament 
        {
            get
            {
                if (_tournament.IsNull())
                    _tournament = (Application.Current.MainWindow as MainWindow).Tournament;

                return _tournament;
            }
        }



        public ReBuyDlg(TournamentResult tournamentResult)
        {
            TournamentResult = tournamentResult;

            InitializeComponent();
            DataContext = this;
        }

        private void okBtn_click(object sender, RoutedEventArgs e)
        {
            if (IsAddOn)
                TournamentResult.AddOnCount++;

            if (IsSingleRebuy)
                TournamentResult.ReBuyCount++;

            if (IsDoubleRebuy)
            { 
                TournamentResult.ReBuyCount++;
                TournamentResult.ReBuyCount++;
            }

            this.Close();
        }

        private void closeBtn_click(object sender, RoutedEventArgs e)
        {

        }
    }
}

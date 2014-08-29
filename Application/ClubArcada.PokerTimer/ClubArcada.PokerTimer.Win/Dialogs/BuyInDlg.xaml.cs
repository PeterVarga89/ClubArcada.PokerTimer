using ClubArcada.BusinessObjects;
using ClubArcada.BusinessObjects.DataClasses;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;

namespace ClubArcada.PokerTimer.Win.Dialogs
{
    public partial class BuyInDlg : Window, INotifyPropertyChanged
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
            IsRebuy,
            IsDoubleChance,
            IsTripleChance,
            IsTimeBonus,
            TotalStack,
            IsBuyBonus,
            BuyInStack,
            ReBuyStack,
            AddOnStack,
            BonusStack,
            BuyOverBuyInStack,
            BuyBonusStack,
            IsTripleChanceEnabled,
            TotalSum
        }

        private void RefreshValues()
        {
            //PropertyChange(Property.IsTimeBonus);
            //PropertyChange(Property.IsBuyBonus);
            //PropertyChange(Property.BuyInStack);
            //PropertyChange(Property.ReBuyStack);
            //PropertyChange(Property.AddOnStack);
            //PropertyChange(Property.BonusStack);
            //PropertyChange(Property.BuyOverBuyInStack);
            //PropertyChange(Property.BuyBonusStack);

            PropertyChange(Property.TotalStack);

            if (!IsDoubleChance)
                IsTripleChance = IsDoubleChance;
        }

        # endregion

        public TournamentResult TournamentResult { get; set; }

        private Tournament Tournament { get; set; }

        public bool IsChangesEnabled { get { return Tournament.GameTypeEnum.In(eGameType.DoubleChance, eGameType.TripleChance); } }

        public bool IsTripleChanceEnabled { get { return IsChangesEnabled && IsDoubleChance; } }

        public bool IsReBuyEnabled { get { return Tournament.GameTypeEnum.In(eGameType.FreeRoll, eGameType.RebuyLimited, eGameType.RebuyUnlimited); } }

        private bool _isRebuy;
        public bool IsRebuy 
        {
            get { return _isRebuy; }
            set { _isRebuy = value; PropertyChange(Property.IsRebuy); PropertyChange(Property.TotalStack); PropertyChange(Property.TotalSum); }
        }

        private bool _isDoubleChance;
        public bool IsDoubleChance
        {
            get { return _isDoubleChance; }
            set 
            { 
                _isDoubleChance = value; 
                PropertyChange(Property.IsDoubleChance);
                PropertyChange(Property.IsTripleChance);
                PropertyChange(Property.IsTripleChanceEnabled);
                PropertyChange(Property.TotalStack);
                PropertyChange(Property.TotalSum);
            }
        }

        private bool _isTripleChance;
        public bool IsTripleChance
        {
            get { return _isTripleChance; }
            set 
            { 
                _isTripleChance = value; 
                PropertyChange(Property.IsTripleChance);
                PropertyChange(Property.TotalStack);
                PropertyChange(Property.TotalSum);
            }
        }

        private bool _isTimeBonus;
        public bool IsTimeBonus
        {
            get { return _isTimeBonus; }
            set { _isTimeBonus = value; PropertyChange(Property.IsTimeBonus); PropertyChange(Property.TotalStack); PropertyChange(Property.TotalSum); }
        }

        public bool IsBuyBonus { get { return IsRebuy || IsDoubleChance; } }

        public int BuyInStack { get { return Tournament.TournamentDetail.BuyInStack; } }
        public int ReBuyStack { get { return Tournament.TournamentDetail.ReBuyStack; } }
        public int AddOnStack { get { return Tournament.TournamentDetail.AddOnStack; } }
        public int BonusStack { get { return Tournament.TournamentDetail.BonusStack; } }


        public Double StackRaw
        {
            get
            {
                return BuyInStack + ((IsRebuy || IsDoubleChance) ? ReBuyStack : 0) + (IsTripleChance ? AddOnStack : 0);
            }
        }

        public Double StackBuyBonus
        {
            get
            {
                if (IsRebuy || IsDoubleChance)
                {
                    return StackRaw * 0.20;
                }
                else
                    return 0;
            }
        }

        public Double TotalStack
        {
            get
            {
                return StackRaw + StackBuyBonus + (IsTimeBonus ? BonusStack : 0);
            }
        }

        public Double StackBonusTotal
        {
            get
            {
                return StackBuyBonus + (IsTimeBonus ? BonusStack : 0);
            }
        }

        private int RebuyPrize { get { return (IsRebuy || IsDoubleChance) ? Tournament.TournamentDetail.ReBuyPrize : 0; } }
        private int AddOnPrize { get { return IsTripleChance ? Tournament.TournamentDetail.AddOnPrize : 0; } }
        public int TotalSum 
        { 
            get 
            { 
                return Tournament.TournamentDetail.BuyInPrize + RebuyPrize + AddOnPrize; 
            } 
        }

        public BuyInDlg(Tournament tournament)
        {
            Tournament = tournament.GetCopy();
            Tournament.LoadTournamentDetails();
            InitializeComponent();
            DataContext = this;
            BindTablesAndSits();
        }

        private void BindTablesAndSits()
        {
            MainWindow window = Application.Current.MainWindow as MainWindow;

            if (window.TableList.IsNull() || window.TableList.Count == 0)
            {
                window.TableList = new List<Tables>();
                window.TableList.Add(new Tables() { Number = 1 });
            }

            if (window.TableList.Last().GetAvaibleSitNumber() < 1)
            {
                window.TableList.Add(new Tables() { Number = window.TableList.Last().Number + 1 });
            }
            
            txtTable.Text = window.TableList.Last().Number.ToString();

            foreach (var t in window.TableList)
            {
                if (t.GetAvaibleSitNumber() > 0)
                {
                    var randomSit = t.GetRanomSit();

                    t.Sits.Add(new Sits() { Number = randomSit });
                    txtSit.Text = randomSit.ToString();
                }
            }
            
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;

            TournamentResult.ReBuyCount = IsRebuy || IsDoubleChance ? 1 : 0;
            TournamentResult.AddOnCount = IsTripleChance ? 1 : 0;
            TournamentResult.IsTimeBonus = IsTimeBonus;

            TournamentResult.BonusStackTotal = (int)StackBonusTotal;

            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtSit_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            
        }

        private void txtTable_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
        }
    }
}

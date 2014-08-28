using ClubArcada.BusinessObjects.DataClasses;
using ClubArcada.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ClubArcada.PokerTimer.Win.Dialogs
{
    public partial class PlayerListDialog : Window
    {
        public ObservableCollection<TournamentResult> PlayerList { get; set; }

        public PlayerListDialog(ObservableCollection<TournamentResult> playerList)
        {
            PlayerList = playerList;

            InitializeComponent();

            this.KeyUp += PlayerListDialog_KeyUp;

            lbPlayers.ItemsSource = PlayerList;
        }

        private void PlayerListDialog_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F3)
            {
                this.Close();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRebuyDown_Click(object sender, RoutedEventArgs e)
        {
            var id = (sender as Button).Tag.ToString().ToGuid();

            var result = PlayerList.SingleOrDefault(p => p.TournamentResultId == id);

            if (result.ReBuyCount > 0)
                result.ReBuyCount--;
        }
        private void btnRebuyUp_Click(object sender, RoutedEventArgs e)
        {
            var id = (sender as Button).Tag.ToString().ToGuid();

            var result = PlayerList.SingleOrDefault(p => p.TournamentResultId == id);

            var dlg = new Dialogs.ReBuyDlg(result);
            dlg.ShowDialog();
            this.Focus();
        }

        private void btnAddOnDown(object sender, RoutedEventArgs e)
        {
            var id = (sender as Button).Tag.ToString().ToGuid();

            var result = PlayerList.SingleOrDefault(p => p.TournamentResultId == id);

            if (result.AddOnCount > 0)
                result.AddOnCount--;
        }
        private void btnAddOnUp(object sender, RoutedEventArgs e)
        {
            var id = (sender as Button).Tag.ToString().ToGuid();

            var result = PlayerList.SingleOrDefault(p => p.TournamentResultId == id);
            if(result.AddOnCount == 0)
                result.AddOnCount++;
        }

        private void btnPokerDown(object sender, RoutedEventArgs e)
        {
            var id = (sender as Button).Tag.ToString().ToGuid();

            var result = PlayerList.SingleOrDefault(p => p.TournamentResultId == id);

            if (result.PokerCount > 0)
                result.PokerCount--;
        }
        private void btnPokerUp(object sender, RoutedEventArgs e)
        {
            var id = (sender as Button).Tag.ToString().ToGuid();

            var result = PlayerList.SingleOrDefault(p => p.TournamentResultId == id);

            result.PokerCount++; 
        }

        private void btnSFlushDown(object sender, RoutedEventArgs e)
        {
            var id = (sender as Button).Tag.ToString().ToGuid();

            var result = PlayerList.SingleOrDefault(p => p.TournamentResultId == id);

            if (result.StraightFlushCount > 0)
                result.StraightFlushCount--;
        }
        private void btnSFlushUp(object sender, RoutedEventArgs e)
        {
            var id = (sender as Button).Tag.ToString().ToGuid();

            var result = PlayerList.SingleOrDefault(p => p.TournamentResultId == id);

            result.StraightFlushCount++;
        }
        
        private void btnRFlushDown(object sender, RoutedEventArgs e)
        {
            var id = (sender as Button).Tag.ToString().ToGuid();

            var result = PlayerList.SingleOrDefault(p => p.TournamentResultId == id);

            if (result.RoyalFlushCount > 0)
                result.RoyalFlushCount--;
        }
        private void btnRFlushUp(object sender, RoutedEventArgs e)
        {
            var id = (sender as Button).Tag.ToString().ToGuid();

            var result = PlayerList.SingleOrDefault(p => p.TournamentResultId == id);

            result.RoyalFlushCount++;
        }
    }
}

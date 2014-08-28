using System;
using System.Collections.Generic;
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
using ClubArcada.BusinessObjects.DataClasses;
using ClubArcada.BusinessObjects;

namespace ClubArcada.PokerTimer.Win.Dialogs
{
    public partial class TournamentSettingsDlg : Window
    {
        public MainWindow MainWindow = Application.Current.MainWindow as MainWindow;

        public Tournament Tournament { get { return MainWindow.Tournament; } }
        public TournamentDetail Detail { get { return Tournament.TournamentDetail; } }

        public Double PrizePool { get { return MainWindow.PrizePool; } }

        public bool IsChanceVisible { get { return Tournament.GameTypeEnum.In(eGameType.DoubleChance, eGameType.TripleChance); } }

        public Double Dotation { get { return MainWindow.Dotation; } }
        public Double League { get { return MainWindow.LeagueMoney; } }
        public Double Rake { get { return MainWindow.RakeMoney; } }

        public Double MoneyPool { get { return MainWindow.MoneyPool; } }

        public int[] Places { get { return MainWindow.GetPlaces(); } }

        public TournamentSettingsDlg()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void closeBtn_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void okBtn_click(object sender, RoutedEventArgs e)
        {

        }
    }
}

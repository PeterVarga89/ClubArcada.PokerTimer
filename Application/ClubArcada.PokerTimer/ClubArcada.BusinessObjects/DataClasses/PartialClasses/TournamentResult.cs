using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubArcada.BusinessObjects.DataClasses
{
    public partial class TournamentResult
    {
        public User User { get; set; }

        public string FullDislpayName
        {
            get
            {
                return string.Format("{0} - {1} {2}", this.User.NickName, this.User.FirstName, this.User.LastName);
            }
        }

        public int BonusStackTotal { get; set; }
    }
}

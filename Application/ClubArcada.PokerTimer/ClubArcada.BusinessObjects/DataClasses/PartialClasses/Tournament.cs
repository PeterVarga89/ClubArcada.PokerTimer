using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubArcada.BusinessObjects.Data;
using ClubArcada.BusinessObjects;

namespace ClubArcada.BusinessObjects.DataClasses
{
    public partial class Tournament
    {
        public TournamentDetail TournamentDetail
        {
            get;
            set;
        }

        public void LoadTournamentDetails()
        {
            this.TournamentDetail = TournamentData.GetDetailById(this.TournamentId);
        }

        public eGameType GameTypeEnum
        {
            get
            {
                return this.GameType.ToGameType();
            }
        }

        public string DisplayDateName
        {
            get
            {
                if (this.Date.Date == DateTime.Today.Date)
                {
                    return string.Format("Dnes - {0}", this.Name);
                }
                else
                {
                    return string.Format("{0} - {1}", this.Date.ToString("dd.MM.yyyy"), this.Name);
                }
            }
        }

    }
}

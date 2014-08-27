using ClubArcada.BusinessObjects.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubArcada.BusinessObjects.Data
{
    public class TournamentData
    {
        public static List<Tournament> GetList(int take = 5)
        {
            using (var app = new PSDBDataContext(Settings.ConnectionString))
            {
                return app.Tournaments.Where(t => t.GameType != 'C').OrderByDescending(t => t.Date).Take(take).ToList();
            }
        }

        public static Tournament GetById(Guid id)
        {
            using (var app = new PSDBDataContext(Settings.ConnectionString))
            {
                return app.Tournaments.SingleOrDefault(u => u.TournamentId == id);
            }
        }

        public static TournamentDetail GetDetailById(Guid id)
        {
            using (var app = new PSDBDataContext(Settings.ConnectionString))
            {
                return app.TournamentDetails.SingleOrDefault(u => u.TournamentId == id);
            }
        }
    }
}

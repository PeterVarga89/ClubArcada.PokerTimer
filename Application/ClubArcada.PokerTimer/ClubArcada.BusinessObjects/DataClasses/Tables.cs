using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubArcada.BusinessObjects.DataClasses
{
    public class Tables
    {
        public int Number { get; set; }
        public List<Sits> Sits { get; set; }

        public int GetRanomSit()
        {
            if (Sits.IsNull())
                Sits = new List<Sits>();

            int[] positions = {1,2,3,4,5,6,7,8,9,10};

            var unavaibleSitNumber = Sits.Select(s => s.Number).ToArray();

            var list3 = positions.Except(unavaibleSitNumber).ToList();

            return list3.GetRandomItem();
        }

        public int GetAvaibleSitNumber()
        {
            if (Sits.IsNull())
                return 10;

            return 10 - Sits.Count;
        }
    }
}

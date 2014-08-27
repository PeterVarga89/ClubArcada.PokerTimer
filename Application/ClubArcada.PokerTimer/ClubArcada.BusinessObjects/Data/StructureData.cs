using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubArcada.BusinessObjects.DataClasses;

namespace ClubArcada.BusinessObjects.Data
{
    public class StructureData
    {
        public static List<Structure> GetList()
        {
            using (var appDC = new PSDBDataContext(Settings.ConnectionString))
            {
                return appDC.Structures.ToList();
            }
        }

        public static StructureDetail GetDetailsByStrucureId(Guid id)
        {
            using (var appDC = new PSDBDataContext(Settings.ConnectionString))
            {
                return appDC.StructureDetails.SingleOrDefault(st => st.StructureId == id);
            }
        }

        public static void FillDetails(ref Structure structure)
        {
            structure.Details = GetDetailsByStrucureId(structure.StructureId);
        }
    }
}

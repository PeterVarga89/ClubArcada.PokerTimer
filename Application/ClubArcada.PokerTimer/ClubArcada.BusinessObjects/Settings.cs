using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubArcada.BusinessObjects
{
    public class Settings
    {
        public static string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings[String.Concat("DB_", Environment.ToString())];
            }
        }

        public static eEnvironment Environment
        {
            get
            {
                string env = System.Configuration.ConfigurationManager.AppSettings["Environment"];

                switch (env)
                {
                    case "Development":
                        return eEnvironment.Development;
                    case "QA":
                        return eEnvironment.QA;
                    case "Live":
                        return eEnvironment.Live;
                    default:
                        throw new NotSupportedException();
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnitchNet_PROJECT_9_6_25.DATA.alerts_DAL;
using SnitchNet_PROJECT_9_6_25.DATA.people_DAL;
using SnitchNet_PROJECT_9_6_25.DATA.reports_DAL;

namespace SnitchNet_PROJECT_9_6_25
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //dal_people.add_people("John", "Doe", "2" , "reporter" , 0 ,0);
            //dal_people.add_people("John", "Doe", "1" , "reporter" , 0 ,0);
            //dal_reports.add_report(7, 6, "This is a test report for John Doe.");
            dal_alerts.add_alert(7, DateTime.Now, DateTime.UtcNow , "exenple");
        }
    }
}

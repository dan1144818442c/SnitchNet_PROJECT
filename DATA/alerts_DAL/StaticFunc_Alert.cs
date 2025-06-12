using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnitchNet_PROJECT_9_6_25.DATA.alerst_DAL;
using SnitchNet_PROJECT_9_6_25.models;

namespace SnitchNet_PROJECT_9_6_25.DATA.alerts_DAL
{
    static class StaticFunc_Alert
    {
        public static void PrintAllAlerts()
        {
            List<alerts> alertsList = dal_alerts.all_alert_list();

            if (alertsList.Count == 0)
            {
                Console.WriteLine("\n*** No alerts found. ***\n");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("=====================================");
            Console.WriteLine("           ALERTS SUMMARY            ");
            Console.WriteLine("=====================================");

            foreach (var alert in alertsList)
            {
                Console.WriteLine($"[Alert ID    ] {alert.id}");
                Console.WriteLine($"[Target ID   ] {alert.target_id}");
                Console.WriteLine($"[Window Start] {alert.WindowStart}");
                Console.WriteLine($"[Window End  ] {alert.WindowEnd}");
                Console.WriteLine($"[Reason      ] {alert.Reason}");
                Console.WriteLine($"[Created At  ] {alert.CreatedAt}");
                Console.WriteLine("-------------------------------------");
            }

            Console.WriteLine("End of alerts list.");
            Console.WriteLine("=====================================\n");
        }

    }
}

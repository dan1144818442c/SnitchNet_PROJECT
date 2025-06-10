using System;
using SnitchNet_PROJECT_9_6_25.models;

namespace SnitchNet_PROJECT_9_6_25.DATA.alerst_DAL
{
    internal class dal_alerts
    {
        public dal_alerts() { }

        public static void add_alert(int target_id, DateTime windowStart, DateTime windowEnd, string reason)
        {
            alerts new_alert = new alerts(target_id, windowStart, windowEnd, reason);

            string sql = $@"
                INSERT INTO Alerts (TargetId, WindowStart, WindowEnd, Reason, CreatedAt)
                VALUES ({new_alert.target_id}, 
                        '{new_alert.WindowStart:yyyy-MM-dd HH:mm:ss}', 
                        '{new_alert.WindowEnd:yyyy-MM-dd HH:mm:ss}', 
                        '{new_alert.Reason}', 
                        '{new_alert.CreatedAt:yyyy-MM-dd HH:mm:ss}');

                SELECT LAST_INSERT_ID();";

            var result = Main_DAL.Execute(sql);
            
        }
    }
}

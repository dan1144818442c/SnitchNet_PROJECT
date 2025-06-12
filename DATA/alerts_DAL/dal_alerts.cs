using System;
using System.Collections.Generic;
using System.Linq;
using SnitchNet_PROJECT_9_6_25.DATA.people_DAL;
using SnitchNet_PROJECT_9_6_25.DATA.reports_DAL;
using SnitchNet_PROJECT_9_6_25.models;
using static SnitchNet_PROJECT_9_6_25.DATA.reports_DAL.dal_reports;
namespace SnitchNet_PROJECT_9_6_25.DATA.alerst_DAL
{
    internal class dal_alerts
    {
        public dal_alerts() { }

        private static void add_alert(int target_id, DateTime windowStart, DateTime windowEnd, string reason)
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
        public static bool AlertExists_3_rep(int targetId, DateTime windowStart, DateTime windowEnd)
        {
            string sql = $@"
                SELECT COUNT(*) as AlertCount
                FROM alerts
                WHERE TargetId = {targetId}
                  AND WindowStart = '{windowStart:yyyy-MM-dd HH:mm:ss}'
                  AND WindowEnd = '{windowEnd:yyyy-MM-dd HH:mm:ss}'";

            var result = Main_DAL.Execute(sql);
            int count = Convert.ToInt32(result[0]["AlertCount"]);
            return count > 0;
        }
        public static void CheckAndCreateBurstAlert_for_3_rep(int targetId, DateTime submittedAt)
        {
            if (dal_reports.IsReportBurst(targetId, submittedAt))
            {
                DateTime windowStart = submittedAt.AddMinutes(-15);
                DateTime windowEnd = submittedAt;
                string reason = "Burst of 3 or more reports within 15 minutes";
                dal_people.update_status(targetId, "dangerous");

                if (!AlertExists_3_rep(targetId, windowStart, windowEnd))
                {
                    dal_alerts.add_alert(targetId, windowStart, windowEnd, reason);
                }
            }
        }
        public static bool AlertExists_dangerous(int targetId)
        {
            string sql = $@"
                SELECT COUNT(*) as AlertCount
                FROM alerts
                WHERE TargetId = {targetId}
                  AND Reason = 'Dangerous Status'"; // Assuming 'Dangerous Status' is the reason for dangerous alerts
            var result = Main_DAL.Execute(sql);
            int count = Convert.ToInt32(result[0]["AlertCount"]);
            return count > 0;
        }

        public static void chek_and_create_alert_for_type(int targetId)
        {
            if (dal_people.IS_status_dangerous(targetId))
                {
                if (!AlertExists_dangerous(targetId))
                {
                    DateTime now = DateTime.Now;
                    string reason = "Dangerous Status";
                    add_alert(targetId, now, now, reason);
                }
            }



        }

        public static List<alerts> all_alert_list()
        {
            List<alerts> all_alerts = new List<alerts>();
            string sql = "SELECT * FROM ALERTS";
            var result = Main_DAL.Execute(sql);

            foreach (var row in result)
            {
                int id = Convert.ToInt32(row["Id"]);
                int targetId = Convert.ToInt32(row["TargetId"]);
                DateTime windowStart = Convert.ToDateTime(row["WindowStart"]);
                DateTime windowEnd = Convert.ToDateTime(row["WindowEnd"]);
                string reason = row["Reason"].ToString();
                DateTime createdAt = Convert.ToDateTime(row["CreatedAt"]);

                alerts new_alert = new alerts(targetId, windowStart, windowEnd, reason)
                {
                    id = id,
                    CreatedAt = createdAt
                };

                all_alerts.Add(new_alert);
            }

            return all_alerts;
        }

    }





}



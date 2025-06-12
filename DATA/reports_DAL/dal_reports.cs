using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnitchNet_PROJECT_9_6_25.DATA.people_DAL;
using SnitchNet_PROJECT_9_6_25.models;
using SnitchNet_PROJECT_9_6_25.DATA.alerst_DAL;
namespace SnitchNet_PROJECT_9_6_25.DATA.reports_DAL
{
    internal class dal_reports
    {
        public dal_reports() { }
        public static void add_report(int reporterId, int targetId, string reportText)
        {
            try
            {
                reports new_report = new reports(reporterId, targetId, reportText);

                string sql = $@"INSERT INTO reports (ReporterId, TargetId, ReportText, SubmittedAt) 
                            VALUES 
                            ({new_report.ReporterId}, {new_report.TargetId}, '{new_report.ReportText}', '{new_report.SubmittedAt:yyyy-MM-dd HH:mm:ss}')";
                Main_DAL.Execute(sql);
                dal_alerts.CheckAndCreateBurstAlert_for_3_rep(targetId, new_report.SubmittedAt);
                dal_alerts.chek_and_create_alert_for_type(targetId);
                Logger.Log($"Report added successfully: {new_report.ReportText} by Reporter ID {reporterId} on Target ID {targetId} at {new_report.SubmittedAt}");
            }
            catch (Exception ex)
            {
                Logger.Log($"Error adding report: add report to DB");

            }

        }

        public static List<reports> get_reports()
        {
            try
            {
                string sql = "SELECT * FROM reports";
                var rows = Main_DAL.Execute(sql);
                List<reports> reportsList = new List<reports>();
                foreach (var row in rows)
                {
                    reports report = new reports(

                        Convert.ToInt32(row["ReporterId"]),
                        Convert.ToInt32(row["TargetId"]),
                        row["ReportText"].ToString()
                    )
                    {
                        id = Convert.ToInt32(row["Id"]),
                        SubmittedAt = Convert.ToDateTime(row["SubmittedAt"])
                    };
                    reportsList.Add(report);
                }
                Logger.Log("Reports retrieved successfully.");
                return reportsList;
            }
            catch (Exception ex)
            {
                Logger.Log($"Error retrieving reports: get all report to list");
                return new List<reports>();

            }
        }

        public static void delete_report(int reportId)
        {
            try
            {
                string sql = $"DELETE FROM reports WHERE id = {reportId}";
                Main_DAL.Execute(sql);
                Logger.Log($"Report with ID {reportId} deleted successfully.");
            }
            catch (Exception ex)
            {
                Logger.Log($"Error deleting report: delete report from DB");
            }
        }

        public static void UpdateReport(int reportId, string newReportText)
        {
            try
            {
                string sql = $@"UPDATE reports 
                           SET ReportText = '{newReportText}' 
                           WHERE id = {reportId}";
                Main_DAL.Execute(sql);
                Logger.Log($"Report with ID {reportId} updated successfully.");
            }
            catch (Exception ex)
            {
                Logger.Log($"Error updating report: update report in DB");
            }
        }

        public static void GetTextReport(People reporter, string report_text, string firstName, string lastName)
        {
            People target = new People();
            //(string firstName, string lastName) = StaticFunc.FindName_InText(report_text);

            StaticFunc.check_name_and_upload_DB(firstName, lastName, "target");

            target = dal_people.GetPeople_by_full_name_andCodeName(firstName, lastName);

            StaticFunc.check_name_and_upload_DB(reporter.FirstName, reporter.LastName, "report");


            People reporter_fromDB = dal_people.GetPeople_by_full_name_andCodeName(reporter.FirstName, reporter.LastName);


            dal_people.update_num_reports(reporter_fromDB.id);

            Console.WriteLine("update  reporter");

            add_report(reporter_fromDB.id, target.id, report_text);
            Console.WriteLine("add report to DB");
            dal_people.update_num_mentions(target.id);
            Console.WriteLine("update num mentions for target");



        }
        public static bool IsReportBurst(int targetId, DateTime currentTime)
        {
            try
            {
                string sql = $@"
                SELECT COUNT(*) as ReportCount
                FROM reports
                WHERE TargetId = {targetId}
                  AND SubmittedAt >= '{currentTime.AddMinutes(-15):yyyy-MM-dd HH:mm:ss}'
                  AND SubmittedAt <= '{currentTime:yyyy-MM-dd HH:mm:ss}'";

                var result = Main_DAL.Execute(sql);
                int count = Convert.ToInt32(result[0]["ReportCount"]);
                Logger.Log($"Checking report burst for Target ID {targetId} at {currentTime}: {count} reports found.");
                return count >= 3;


            }
            catch (Exception ex)
            {
                Logger.Log($"Error checking report burst: cant check if have 3 report");
                return false;
            }
        }



        public static List<reports> GetReportsByTargetId(int targetId)
        {
            try
            {
                string sql = $@"
                SELECT * 
                FROM reports 
                WHERE TargetId = {targetId}";
                var rows = Main_DAL.Execute(sql);
                List<reports> reportsList = new List<reports>();
                foreach (var row in rows)
                {
                    reports report = new reports(
                        Convert.ToInt32(row["ReporterId"]),
                        Convert.ToInt32(row["TargetId"]),
                        row["ReportText"].ToString()
                    )
                    {
                        id = Convert.ToInt32(row["id"]),
                        SubmittedAt = Convert.ToDateTime(row["SubmittedAt"])
                    };
                    reportsList.Add(report);
                }
                Logger.Log($"Reports for Target ID {targetId} retrieved successfully.");
                return reportsList;

            }
            catch (Exception ex)
            {
                Logger.Log($"Error retrieving reports by Target ID {targetId}: get all report to list");
                return new List<reports>();
            }


        }




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnitchNet_PROJECT_9_6_25.DATA.people_DAL;
using SnitchNet_PROJECT_9_6_25.models;
namespace SnitchNet_PROJECT_9_6_25.DATA.reports_DAL
{
    internal class dal_reports
    {
        public dal_reports() { }
        public static void add_report(int reporterId, int targetId, string reportText)
        {
            reports new_report = new reports(reporterId, targetId, reportText);
            string sql = $@"INSERT INTO reports (ReporterId, TargetId, ReportText, SubmittedAt) 
                            VALUES 
                            ({new_report.ReporterId}, {new_report.TargetId}, '{new_report.ReportText}', '{new_report.SubmittedAt:yyyy-MM-dd HH:mm:ss}')";
            Main_DAL.Execute(sql);
        }

        public static List<reports> get_reports()
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
                    id = Convert.ToInt32(row["id"]),
                    SubmittedAt = Convert.ToDateTime(row["SubmittedAt"])
                };
                reportsList.Add(report);
            }
            return reportsList;
        }

        public static void delete_report(int reportId)
        {
            string sql = $"DELETE FROM reports WHERE id = {reportId}";
            Main_DAL.Execute(sql);
        }

        public static void UpdateReport(int reportId, string newReportText)
        {
            string sql = $@"UPDATE reports 
                           SET ReportText = '{newReportText}' 
                           WHERE id = {reportId}";
            Main_DAL.Execute(sql);
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

    }
}

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

        public static void update_report(int reportId, string newReportText)
        {
            string sql = $@"UPDATE reports 
                           SET ReportText = '{newReportText}' 
                           WHERE id = {reportId}";
            Main_DAL.Execute(sql);
        }

        public static async Task get_txt_report(People reporter, string report_text)
        {
            People target  = new People();
            (string firstName, string lastName) = StaticFunc.FindName_InText(report_text);
            if (!(StaticFunc.check_name(firstName, lastName)))
            {
                Console.WriteLine($"enter secret code for {firstName + " " + lastName}");
                string secret_code = Console.ReadLine();
                dal_people.add_people(firstName, lastName, secret_code, "target");
                Console.WriteLine($"add  {firstName + " " + lastName} to DB ");
                 target = dal_people.GetPeople_by_full_name_andCodeName(firstName, lastName, secret_code);
            }
            if (target == null)
            {
                target = dal_people.GetPeople_by_full_name_andCodeName(firstName, lastName);
            }

            if (StaticFunc.check_have_id(reporter.id))
            {
                dal_people.update_num_reports(reporter.id);

            }
            else
            {
                dal_people.add_people(reporter.FirstName, reporter.LastName, reporter.secret_code, "reporter", 0, 1);
            }
            People reporter_with_id = dal_people.GetPeople_by_full_name_andCodeName(reporter.FirstName, reporter.LastName, reporter.secret_code);
            await Task.Delay(1000); // Simulate some delay for async operation

            add_report(reporter_with_id.id, target.id, report_text);
            dal_people.update_num_mentions(target.id);

            Console.WriteLine("hhhhhhhhhhhh");
            Console.ReadLine(); 

        }

    }
}

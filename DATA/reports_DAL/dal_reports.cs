using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}

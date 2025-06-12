using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnitchNet_PROJECT_9_6_25.models;

namespace SnitchNet_PROJECT_9_6_25.DATA.reports_DAL
{
    static class Staticfunc_report
    {
        public static void PrintReportsTable()
        {
            List<reports> reportsList = dal_reports.get_reports();

            if (reportsList == null || reportsList.Count == 0)
            {
                Console.WriteLine("No reports to display.");
                return;
            }

            Console.WriteLine(new string('-', 100));
            Console.WriteLine("{0,-5} | {1,-10} | {2,-10} | {3,-50} | {4,-20}", "ID", "ReporterId", "TargetId", "ReportText", "SubmittedAt");
            Console.WriteLine(new string('-', 100));

            foreach (var report in reportsList)
            {
                string reportText = report.ReportText.Length > 47 ? report.ReportText.Substring(0, 47) + "..." : report.ReportText;

                Console.WriteLine("{0,-5} | {1,-10} | {2,-10} | {3,-50} | {4,-20}",
                    report.id,
                    report.ReporterId,
                    report.TargetId,
                    reportText,
                    report.SubmittedAt.ToString("yyyy-MM-dd HH:mm"));
            }

            Console.WriteLine(new string('-', 100));
        }


    }
}

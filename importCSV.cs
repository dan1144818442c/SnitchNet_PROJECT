using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnitchNet_PROJECT_9_6_25.DATA.people_DAL;
using SnitchNet_PROJECT_9_6_25.DATA.reports_DAL;
using SnitchNet_PROJECT_9_6_25.DATA.alerst_DAL;
using SnitchNet_PROJECT_9_6_25.models;

namespace SnitchNet_PROJECT_9_6_25
{
    public static class importCSV
    {
        public static void ImportCsv()
        {
            Console.Write("CSV file path: ");
            var path = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path)) { Console.WriteLine("File not found.\n"); return; }
            int count = 0;
             var reader = new StreamReader(path);
            string header = reader.ReadLine();
            if (header == null) { Console.WriteLine("CSV is empty.\n"); return; }
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split(',');
                if (parts.Length < 4) continue;
                var reporter = parts[0];
                var target = parts[1];
                var text = parts[2];
                if (!DateTime.TryParse(parts[3], null, System.Globalization.DateTimeStyles.AssumeLocal, out var ts)) continue;
                if (string.IsNullOrWhiteSpace(reporter) || string.IsNullOrWhiteSpace(target) || string.IsNullOrWhiteSpace(text)) continue;
                if (reporter.Split(' ').Length < 2)
                {
                    continue;
                }
                string reporterFirstName = reporter.Split(' ')[0];
                string reporterLastName = reporter.Split(' ')[1];
                People reporterPerson = new People(reporterFirstName, reporterLastName, "1", "reporter", 0, 0);
                int reporterId = dal_people.add_object_people(reporterPerson);
                if (target.Split(' ').Length < 2)
                {
                    continue;
                }
                string targetFirstName = target.Split(' ')[0];
                string targetLastName = target.Split(' ')[1];
                People targetPerson = new People(targetFirstName, targetLastName, "1", "target", 0, 0);
                int targetId = dal_people.add_object_people(targetPerson);
                
                dal_reports.add_report(reporterId, targetId, text);
                count++;
                
                dal_alerts.add_alert(reporterId, ts, ts, text);
            }
            Logger.Log($"CSVImport: Imported {count} reports from {path}");
            Console.WriteLine($"Imported {count} reports.\n");
        }
    }
}

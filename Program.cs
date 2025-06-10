using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnitchNet_PROJECT_9_6_25.DATA.people_DAL;
using SnitchNet_PROJECT_9_6_25.DATA.reports_DAL;

namespace SnitchNet_PROJECT_9_6_25
{
    internal class Program
    {
        
            static async Task Main(string[] args)
            {


            //dal_people.add_people("John", "Doenew", "2125855" , "reporter" , 0 ,0);//good
            //dal_people.add_people("John", "Doe", "1" , "reporter" , 0 ,0);
            //dal_reports.add_report(7, 6, "This is a test report for John Doe.");
            //dal_alerts.add_alert(7, DateTime.Now, DateTime.UtcNow , "exenple");
            //dal_people.Person_Identification();
            //(string a , string b) = StaticFunc.FindName_InText("rfgtshydjf Dan Cogh wfjdgfghfjndknbjfn");
            //Console.WriteLine(a + " " + b);
            //dal_reports.GetTextReport(new People("Jhgtsgtggdtgtdpffn", "Doegfbggxfeel", "1548554515"), "This is a test report for Dangie Rotlev." , "Dafrgsdfggggbdgfdgnie" , "levigvbbcf");
            //importCSV.ImportCsv();f

            Menu.act_menu();
        }
    }
}

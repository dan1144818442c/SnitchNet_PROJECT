using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnitchNet_PROJECT_9_6_25.DATA.people_DAL;
using SnitchNet_PROJECT_9_6_25.DATA.reports_DAL;

namespace SnitchNet_PROJECT_9_6_25
{
    public static class Menu
    {
       

        public static void act_menu() {
            
            Console.WriteLine($@"chois :
1 - to sumbit report
2  - to import from CSV
3 -  to show secret code by full name
 4 - ");
        
           string chois = Console.ReadLine();
            switch  (chois)
            {
                case "1":
                    {
                        Menu.deteils_to_sumbit_report();
                        break;
                    }
                case "2":
                    {
                        Console.WriteLine("You chose to import from CSV.");
                        importCSV.ImportCsv();
                        break;
                    }
                case "3":
                    {
                        Console.WriteLine("You chose to show secret code by full name.");
                        Console.WriteLine("Enter first name:");
                        string firstName = Console.ReadLine();
                        Console.WriteLine("Enter last name:");
                        string lastName = Console.ReadLine();
                        People person = dal_people.GetPeople_by_full_name_andCodeName(firstName, lastName);
                        if (person != null)
                        {
                            Console.WriteLine($"Secret code for {firstName} {lastName} is: {person.secret_code}");
                        }
                        else
                        {
                            Console.WriteLine("Person not found.");
                        }
                        // Call the method to show secret code by full name
                        break;
                    }
                case "4":
                    {
                        Console.WriteLine("You chose option 4.");
                        // Implement option 4 functionality here
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid choice, please try again.");
                        act_menu(); // Recursively call the menu function for another attempt
                        break;
                    }
            }

        }

        public static void deteils_to_sumbit_report()
        {

            Console.WriteLine("You chose to submit a report.");
            Console.WriteLine("enter your detalis");
            People person = StaticFunc.add_people();

            Console.WriteLine("enter target detiels");
            string firstName, lastName;
            Console.WriteLine("Enter first name of the target:");
            firstName = Console.ReadLine();
            Console.WriteLine("Enter last name of the target:");
            lastName = Console.ReadLine();
            Console.WriteLine("enter text report");
            string report_text = Console.ReadLine();
            dal_reports.GetTextReport(person, report_text, firstName, lastName);

        }
    }
}

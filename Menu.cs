using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnitchNet_PROJECT_9_6_25.DATA.alerst_DAL;
using SnitchNet_PROJECT_9_6_25.DATA.alerts_DAL;
using SnitchNet_PROJECT_9_6_25.DATA.people_DAL;
using SnitchNet_PROJECT_9_6_25.DATA.reports_DAL;


namespace SnitchNet_PROJECT_9_6_25
{
    public static class Menu
    {
       

        public static void act_menu()
        {
            string choice = "act";
            while (choice != "5")
            {
                try
                {
                    print_menu();
                    choice =main_menu();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine("Please try again.");
                }
            }

             }
        public static void print_menu()
        {
            Console.WriteLine("******************************");
            Console.WriteLine("*     Welcome to SnitchNet   *");
            Console.WriteLine("******************************");
            Console.WriteLine("* 1. Submit a report         *");
            Console.WriteLine("* 2. Import from CSV         *");
            Console.WriteLine("* 3. Show secret code        *");
            Console.WriteLine("* 4. Show data               *");
            Console.WriteLine("* 5. Exit                    *");
            Console.WriteLine("******************************");
            Console.Write("Enter your choice (1-5): ");
        }



        public static string main_menu() { 

        
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
                        show_menu(); // Call the method to show the menu
                        break;
                    }
                case "5":
                    {
                        Console.WriteLine("Exiting the application. Goodbye!");
                         // Exit the application
                         return chois; // Return the choice to exit the loop

                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid choice, please try again.");
                         // Recursively call the menu function for another attempt
                        break;
                    }
                    // Return the choice to exit the loop
            }
            return chois;

        }

        public static void show_menu()
        {
            string choice = "";
            while (choice != "4")
            {
                Console.WriteLine("******************************");
                Console.WriteLine("*          SHOW MENU          *");
                Console.WriteLine("******************************");
                Console.WriteLine("* 1. Show all people          *");
                Console.WriteLine("* 2. Show all reports         *");
                Console.WriteLine("* 3. Show worthy reporters    *");
                Console.WriteLine("* 4. Show all alerts          *");
                Console.WriteLine("* 5. Back to Main Menu        *");
                Console.WriteLine("******************************");
                Console.Write("Enter your choice: ");

                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        StaticFunc.PrintPeopleTable(); // הדפסת כל האנשים
                        break;
                    case "2":
                        Staticfunc_report.PrintReportsTable(); // הדפסת כל הדיווחים
                        break;
                    case "3":
                        dal_people.get_all_Recruit_Worthy_Reporters(); // מדפיס מדווחים ראויים
                        break;
                    case "4":
                        StaticFunc_Alert.PrintAllAlerts();
                        break;
                    case "5":
                        Console.WriteLine("Returning to Main Menu...");
                        return; // חזרה לתפריט הראשי
                        
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
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



        public static class Screen
        {
            public static void ShowTitle(string title)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
               
                Console.WriteLine("\n\n===============================");
                Console.WriteLine("        " + title.ToUpper());
                Console.WriteLine("===============================\n");
                Console.ResetColor();
            }

            public static void ShowMessage(string message)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(message);
                Console.ResetColor();
            }

            public static void ShowError(string error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: " + error);
                Console.ResetColor();
            }

            public static void ShowWarning(string warning)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Warning: " + warning);
                Console.ResetColor();
            }
        }
    }

}


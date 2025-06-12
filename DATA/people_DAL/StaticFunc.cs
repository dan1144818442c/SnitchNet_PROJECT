using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SnitchNet_PROJECT_9_6_25.DATA.people_DAL
{
    static class StaticFunc
    {
        public static bool check_name(string first_name, string last_name)
        {
            List<People> peopleList = dal_people.get_people();
            bool exists = false;
            foreach (People person in peopleList)
            {
                if (person.FirstName.Equals(first_name, StringComparison.OrdinalIgnoreCase) &&
                    person.LastName.Equals(last_name, StringComparison.OrdinalIgnoreCase))
                {
                    exists = true;
                    break;
                }

            }
            if (exists)
            {
                return true;
            }
            return false;
        }


        public static void check_name_and_upload_DB(string firstName, string lastName, string type)
        {
            if (!(StaticFunc.check_name(firstName, lastName)))
            {
                Console.WriteLine($"Enter good decret code to {firstName} {lastName} ");
                string secret_code = StaticFunc.get_good_secret_code();
                dal_people.add_people(firstName, lastName, secret_code, type);
                Console.WriteLine($"add  {firstName + " " + lastName} to DB ");
            }
        }

        public static bool check_secet_code(string secret_code)
        {
            List<People> peopleList = dal_people.get_people();
            bool exists = false;
            foreach (People person in peopleList)
            {
                if (person.secret_code.Equals(secret_code, StringComparison.OrdinalIgnoreCase))
                {
                    exists = true;
                    break;
                }
            }
            if (exists)
            {
                return true;
            }
            return false;
        }

        public static (string firstName, string lastName) FindName_InText(string text)
        {

            Regex regex = new Regex(@"\b[A-Z][a-z]+\s[A-Z][a-z]+\b");
            Match match = regex.Match(text);

            if (match.Success)
            {
                string fullName = match.Value;
                string[] parts = fullName.Split(' ');
                return (parts[0], parts[1]);
            }
            else
            {
                return ("", "");
            }

        }

        public static bool check_have_id(int id)
        {
            List<People> peopleList = dal_people.get_people();
            bool exists = false;
            foreach (People person in peopleList)
            {
                if (person.id == id)
                {
                    exists = true;
                    break;
                }
            }
            if (exists)
            {
                return true;
            }
            Console.WriteLine("This ID does not exist in the database.");
            return false;
        }

        public static string get_good_secret_code(string secret_code = null)
        {
            if (StaticFunc.check_secet_code(secret_code) || secret_code == null)
            {
                do
                {
                    Console.WriteLine("need to enter another secret code");
                    secret_code = Console.ReadLine();
                }
                while ((StaticFunc.check_secet_code(secret_code)));
            }

            return secret_code;
        }

        public static People add_people()
        {
            string firstName, lastName, secret_code;
            Console.WriteLine("Enter first name:");
            firstName = Console.ReadLine();
            Console.WriteLine("Enter last name:");
            lastName = Console.ReadLine();
            Console.WriteLine("Enter secret code:");
            secret_code = Console.ReadLine();
            People people = new People(firstName, lastName, secret_code);
            return people;


        }
        public static void PrintPeopleTable()
        {
            List<People> peopleList = dal_people.get_people();
            if (peopleList == null || peopleList.Count == 0)
            {
                Console.WriteLine("No people to display.");
                return;
            }

            Console.WriteLine(new string('-', 150));
            Console.WriteLine(
                "{0,-5} | {1,-15} | {2,-15} | {3,-12} | {4,-10} | {5,-12} | {6,-12} | {7,-20} | {8,-10}",
                "ID", "First Name", "Last Name", "Secret Code", "Type", "Mentions", "Reports", "Created At", "Status"
            );
            Console.WriteLine(new string('-', 150));

            foreach (var person in peopleList)
            {
                Console.Write(
                    "{0,-5} | {1,-15} | {2,-15} | {3,-12} | ",
                    person.id,
                    Truncate(person.FirstName, 15),
                    Truncate(person.LastName, 15),
                    Truncate(person.secret_code, 12)
                );

                // הדפסת Type בצבע מתאים
                switch (person.type.ToLower())
                {
                    case "reporter":
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case "both":
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                }
                Console.Write("{0,-10}", Truncate(person.type, 10));
                Console.ResetColor();

                // המשך שאר העמודות
                Console.Write(
                    " | {0,-12} | {1,-12} | {2,-20} | ",
                    person.num_mentions,
                    person.num_reports,
                    person.CreatedAt.ToString("yyyy-MM-dd HH:mm")
                );

                // הדפסת Status בצבע מתאים
                if (person.status_ == "dangerous")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.WriteLine("{0,-10}", Truncate(person.status_, 10));
                Console.ResetColor();
            }

            Console.WriteLine(new string('-', 150));
        }

        public static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength - 3) + "...";
        }


    }
}


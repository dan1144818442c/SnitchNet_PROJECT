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

        public  static void check_name_and_upload_DB(string firstName , string lastName  , string type )
        {
            if (!(StaticFunc.check_name(firstName, lastName)))
            {
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

        public static string get_good_secret_code(string secret_code = null) {
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

    }
}


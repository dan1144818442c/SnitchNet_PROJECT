using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SnitchNet_PROJECT_9_6_25.DATA.people_DAL
{
    // Renamed the class to avoid conflict with another definition of 'StaticFunc' in the same namespace.  
    static class StaticFuncHelper
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
            return exists;
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
            return exists;
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
            return exists;
        }
    }
}

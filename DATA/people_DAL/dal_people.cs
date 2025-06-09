using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnitchNet_PROJECT_9_6_25.DATA.people_DAL
{
    internal class dal_people
    {
        public dal_people() { }

        public static void add_people(string firstName, string lastName, string secret_code, string type, int num_mentions, int num_reports)
        {
            People new_people = new People(firstName, lastName, secret_code, type, num_mentions, num_reports);
            string sql = $@"
INSERT INTO people 
(FirstName, LastName, secret_code, type, num_mentions, num_reports, CreatedAt) 
VALUES 
('{new_people.FirstName}', '{new_people.LastName}', '{new_people.secret_code}', 
'{new_people.type}', {new_people.num_mentions}, {new_people.num_reports}, 
'{new_people.CreatedAt:yyyy-MM-dd HH:mm:ss}')";

            Main_DAL.Execute(sql);

        }

        public static List<Dictionary<string, object>> get_people()
        {
            string sql = "SELECT * FROM people";
            return Main_DAL.Execute(sql);
        }
        public static Dictionary<string, object> get_people_by_id(int id)
        {
            string sql = $"SELECT * FROM people WHERE id = {id}";
            var result = Main_DAL.Execute(sql);
            return result.FirstOrDefault();
        }


    }

}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Mysqlx;

namespace SnitchNet_PROJECT_9_6_25.DATA.people_DAL
{
    internal class dal_people
    {
        public dal_people() { }

        public static int add_people(string firstName, string lastName, string secret_code, string type = "clean", int num_mentions = 0, int num_reports = 0)
        {

            try
            {
                if (StaticFunc.check_name(firstName, lastName))
                {
                    Console.WriteLine("This person already exists in the database.");
                    return -1; // or throw an exception
                }
                secret_code = StaticFunc.get_good_secret_code(secret_code);
                People new_people = new People(firstName, lastName, secret_code, type, num_mentions, num_reports);

                string sql = $@"
INSERT INTO people 
(FirstName, LastName, secret_code, type, num_mentions, num_reports, CreatedAt) 
VALUES 
('{new_people.FirstName}', '{new_people.LastName}', '{new_people.secret_code}', 
'{new_people.type}', {new_people.num_mentions}, {new_people.num_reports}, 
'{new_people.CreatedAt:yyyy-MM-dd HH:mm:ss}')";

                Main_DAL.Execute(sql);

                People people = GetPeople_by_full_name_andCodeName(new_people.FirstName, new_people.LastName);
                Logger.Log($"add {people.FirstName} {people.LastName} to DB");

                return people.id;

            }
            catch (Exception ex)
            {
                Logger.Log("Error updating report: " + "add {people.FirstName} {people.LastName} to DB");
                return -1;
            }

        }

        public static int add_object_people(People new_people)
        {
            if (StaticFunc.check_name(new_people.FirstName, new_people.LastName))
            {
                Console.WriteLine("This person already exists in the database.");
                return -1; // or throw an exception

            }
            new_people.secret_code =  StaticFunc.get_good_secret_code(new_people.secret_code);
            string sql = $@"
INSERT INTO people 
(FirstName, LastName, secret_code, type, num_mentions, num_reports, CreatedAt) 
VALUES 
('{new_people.FirstName}', '{new_people.LastName}', '{new_people.secret_code}', 
'{new_people.type}', {new_people.num_mentions}, {new_people.num_reports}, 
'{new_people.CreatedAt:yyyy-MM-dd HH:mm:ss}')";
            Main_DAL.Execute(sql);

            People people1 = GetPeople_by_full_name_andCodeName(new_people.FirstName, new_people.LastName, new_people.secret_code);
            return people1.id;

        }

        public static void update_status(int id, string newStatus)
        {
            string sql = $@"UPDATE people 
                            SET status_ = '{newStatus}' 
                            WHERE id = {id}";
            Main_DAL.Execute(sql);
        }

        public static void Person_Identification()
        {


            Console.WriteLine("Enter First Name:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter Last Name:");
            string lastName = Console.ReadLine();
            if (StaticFunc.check_name(firstName, lastName))
            {
                Console.WriteLine("This person already exists in the database.");
                return;
            }

            Console.WriteLine("Enter Secret Code:");
            string secret_code = Console.ReadLine();
            do
            {
                if (StaticFunc.check_secet_code(secret_code))
                {
                    Console.WriteLine("This secret code already exists. Please enter a different code:");
                    secret_code = Console.ReadLine();
                }
                else
                {
                    break;
                }
            } while (true);

            Console.WriteLine("add to DB");
            add_people(firstName, lastName, secret_code);

        }

        public static List<People> get_people()
        {
            string sql = "SELECT * FROM people";
            List<Dictionary<string, object>> result = Main_DAL.Execute(sql);
            List<People> peopleList = new List<People>();
            foreach (var row in result)
            {
                People person = new People
                {
                    id = Convert.ToInt32(row["Id"]),
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    secret_code = row["secret_code"].ToString(),
                    type = row["type"].ToString(),
                    num_mentions = Convert.ToInt32(row["num_mentions"]),
                    num_reports = Convert.ToInt32(row["num_reports"]),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                    status_ = row.ContainsKey("status_") ? row["status_"].ToString() : "unknown"
                };
                peopleList.Add(person);
            }
            return peopleList;

        }
        public static People GetPeople_by_id(int id)
        {
            string sql = $"SELECT * FROM people WHERE id = {id}";
            var result = Main_DAL.Execute(sql);
            if (result.Count > 0)
            {
                var row = result[0];
                return new People
                {
                    id = Convert.ToInt32(row["Id"]),
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    secret_code = row["secret_code"].ToString(),
                    type = row["type"].ToString(),
                    num_mentions = Convert.ToInt32(row["num_mentions"]),
                    num_reports = Convert.ToInt32(row["num_reports"]),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"])
                };
            }
            return null;
        }

        public static void delete_people(int id)
        {
            string sql = $"DELETE FROM people WHERE id = {id}";
            Main_DAL.Execute(sql);
        }

        public static People GetPeople_by_full_name_andCodeName(string firstname, string last_name, string code_name = null)
        {
            string sql;



            sql = $@"
        SELECT * FROM people 
        WHERE FirstName = '{firstname}' 
          AND ( LastName= '{last_name}')
          ";


            var result = Main_DAL.Execute(sql);
            if (result.Count > 0)
            {
                var row = result[0];
                return new People
                {
                    id = Convert.ToInt32(row["Id"]),
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    secret_code = row["secret_code"].ToString(),
                    type = row["type"].ToString(),
                    num_mentions = Convert.ToInt32(row["num_mentions"]),
                    num_reports = Convert.ToInt32(row["num_reports"]),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"])
                };
            }
            Console.WriteLine("no find in DB");
            return null;
        }


        public static void update_Type(int id, string newType)
        {
            string sql = $@"UPDATE people 
                            SET type = '{newType}' 
                            WHERE id = {id}";
            Main_DAL.Execute(sql);
        }
        public static void update_num_mentions(int id)
        {
            People people = GetPeople_by_id(id);

            int new_num_mentions = people.num_mentions + 1;
            string sqlMentions = $@"UPDATE people
                           SET num_mentions = {new_num_mentions}
                           WHERE id = {id}";
            Main_DAL.Execute(sqlMentions);


            people = GetPeople_by_id(id);

            string current_type = people.type;
            string new_calculated_type = current_type;

            if (people.num_mentions > 0 && people.num_reports > 0)
            {
                new_calculated_type = "both";
            }
            else if (people.num_mentions > 0 && people.num_reports == 0)
            {
                new_calculated_type = "target";
            }
            else if (people.num_mentions == 0 && people.num_reports > 0)
            {
                new_calculated_type = "reporter";
            }
            else
            {
                new_calculated_type = "clear";
            }
            if (new_num_mentions >= 20)
            {
                update_status(id, "dangerous");
            }
            if (new_calculated_type != current_type)
            {
                update_Type(id, new_calculated_type);
            }
        }

        public static void update_num_reports(int id)
        {
            People people = GetPeople_by_id(id);

            int new_num_reports = people.num_reports + 1;
            string sqlReports = $@"UPDATE people
                           SET num_reports = {new_num_reports}
                           WHERE id = {id}";
            Main_DAL.Execute(sqlReports);

            people = GetPeople_by_id(id);

            string current_type = people.type;
            string new_calculated_type = current_type;

            if (people.num_mentions > 0 && people.num_reports > 0)
            {
                new_calculated_type = "both";
            }
            else if (people.num_mentions > 0 && people.num_reports == 0)
            {
                new_calculated_type = "target";
            }
            else if (people.num_mentions == 0 && people.num_reports > 0)
            {
                new_calculated_type = "reporter";
            }
            else
            {
                new_calculated_type = "clear";
            }

            if (new_calculated_type != current_type)
            {
                update_Type(id, new_calculated_type);
            }
        }

        public static bool IS_status_dangerous(int id)
        {
            string status = check_status_(id);
            return status == "dangerous";

        }

        public static string check_status_(int id)
        {
            string sql = $@"
                SELECT status_ 
                FROM people 
                WHERE id = {id}";
            var result = Main_DAL.Execute(sql);
            if (result.Count > 0)
            {
                return result[0]["status_"].ToString();
            }
            return "Unknown";
        }

        public static void get_all_Recruit_Worthy_Reporters() {
            string sql = @"SELECT ReporterId,
       COUNT(*) AS report_count,
       AVG(CHAR_LENGTH(ReportText)) AS avg_length
FROM reports
GROUP BY ReporterId
HAVING COUNT(*) >= 7 AND avg_length >= 10
";

            List< Dictionary<string, object> >  resulot = Main_DAL.Execute(sql);
            Dictionary<int , int> dict_id_num_rep = new Dictionary<int, int>();

            foreach (var row in resulot)
            {

                dict_id_num_rep[(Convert.ToInt32(row["ReporterId"]))] = Convert.ToInt32(row["report_count"]);
            }
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine($"{"ID",-5} | {"First Name",-15} | {"Last Name",-15} | {"Reports",-8}");
            Console.WriteLine("---------------------------------------------------------------");

            foreach (KeyValuePair<int, int> pair in dict_id_num_rep)
            {
                People p = dal_people.GetPeople_by_id(pair.Key);
                Console.WriteLine($"{p.id,-5} | {p.FirstName,-15} | {p.LastName,-15} | {pair.Value,-8}");
            }

            Console.WriteLine("---------------------------------------------------------------");


        }




        //public static void get_all_people_with_detiles()
        //{
        //    string sql = "SELECT people.* , "
        ////}
    }

}

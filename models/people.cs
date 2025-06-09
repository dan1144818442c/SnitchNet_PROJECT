using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnitchNet_PROJECT_9_6_25
{
    internal class People
    {
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string secret_code { get; set; }
        public string type { get; set; }
        public int num_mentions { get; set; }
        public int num_reports { get; set; }
        public DateTime CreatedAt { get; set; }

        public People( string firstName, string lastName, string secret_code, string type, int num_mentions, int num_reports)
        {
            
            this.FirstName = firstName;
            this.LastName = lastName;
            this.secret_code = secret_code;
            this.type = type;
            this.num_mentions = num_mentions;
            this.num_reports = num_reports;
            this.CreatedAt = DateTime.Now;

        }



    }
}

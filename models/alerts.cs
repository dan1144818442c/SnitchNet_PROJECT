using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnitchNet_PROJECT_9_6_25.models
{
    internal class alerts
    { 
        public int id { get; set; }
        public int target_id { get; set; }
        public DateTime WindowStart { get; set; }
        public DateTime WindowEnd { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedAt { get; set; }
        public alerts(int target_id, DateTime windowStart, DateTime windowEnd, string reason)
        {
            
            this.target_id = target_id;
            this.WindowStart = windowStart;
            this.WindowEnd = windowEnd;
            this.Reason = reason;
            this.CreatedAt = DateTime.Now;
        }

    }
}

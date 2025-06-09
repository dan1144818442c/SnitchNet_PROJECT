using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnitchNet_PROJECT_9_6_25.models
{
    internal class reports
    {
        public int id { get; set; }
      public int ReporterId { get; set; }
        public int  TargetId { get; set; }
        public string ReportText { get; set; }
        public DateTime SubmittedAt { get; set; }
        public reports(int reporterId, int targetId, string reportText )
        {
            
            this.ReporterId = reporterId;
            this.TargetId= targetId;
            this.ReportText = reportText;
            this.SubmittedAt = DateTime.Now;
        }

    }
}

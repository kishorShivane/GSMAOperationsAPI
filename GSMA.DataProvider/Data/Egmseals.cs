using System;
using System.Collections.Generic;

namespace GSMA.DataProvider.Data
{
    public partial class Egmseals
    {
        public Egmseals()
        {
            InverseSeal = new HashSet<Egmseals>();
        }

        public int Id { get; set; }
        public int Egmid { get; set; }
        public int SealId { get; set; }
        public DateTime Captureddatetime { get; set; }
        public DateTime? JobCompleateDateTime { get; set; }
        public int CapturedUserId { get; set; }
        public int AssaignedUserId { get; set; }

        public virtual User AssaignedUser { get; set; }
        public virtual User CapturedUser { get; set; }
        public virtual Egmdetails Egm { get; set; }
        public virtual Egmseals Seal { get; set; }
        public virtual ICollection<Egmseals> InverseSeal { get; set; }
    }
}

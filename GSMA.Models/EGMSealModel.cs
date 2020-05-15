using System;
using System.Collections.Generic;
using System.Text;

namespace GSMA.Models
{
    public class EGMSealModel
    {
        public int Id { get; set; }
        public int Egmid { get; set; }
        public int SealId { get; set; }
        public DateTime? Captureddatetime { get; set; }
        public DateTime? JobCompleateDateTime { get; set; }
        public int CapturedUserId { get; set; }
        public int AssaignedUserId { get; set; }
    }
}

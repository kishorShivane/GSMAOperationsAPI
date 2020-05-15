using System;
using System.Collections.Generic;
using System.Text;

namespace GSMA.Models
{
    public class EGMDetailModel
    {
        public int Id { get; set; }
        public int Egmid { get; set; }
        public string EgmserialNumber { get; set; }
        public string Egmname { get; set; }
        public int PortNumber { get; set; }
    }
}

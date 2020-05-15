using System;
using System.Collections.Generic;

namespace GSMA.DataProvider.Data
{
    public partial class Egmdetails
    {
        public Egmdetails()
        {
            Egmseals = new HashSet<Egmseals>();
        }

        public int Id { get; set; }
        public int Egmid { get; set; }
        public string EgmserialNumber { get; set; }
        public string Egmname { get; set; }
        public int PortNumber { get; set; }

        public virtual ICollection<Egmseals> Egmseals { get; set; }
    }
}

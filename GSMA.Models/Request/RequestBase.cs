using System;
using System.Collections.Generic;
using System.Text;

namespace GSMA.Models.Request
{
    public class RequestBase
    {
        public RequestBase()
        {
            TrackingInformation = Guid.NewGuid();
        }
        public Guid TrackingInformation { get; set; }
    }
}

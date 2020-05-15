using System;
using System.Collections.Generic;
using System.Text;

namespace GSMA.Models.Request
{
    public class Request<T>:RequestBase
    {
        public T Entity { get; set; }
    }
}

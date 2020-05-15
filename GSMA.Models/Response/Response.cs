using System;
using System.Collections.Generic;
using System.Text;

namespace GSMA.Models.Response
{
    public class Response<T> : ResponseBase
    {
        public T ResultSet { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
    public class ResponseModel
    {
        public bool isSuccess { get; set; }
        public String Message { get; set; }
        public Object Result { get; set; }
    }
}

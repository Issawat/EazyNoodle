using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EznManager
{
    public class Order_Details
    {
        public string id
        {
            get;
            set;
        }
        public string MachineID
        {
            get;
            set;
        }
        public string OrderDescription
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }
        public int OrderID
        {
            get;
            set;
        }

    }
}

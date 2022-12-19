using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_Advanced02_Restoranas.Models.receipt
{
    internal class CustomerReceipt : IReceipt
    {
        private Order order;

        public CustomerReceipt(Order order)
        {
            this.order = order;
        }

        public string GetBody()
        {
            return @"";
        }
    }
}

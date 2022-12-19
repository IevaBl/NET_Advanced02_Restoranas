using NET_Advanced02_Restoranas.Models.receipt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_Advanced02_Restoranas.Services
{
    internal class EmailSender
    {

        public void SendReceipt(String receiver, IReceipt receipt)
        {

            string body = receipt.GetBody();


        }

    }
}

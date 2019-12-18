using System;
using System.Collections.Generic;
using System.Text;

namespace MigratorModule.Console.Models
{
    public class TransferData
    {
        public ClientData ClientData { get; set; }
        public List<AccountData> AccountDataList { get; set; }
        public List<ClientAccountData> ClientAccountDataList { get; set; }
        public AddressData AddressData { get; set; }
    }
}

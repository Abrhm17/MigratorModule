using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Dapper.Contrib.Extensions;

namespace MigratorModule.Console.Models
{
    [Table("ClientAccount")]
    public class ClientAccountData
    {
        [Key]
        public int ClientAccountRef { get; set; }
        public int ClientRef { get; set; }
        public int AccountRef { get; set; }
        public decimal Ownership { get; set; }
    }
}

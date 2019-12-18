using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Dapper.Contrib.Extensions;

namespace MigratorModule.Console.Models
{
    [Table("Client")]
    public class ClientData
    {
        [Key]
        public int ClientRef { get; set; }
        [Write(false)]
        public int OldClientRef { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string SortName { get; set; }
        public DateTime? CalculationDate { get; set; }
        public int CompanyRef { get; set; }
    }
}

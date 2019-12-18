using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Dapper.Contrib.Extensions;

namespace MigratorModule.Console.Models
{
    [Table("Addresses")]
    public class AddressData
    {
        [Key]
        public int AddressRef { get; set; }
        public int? ClientRef { get; set; }
        public string AddressName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Postcode { get; set; }
    }
}

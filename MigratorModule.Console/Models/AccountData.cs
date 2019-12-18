using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Dapper.Contrib.Extensions;

namespace MigratorModule.Console.Models
{
    [Table("Account")]
    public class AccountData
    {
        [Key]
        public int AccountRef { get; set; }
        public string AccountId { get; set; }
        [Write(false)]
        public int OldAccountRef { get; set; }
        public string AccountName { get; set; }
        public int CompanyRef { get; set; }
        public bool IsCashAccount { get; set; }
    }
}

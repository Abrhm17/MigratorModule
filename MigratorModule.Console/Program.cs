using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CommandLine;
using Dapper;
using Dapper.Contrib.Extensions;
using MigratorModule.Console.Models;

namespace MigratorModule.Console
{
    class Program
    {
        public static SqlConnectionStringBuilder connectionBuilder = new SqlConnectionStringBuilder();

        static void Main(string[] args)
        {
            CommandLineOptions options = null;
            Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed<CommandLineOptions>(o => options = o);

            if (options == null)
            {
                System.Console.WriteLine("No valid options found");
                System.Console.ReadLine();
                return;
            }
            var clientRef = options.ClientRef;
            int? companyRef = options.CompanyRef;

            LoggingService logger = new LoggingService();
            var transferService = new TransferService(options, logger);
            var clientdata = new ClientData();
            clientdata.OldClientRef = clientRef;
            var transferData = new TransferData
            {
                ClientData = transferService.GetClientDataFromSource(clientRef),
                AccountDataList = transferService.GetAccountDataFromSource(clientRef).ToList(),
                ClientAccountDataList = transferService.GetClientAccountDataFromSource(clientRef).ToList(),
                AddressData = transferService.GetAddressDataFromSource(clientRef)
            };
            if (companyRef != null)
            {
                UpdateCompanyRef(transferData, (int)companyRef);
            }

           transferService.Execute(transferData);

          System.Console.ReadLine();
        }

        private static void UpdateCompanyRef(TransferData transferData, int newCompanyRef)
        {
            transferData.ClientData.CompanyRef = newCompanyRef;
            transferData.AccountDataList.ForEach(o => o.CompanyRef = newCompanyRef);
        }
    }

    public class CommandLineOptions
    {
        [Option("fromServer", Required = true, HelpText = "The Server you want to get the Client data from")]
        public string FromServer { get; set; }
        [Option("fromDb", Required = true, HelpText = "The database on the server you want to get the Client data from")]
        public string FromDatabase { get; set; }
        [Option("toServer", Required = true, HelpText = "The Server you want to move the client data to")]
        public string ToServer { get; set; }
        [Option("toDb", Required = true, HelpText = "The database on the server you want to move the client data to")]
        public string ToDatabase { get; set; }
        [Option('c', "clientRef", Required = true, HelpText = "The client you want to move")]
        public int ClientRef { get; set; }
        [Option('c', "companyRef", Required = true, HelpText = "Overrides company ref to this value")]
        public int? CompanyRef { get; set; }

    }
}

using System;
using System.Text;
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
    public class TransferService
    {
        public SqlConnectionStringBuilder fromConnection = new SqlConnectionStringBuilder();
        public SqlConnectionStringBuilder toConnection = new SqlConnectionStringBuilder();
        public DataAccessLayer DAL;
        LoggingService logger;
        public TransferService(CommandLineOptions options, LoggingService logger)
        {
            this.logger = logger;
            var fromServer = options.FromServer;
            var fromDatabase = options.FromDatabase;
            var toServer = options.ToServer;
            var toDatabase = options.ToDatabase;

            fromConnection.IntegratedSecurity = true;
            fromConnection.PersistSecurityInfo = true;
            fromConnection.InitialCatalog = fromDatabase;
            fromConnection.DataSource = fromServer;

            toConnection.IntegratedSecurity = true;
            toConnection.PersistSecurityInfo = true;
            toConnection.InitialCatalog = toDatabase;
            toConnection.DataSource = toServer;
            this.DAL = new DataAccessLayer(fromConnection, toConnection);
        }
        public void Execute(TransferData transferData)
        {
            using (IDbConnection connection = new SqlConnection(toConnection.ConnectionString))
            {
                connection.Open();
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                      logger.LogInfo("Beginning transaction at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                      TrasferAllData(connection, transaction, transferData);
                      transaction.Commit();
                      logger.LogInfo("Ended transaction at" + DateTime.Now.ToString("yyyyMMddHHmmss"));
                    }
                    catch (Exception ex)
                    {
                        logger.LogErrorMessage(ex);
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void TrasferAllData(IDbConnection connection, IDbTransaction transaction, TransferData transferData )
        {
            MoveClientDataToTarget(connection, transaction, transferData.ClientData);
            MoveAccountDataToTarget(connection, transaction, transferData.AccountDataList);
            MoveClientAccountDataToTarget(connection, transaction, transferData.ClientAccountDataList, transferData.ClientData, transferData.AccountDataList);
            MoveAddressDataToTarget(connection, transaction, transferData.AddressData);
        }
        public void MoveClientDataToTarget(IDbConnection connection, IDbTransaction transaction, ClientData data)
        {
            DAL.Insert(connection, data, transaction);
        }
        public void MoveAddressDataToTarget(IDbConnection connection, IDbTransaction transaction, AddressData data)
        {
            DAL.Insert(connection, data, transaction);
        }

        public void MoveAccountDataToTarget(IDbConnection connection, IDbTransaction transaction, List<AccountData> data)
        {
            data.ForEach(ad => ad.OldAccountRef = ad.AccountRef);

            data.ForEach(d => d.AccountRef = 0);
                data.ForEach(d => d.AccountRef = (int)DAL.Insert(connection, d, transaction));
        }

        public void MoveClientAccountDataToTarget(IDbConnection connection, IDbTransaction transaction, List<ClientAccountData> clientAccountData, ClientData clientData, List<AccountData> accountData)
        {
            clientAccountData.ForEach(ca =>
            {
                ca.ClientRef = clientData.ClientRef;
                ca.AccountRef = accountData.Single(ad => ad.OldAccountRef == ca.AccountRef).AccountRef;
                ca.ClientAccountRef = 0;
            });
            DAL.Insert(connection, clientAccountData, transaction);
        }

        public IEnumerable<AccountData> GetAccountDataFromSource(int clientRef)
        {
                var parameters = new DynamicParameters();
                parameters.Add("@clientRef", clientRef, DbType.Int32);
                var data = DAL.Query<AccountData>("select a.* from ClientAccount c join Account a on a.AccountRef = c.AccountRef where c.ClientRef = @clientRef",
                    parameters, commandType: CommandType.Text);
                return data;
        }
        public IEnumerable<ClientAccountData> GetClientAccountDataFromSource(int clientRef)
        {
                var parameters = new DynamicParameters();
                parameters.Add("@clientRef", clientRef, DbType.Int32);
                var data = DAL.Query<ClientAccountData>("select c.* from ClientAccount c where c.ClientRef = @clientRef",
                    parameters, commandType: CommandType.Text);
                return data;
        }
        public ClientData GetClientDataFromSource(int clientRef)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@clientRef", clientRef, DbType.Int32);
            var data = DAL.Query<ClientData>("select * from Client where ClientRef = @clientRef", parameters, commandType: CommandType.Text);
            return data.FirstOrDefault(); 
        }
        public AddressData GetAddressDataFromSource(int clientRef)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@clientRef", clientRef, DbType.Int32);
            var data = DAL.Query<AddressData>("select * from Addresses where ClientRef = @clientRef", parameters, commandType: CommandType.Text);
            return data.FirstOrDefault();
        }
    }
}

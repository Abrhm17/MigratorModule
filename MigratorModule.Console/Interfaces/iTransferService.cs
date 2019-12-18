using System;
using System.Collections.Generic;
using System.Text;
using MigratorModule.Console.Models;
using System.Data;

namespace MigratorModule.Console
{
    public interface ITransferService
    {
            void Execute(TransferData transferData);
            void MoveClientDataToTarget(IDbConnection connection, IDbTransaction transaction, ClientData data);
            void MoveAddressDataToTarget(IDbConnection connection, IDbTransaction transaction, AddressData data);
            void MoveAccountDataToTarget(IDbConnection connection, IDbTransaction transaction, List<AccountData> data);
            void MoveClientAccountDataToTarget(IDbConnection connection, IDbTransaction transaction, List<ClientAccountData> clientAccountData, ClientData clientData, List<AccountData> accountData);
            IEnumerable<AccountData> GetAccountDataFromSource(int clientRef);
            IEnumerable<ClientAccountData> GetClientAccountDataFromSource(int clientRef);
            ClientData GetClientDataFromSource(int clientRef);
            AddressData GetAddressDataFromSource(int clientRef);
        }
    }

using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeveloperTest.Console;
using Moq;
using System.Data;
using DeveloperTest.Console.Interfaces;
using DeveloperTest.Console.Models;
using Microsoft.Extensions.DependencyInjection;


namespace TransferUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TransferCalled()
        {
            Mock<ITransferService> mockContainer = new Mock<ITransferService>();
            var mockDAL = new Mock<IDataAccessLayer>();
            LoggingService logger = new LoggingService();
            CommandLineOptions options = new CommandLineOptions
            {
                FromDatabase = "TestFrom",
                ToDatabase = "TestTo"
            };
            var transferService = new TransferService(options, logger);
            mockDAL.Verify(o => o.Insert<ClientData>(It.IsAny<ClientData>(), It.IsAny<IDbTransaction>(), It.IsAny<int?>()), Times.AtLeastOnce());
            mockDAL.Verify(o => o.Insert<AccountData>(It.IsAny<AccountData>(), It.IsAny<IDbTransaction>(), It.IsAny<int?>()), Times.AtLeastOnce());
            mockDAL.Verify(o => o.Insert<AddressData>(It.IsAny<AddressData>(), It.IsAny<IDbTransaction>(), It.IsAny<int?>()), Times.AtLeastOnce());
            mockDAL.Verify(o => o.Insert<ClientAccountData>(It.IsAny<ClientAccountData>(), It.IsAny<IDbTransaction>(), It.IsAny<int?>()), Times.AtLeastOnce());

        }

        private Mock<IDataAccessLayer> SetupDAL()
        {
            var mockDAL = new Mock<IDataAccessLayer>();
            mockDAL.Verify(o => o.Insert<ClientData>(It.IsAny<ClientData>(), It.IsAny<IDbTransaction>(), It.IsAny<int?>()), Times.AtLeastOnce());
            mockDAL.Verify(o => o.Insert<AccountData>(It.IsAny<AccountData>(), It.IsAny<IDbTransaction>(), It.IsAny<int?>()), Times.AtLeastOnce());
            mockDAL.Verify(o => o.Insert<AddressData>(It.IsAny<AddressData>(), It.IsAny<IDbTransaction>(), It.IsAny<int?>()), Times.AtLeastOnce());
            mockDAL.Verify(o => o.Insert<ClientAccountData>(It.IsAny<ClientAccountData>(), It.IsAny<IDbTransaction>(), It.IsAny<int?>()), Times.AtLeastOnce());
            return mockDAL;
        }

    }
}

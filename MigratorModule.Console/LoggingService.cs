using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using System.Xml;
using System.IO;
using System.Reflection;

namespace MigratorModule.Console
{
    public class LoggingService
    {
         private readonly ILog log = LogManager.GetLogger(typeof(Program));
        public LoggingService()
        {
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "\\log4net.config"));
            var repo = LogManager.CreateRepository(
            Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
        }

        public void LogErrorMessage(Exception ex)
        {
            log.Error(ex);
        }
        public void LogInfo(string message)
        {
            log.Info(message);
        }
    }
}

using log4net;
using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace subscription.repositories.Log4net
{
    public class Log4net : ILog4net
    {

        private static readonly string LOG_CONFIG_FILE = "D:\\source\\Subscription\\subscription.repositories\\Log4net\\log4net.config";

        private static readonly log4net.ILog _log = GetLogger(typeof(Log4net));


        public Log4net()
        {
           
        }

        public static ILog GetLogger(Type type)
        {

            return LogManager.GetLogger(type);
        }

        public void Warn(object message)
        {
            SetLog4NetConfiguration("Warn");
            _log.Warn(message);
        }

        public void Debug(object message)
        {
            SetLog4NetConfiguration("Debug");
            _log.Debug(message);
        }

        public void Error(object message)
        {
            SetLog4NetConfiguration("Error");
            _log.Error(message);
        }

        public void Info(object message)
        {
            SetLog4NetConfiguration("Info");
            _log.Info(message);
        }

        private void SetLog4NetConfiguration(string logType)
        {
            string logFolderPath = "C:\\Subscription\\Logs\\";

            string LogFile = logFolderPath + logType + "_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";

            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead(LOG_CONFIG_FILE));

            XmlNode root = log4netConfig.DocumentElement;
            XmlNode subNode2 = root.SelectNodes("appender")[1];

            XmlNode nodeForModifyfile = subNode2.SelectSingleNode("file");

            nodeForModifyfile.Attributes[0].Value = LogFile;

            var repo = LogManager.CreateRepository(
                Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);

        }
    }
}

using System;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using log4net;
using log4net.Config;

namespace SIE.Base.Log
{
    public class Logger
    {
        /// <summary>
        /// Genera en el archivo de log el mensaje de Error 
        /// </summary>
        /// <param name="exception"></param>
        public static void Error(Exception exception)
        {
            ILog log = LogManager.GetLogger("SIELog");
            XmlConfigurator.Configure();
            log.Error(exception.Message, exception);
        }

        /// <summary>
        /// Genera en el achivo de log mensaje de información 
        /// </summary>
        public static void Info()
        {
            string result = string.Empty;
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["LoggerInfo"]))
            {
                ILog log = LogManager.GetLogger("SIELog");
                XmlConfigurator.Configure();
                var trace = new StackTrace();
                StackFrame frame = trace.GetFrame(1);
                if (frame != null)
                {
                    MethodBase method = frame.GetMethod();
                    if (method != null)
                    {
                        if (method.DeclaringType != null)
                        {
                            result = string.Format("{0}.{1}", method.DeclaringType.FullName, method.Name);
                        }
                    }
                }
                log.Info(result);
            }
        }
    }
}

using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common.Logs
{
    /// <summary>
    /// 日志记录帮助类
    /// </summary>
    public class NLogHelper
    {
        private static ILogger logger;

        public NLogHelper()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        #region Debug
        public static void Debug(string msg)
        {
            logger.Debug(msg);
        }

        public static void Debug(string msg, Exception err)
        {
            logger.Debug(err, msg);
        }
        #endregion

        #region Info
        public static void Info(string msg)
        {
            logger.Info(msg);
        }

        public static void Info(string msg, Exception err)
        {
            logger.Info(err, msg);
        }
        #endregion

        #region Warn
        public static void Warn(string msg)
        {
            logger.Warn(msg);
        }

        public static void Warn(string msg, Exception err)
        {
            logger.Warn(err, msg);
        }
        #endregion

        #region Trace
        public static void Trace(string msg)
        {
            logger.Trace(msg);
        }

        public static void Trace(string msg, Exception err)
        {
            logger.Trace(err, msg);
        }
        #endregion

        #region Error
        public static void Error(string msg)
        {
            logger.Error(msg);
        }

        public static void Error(string msg, Exception err)
        {
            logger.Error(err, msg);
        }
        #endregion

        #region Fatal
        public static void Fatal(string msg)
        {
            logger.Fatal(msg);
        }

        public static void Fatal(string msg, Exception err)
        {
            logger.Fatal(err, msg);
        }
        #endregion


    }
}

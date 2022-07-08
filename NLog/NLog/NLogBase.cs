using NLog;
using NLog.Config;
using NLog.Targets;
using System;

namespace NLogCustomize
{
    public class NLogBase
    {
        #region [建構函式]
        
        /// <summary>
        /// 建構式
        /// </summary>
        public NLogBase()
        {
            Configuration = new LoggingConfiguration();
        }

        #endregion

        #region [屬性]

        public LoggingConfiguration Configuration;

        #endregion

        #region [方法]

        /// <summary>
        /// NLog Setup TargetName
        /// </summary>
        /// <param name="pConfig"></param>
        /// <param name="pFileNmae"></param>
        /// <param name="pTargetName"></param>
        /// <returns>Logger Manager。</returns>
        public Logger NLogSetup(LoggingConfiguration pConfig, string pFileNmae, string pTargetName)
        {
            try
            {
                FileTarget TargetInfo = new FileTarget
                {
                    FileName = "${basedir}/Logs/${shortdate}/" + pFileNmae + "Logger.log",
                    Layout = "${longdate} | ${level:uppercase=true}  | ${message}",                   
                    KeepFileOpen = true
                    
                };
                FileTarget TargetDebug = new FileTarget
                {
                    FileName = "${basedir}/Logs/${shortdate}/" + pFileNmae + "Logger.log",
                    Layout = "${longdate} | ${level:uppercase=true} | ${message}",
                    KeepFileOpen = true
                };
                FileTarget TargetError = new FileTarget
                {
                    FileName = "${basedir}/Logs/${shortdate}/" + pFileNmae + "Logger.log",
                    Layout = "${longdate} | ${level:uppercase=true} | ${message} ${newline} ${stacktrace}",
                    KeepFileOpen = true,
                };

                // [Rules]
                pConfig.AddRuleForOneLevel(LogLevel.Info, TargetInfo, pTargetName);
                pConfig.AddRuleForOneLevel(LogLevel.Warn, TargetInfo, pTargetName);
                pConfig.AddRuleForOneLevel(LogLevel.Debug, TargetDebug, pTargetName);
                pConfig.AddRuleForOneLevel(LogLevel.Trace, TargetDebug, pTargetName);
                pConfig.AddRuleForOneLevel(LogLevel.Error, TargetError, pTargetName);

                // [Setup]
                LogManager.Configuration = pConfig;
                return LogManager.GetLogger(pTargetName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}

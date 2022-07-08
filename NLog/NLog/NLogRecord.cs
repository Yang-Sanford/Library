using NLog;
using System;

namespace NLogCustomize
{
    public class NLogRecord
    {
        #region [建構函式]

        public NLogRecord() { }

        public NLogRecord(Logger log)
        {
            logger = log;
        }

        #endregion

        #region [屬性]

        public Logger logger;

        #endregion

        #region [方法]

        /// <summary>
        /// 紀錄處理步驟
        /// </summary>
        /// <param name="pStep"></param>
        public void LogStepAction(int pStep)
        {
            switch (pStep)
            {
                case 1:
                    logger.Info("1 - Get Last Record");
                    break;
                case 2:
                    logger.Info("2 - Get Source DataSet");
                    break;
                case 3:
                    logger.Info("3 - Begin Transcation");
                    break;
                case 4:
                    logger.Info("4 - Process Data");
                    break;
                case 5:
                    logger.Info("5 - Batch Data Import");
                    break;
                case 6:
                    logger.Info("6 - Update Last Data Record");
                    break;
                case 7:
                    logger.Info("7 - Logger Process Record");
                    break;
            }
        }

        /// <summary>
        /// 紀錄前一次執行結果 (時間)
        /// </summary>
        /// <param name="pUpdateTime"></param>
        public void LogLastResult(DateTime pUpdateTime)
        {
            logger.Info($"(Last UpdateTime - {pUpdateTime:yyyy-MM-dd HH:mm:ss.ffffff})");
        }

        /// <summary>
        /// 紀錄前一次執行結果 (心跳 | 時間 | 編號)
        /// </summary>
        /// <param name="pHeartBeat"></param>
        /// <param name="pUpdateTime"></param>
        /// <param name="pUpdateID"></param>
        public void LogLastResult(DateTime pHeartBeat, DateTime pUpdateTime, string pUpdateID)
        {
            logger.Info($"Last HeartBeat  - {pHeartBeat:yyyy-MM-dd HH:mm:ss.fff}");
            logger.Info($"Last UpdateTime - {pUpdateTime:yyyy-MM-dd HH:mm:ss.fff}");
            logger.Info($"Last UpdateID   - {pUpdateID:yyyy-MM-dd HH:mm:ss.fff}");
        }

        /// <summary>
        /// 顯示當次執行紀錄
        /// </summary>
        /// <param name="pDataCnt"></param>
        /// <param name="pInsertCnt"></param>
        /// <param name="pUpdateCnt"></param>
        /// <param name="pDeleteCnt"></param>
        public void LogExecResult(int pDataCnt, int pInsertCnt, int pUpdateCnt, int pDeleteCnt)
        {
            logger.Info($"(Source Data Count：{pDataCnt})");
            logger.Info($"(Insert Data Count：{pInsertCnt})");
            logger.Info($"(Update Data Count：{pUpdateCnt})");
            logger.Info($"(Delete Data Count：{pDeleteCnt})");
        }

        /// <summary>
        /// 分隔線 (用於每一次執行紀錄)
        /// </summary>
        public void Demarcation(string pTableName)
        {
            logger.Trace($"-------------------- {pTableName} Data Process  --------------------");
        }

        #endregion
    }
}

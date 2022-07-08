using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;

namespace DataBase.OdbcDataBase
{
    public class OdbcDataCrud <Table> where Table : class, new()
    {
        #region [建構函式]

        /// <summary>
        /// 建構子
        /// </summary>
        public OdbcDataCrud() 
        {
            
        }

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="pConn"></param>
        public OdbcDataCrud(OdbcConnection pConnection)
        {
            Conn = pConnection;
        }

        #endregion

        #region [屬性]

        public OdbcConnection Conn;

        #endregion

        #region [方法]

        #region [CRUD]

        /// <summary>
        /// 取得資料集
        /// </summary>
        /// <param name="pQuery"></param>
        /// <returns></returns>
        public IEnumerable<Table> GetData(string pQuery)
        {
            try
            {
                IEnumerable<Table> dataset = Conn.Query<Table>(pQuery).ToList();
                return dataset;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 取得資料集
        /// </summary>
        /// <param name="pQuery"></param>
        /// <param name="pTranscation"></param>
        /// <returns></returns>
        public IEnumerable<Table> GetData(string pQuery, OdbcTransaction pTranscation)
        {
            try
            {
                IEnumerable<Table> dataset = Conn.Query<Table>(pQuery, null, pTranscation).ToList();
                return dataset;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 取得資料集
        /// </summary>
        /// <param name="pSchema"></param>
        /// <param name="pTable"></param>
        /// <returns></returns>
        public IEnumerable<Table> GetData (string pSchema, string pTable)
        {
            try
            {
                string s_query = $"SELECT * FROM {pSchema}.{pTable}";
                IEnumerable<Table> dataset = Conn.Query<Table>(s_query).ToList();
                return dataset;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 取得資料集
        /// </summary>
        /// <param name="pSchema"></param>
        /// <param name="pTable"></param>
        /// <param name="pColumn"></param>
        /// <param name="pDateTime"></param>
        /// <returns></returns>
        public IEnumerable<Table> GetData (string pSchema, string pTable, string pColumn, DateTime pDateTime)
        {
            try
            {
                string s_query = $"SELECT * FROM {pSchema}.{pTable} WHERE {pColumn} >= '{pDateTime:yyyy-MM-dd HH:mm:ss.fff}'";
                IEnumerable<Table> dataset = Conn.Query<Table>(s_query).ToList();
                return dataset;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 取得多個資料集
        /// </summary>
        /// <param name="pQuery"></param>
        /// <returns></returns>
        public SqlMapper.GridReader GetMultiData(string pQuery)
        {
            try
            {
                SqlMapper.GridReader dataset = Conn.QueryMultiple(pQuery);
                return dataset;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 找尋資料是否存在
        /// </summary>
        /// <param name="pQuery"></param>
        /// <returns>檢查結果，有資料為 True。</returns>
        public bool FindData(string pQuery)
        {
            try
            {
                IEnumerable<dynamic> dataset = Conn.Query(pQuery);

                if (dataset.Count() > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 找尋資料是否存在
        /// </summary>
        /// <param name="pQuery"></param>
        /// <returns></returns>
        public bool FindData(string pQuery, OdbcTransaction pTranscation)
        {
            try
            {
                IEnumerable<dynamic> dataset = Conn.Query(pQuery, null, pTranscation);

                if (dataset.Count() > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        /// <summary>
        /// 執行語法
        /// </summary>
        /// <param name="pQuery"></param>
        /// <returns></returns>
        public int ExecQuery(string pQuery)
        {
            try
            {
                return Conn.Execute(pQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 執行語法
        /// </summary>
        /// <param name="pQuery"></param>
        /// <param name="pTranscation"></param>
        /// <returns></returns>
        public int ExecQuery(string pQuery, OdbcTransaction pTranscation)
        {
            try
            {
                return Conn.Execute(pQuery, null, pTranscation);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 執行批次新增 or 更新語法
        /// </summary>
        /// <param name="pQuery"></param>
        /// <param name="Param"></param>
        /// <param name="pTranscation"></param>
        /// <returns></returns>
        public int ExecQuery(string pQuery, IEnumerable<Table> Param, OdbcTransaction pTranscation)
        {
            try
            {
                return Conn.Execute(pQuery, Param, pTranscation);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region [取得最後執行紀錄]

        /// <summary>
        /// 取得最後執行紀錄
        /// </summary>
        /// <param name="pRecordTable"></param>
        /// <param name="pSourceTable"></param>
        /// <param name="pDestTable"></param>
        /// <returns>最後一筆資料的時間及編號。</returns>
        public (DateTime, DateTime, string) GetLastRecord(string pRecordTable, string pSourceTable, string pDestTable, DateTime pInitailTime)
        {
            DateTime heartbeat = DateTime.Now; 
            DateTime dt = pInitailTime; 
            string id = "";
            try
            {
                string s_query = $"SELECT * FROM {pRecordTable} WHERE source = '{pSourceTable}' AND dest = '{pDestTable}';";
                IEnumerable<dynamic> record = Conn.Query(s_query);

                foreach (dynamic data in record)
                {
                    if (data.heartbeat != null)
                        heartbeat = data.heartbeat;
                    if (data.dt != null)
                        dt = data.dt;
                    if (data.id != null)
                        id = data.id;
                }

                return (heartbeat, dt, id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 取得最後執行紀錄
        /// </summary>
        /// <param name="pRecordTable"></param>
        /// <param name="pSourceTable"></param>
        /// <param name="pDestTable"></param>
        /// <returns></returns>
        public DateTime GetLastTime(string pRecordTable, string pSourceTable, string pDestTable, DateTime InitialTime)
        {
            DateTime dt = InitialTime;
            try
            {
                string s_query = $"SELECT dt FROM {pRecordTable} WHERE source = '{pSourceTable}' AND dest = '{pDestTable}';";
                dynamic record = Conn.Query(s_query);

                foreach (dynamic data in record)
                {
                    if (data.dt != null)
                        dt = data.dt;
                }
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region [更新執行紀錄]

        /// <summary>
        /// 更新執行紀錄
        /// </summary>
        /// <param name="pRecordTable"></param>
        /// <param name="pSourceTable"></param>
        /// <param name="pDestTable"></param>
        /// <param name="pHeartBeat"></param>
        /// <param name="pUpdateTime"></param>
        /// <param name="pUpdateID"></param>
        /// <param name="pTranscation"></param>
        /// <returns></returns>
        public bool UpdateRecord(string pRecordTable, string pSourceTable, string pDestTable, DateTime pUpdateTime, string pUpdateID, OdbcTransaction pTranscation)
        {
            try
            {
                #region [Query]

                string s_query =
                    @"SELECT * FROM {0}";
                string i_query =
                    @"INSERT INTO {0} (source, dest, heartbeat, dt, id) VALUES ('{1}', '{2}', '{3:yyyy-MM-dd HH:mm:ss.fff}', '{4:yyyy-MM-dd HH:mm:ss.fff}', '{5}')";
                string u_query =
                    @"UPDATE {0} SET heartbeat = '{1:yyyy-MM-dd HH:mm:ss.ffffff}', dt = '{2:yyyy-MM-dd HH:mm:ss.ffffff}', id = '{3}' WHERE source = '{4}' AND dest = '{5}'";

                #endregion

                IEnumerable<dynamic> dataset = Conn.Query(string.Format(s_query, pRecordTable), null, pTranscation);
                IEnumerable<dynamic> check_result =
                    from d in dataset
                    where d.source == pSourceTable
                        & d.dest == pDestTable
                    select d;

                if (check_result.Count() == 0)
                    Conn.Execute(string.Format(i_query, pRecordTable, pSourceTable, pDestTable, DateTime.Now, pUpdateTime, pUpdateID), null, pTranscation);
                else
                    Conn.Execute(string.Format(u_query, pRecordTable, DateTime.Now, pUpdateTime, pUpdateID, pSourceTable, pDestTable), null, pTranscation);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 更新執行紀錄
        /// </summary>
        /// <param name="pRecordTable"></param>
        /// <param name="pSourceTable"></param>
        /// <param name="pDestTable"></param>
        /// <param name="pHeartBeat"></param>
        /// <param name="pUpdateTime"></param>
        /// <returns></returns>
        public bool UpdateRecord(string pRecordTable, string pSourceTable, string pDestTable, DateTime pUpdateTime, OdbcTransaction pTranscation)
        {
            try
            {
                #region [Query]

                string s_query = 
                    @"SELECT * FROM {0}";
                string i_query = 
                    @"INSERT INTO {0} (source, dest, heartbeat, dt) VALUES ('{1}', '{2}', '{3:yyyy-MM-dd HH:mm:ss.fff}', '{4:yyyy-MM-dd HH:mm:ss.fff}')";
                string u_query = 
                    @"UPDATE {0} SET heartbeat = '{1:yyyy-MM-dd HH:mm:ss.ffffff}', dt = '{2:yyyy-MM-dd HH:mm:ss.ffffff}' WHERE source = '{3}' AND dest = '{4}'";

                #endregion

                IEnumerable<dynamic> dataset = Conn.Query(string.Format(s_query, pRecordTable), null, pTranscation);
                IEnumerable<dynamic> check_result = 
                    from d in dataset
                    where d.source == pSourceTable
                        & d.dest == pDestTable
                    select d;

                if (check_result.Count() == 0)
                    Conn.Execute(string.Format(i_query, pRecordTable, pSourceTable, pDestTable, DateTime.Now, pUpdateTime), null, pTranscation);
                else
                    Conn.Execute(string.Format(u_query, pRecordTable, DateTime.Now, pUpdateTime, pSourceTable, pDestTable), null, pTranscation);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 更新執行紀錄
        /// </summary>
        /// <param name="pRecordTable"></param>
        /// <param name="pSourceTable"></param>
        /// <param name="pDestTable"></param>
        /// <param name="pHeartBeat"></param>
        /// <param name="pUpdateTime"></param>
        /// <returns></returns>
        public bool UpdateRecord(string pRecordTable, string pSourceTable, string pDestTable, string pUpdateID, OdbcTransaction pTranscation)
        {
            try
            {
                #region [Query]

                string s_query = 
                    @"SELECT * FROM {0}";
                string i_query =
                    @"INSERT INTO {0} (source, dest, heartbeat, id) VALUES ('{1}', '{2}', '{3:yyyy-MM-dd HH:mm:ss.fff}', '{4}')";
                string u_query =
                    @"UPDATE {0} SET heartbeat = '{1:yyyy-MM-dd HH:mm:ss.ffffff}', id = '{2}' WHERE source = '{3}' AND dest = '{4}'";

                #endregion

                IEnumerable<dynamic> dataset = Conn.Query(string.Format(s_query, pRecordTable), null, pTranscation);
                IEnumerable<dynamic> check_result = 
                    from d in dataset
                    where d.source == pSourceTable
                        & d.dest == pDestTable
                    select d;

                if (check_result.Count() == 0)
                    Conn.Execute(string.Format(i_query, pRecordTable, pSourceTable, pDestTable, DateTime.Now, pUpdateID), null, pTranscation);
                else
                    Conn.Execute(string.Format(u_query, pRecordTable, DateTime.Now, pUpdateID, pSourceTable, pDestTable), null, pTranscation);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 更新執行紀錄
        /// </summary>
        /// <param name="pRecordTable"></param>
        /// <param name="pSourceTable"></param>
        /// <param name="pDestTable"></param>
        /// <param name="pHeartBeat"></param>
        /// <returns></returns>
        public bool UpdateRecord(string pRecordTable, string pSourceTable, string pDestTable, OdbcTransaction pTranscation)
        {
            try
            {
                #region [Query]

                string s_query = 
                    @"SELECT * FROM {0}";
                string i_query = 
                    @"INSERT INTO {0} (source, dest, heartbeat) VALUES ('{1}', '{2}', '{3:yyyy-MM-dd HH:mm:ss.ffffff}')";
                string u_query = 
                    @"UPDATE {0} SET heartbeat = '{1:yyyy-MM-dd HH:mm:ss.ffffff}' WHERE source = '{2}' AND dest = '{3}'";

                #endregion

                IEnumerable<dynamic> dataset = Conn.Query(string.Format(s_query, pRecordTable), null, pTranscation);
                IEnumerable<dynamic> check_result = 
                    from d in dataset
                    where d.source == pSourceTable
                        & d.dest == pDestTable
                    select d;

                if (check_result.Count() == 0)
                    Conn.Execute(string.Format(i_query, pRecordTable, pSourceTable, pDestTable, DateTime.Now), null, pTranscation);
                else
                    Conn.Execute(string.Format(u_query, pRecordTable, DateTime.Now, pSourceTable, pDestTable), null, pTranscation);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #endregion
    }
}

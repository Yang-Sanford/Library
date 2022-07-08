using System;
using System.Data.Odbc;

namespace DataBase.OdbcDataBase
{
    public class OdbcDataBase : IDisposable
    {
        #region [建構函式]

        /// <summary>
        /// 建構子
        /// </summary>
        public OdbcDataBase() 
        {
            
        }

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="pConnStr"></param>
        public OdbcDataBase(string pOdbcName)
        {
            Conn = Initialize(pOdbcName);
            Open();
        }

        #endregion

        #region [屬性]

        public OdbcConnection Conn;

        #endregion

        #region [方法]

        public OdbcConnection Initialize(string pOdbcName)
        {
            string connection_string = $"DSN={pOdbcName}";

            return new OdbcConnection(connection_string);
        }

        /// <summary>
        /// 開啟資料庫連線
        /// </summary>
        /// <returns>連線開起，成功回傳 Ture。</returns>
        public bool Open()
        {
            try
            {
                if (Conn.State.ToString() == "Closed")
                    Conn.Open();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 關閉資料庫連線
        /// </summary>
        /// <returns>連線關閉，成功回傳 Ture。</returns>
        public bool Close()
        {
            try
            {
                if (Conn.State.ToString() == "Open")
                    Conn.Close();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 釋放資料庫資源
        /// </summary>
        /// <returns>釋放資源，成功回傳 Ture。</returns>
        public void Dispose()
        {
            try
            {
                Conn.Dispose();
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}

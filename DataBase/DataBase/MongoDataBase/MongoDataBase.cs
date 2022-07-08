using MongoDB.Driver;
using System;

namespace DataBase.MongoDataBase
{
    public class MongoDataBase
    {
        #region [建構函式]

        /// <summary>
        /// 建構子
        /// </summary>
        public MongoDataBase() { }

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="pConnectionString"></param>
        public MongoDataBase(string pConnectionString)
        {
            Conn = Initialize(pConnectionString);
        }

        #endregion

        #region [屬性]

        public MongoClient Conn;

        #endregion

        #region [方法]

        public MongoClient Initialize(string pConnectionString)
        {
            return new MongoClient(pConnectionString);
        }

        #endregion
    }
}

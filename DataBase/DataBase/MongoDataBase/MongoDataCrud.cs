using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataBase.MongoDataBase
{
    public class MongoDataCrud
    {
        #region [建構函式]

        /// <summary>
        /// 建構子
        /// </summary>
        public MongoDataCrud() { }

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="pConn"></param>
        public MongoDataCrud(MongoClient conn) 
        {
            Conn = conn;
        }

        #endregion

        #region [屬性]

        public MongoClient Conn;

        #endregion

        #region [方法]

        /// <summary>
        /// 讀取資料表 (讀取整張資料表)
        /// </summary>
        /// <param name="pDataBase"></param>
        /// <param name="pTable"></param>
        /// <returns>資料集。</returns>
        public List<BsonDocument> GetData(string pDataBase, string pTable)
        {
            try
            {
                IMongoCollection<BsonDocument> collection = Conn.GetDatabase(pDataBase).GetCollection<BsonDocument>(pTable);
                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Nin("_id", new List<string> { "", "null", null });
                List<BsonDocument> dataset = collection.Find(filter).ToList();

                return dataset;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 讀取資料表 (限制讀取十萬筆 - 未排序)
        /// </summary>
        /// <param name="pDatabBase"></param>
        /// <param name="pTable"></param>
        /// <param name="pFilter"></param>
        /// <returns>資料集。</returns>
        public List<BsonDocument> GetData(string pDatabBase, string pTable, FilterDefinition<BsonDocument> pFilter)
        {
            try
            {
                IMongoCollection<BsonDocument> collection = Conn.GetDatabase(pDatabBase).GetCollection<BsonDocument>(pTable);
                List<BsonDocument> dataset = collection.Find(pFilter).Limit(100000).ToList();

                return dataset;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 讀取資料表 (限制讀取五萬筆 - 排序)
        /// </summary>
        /// <param name="pDataBase"></param>
        /// <param name="pTable"></param>
        /// <param name="pFilter"></param>
        /// <param name="pFiledName"></param>
        /// <returns></returns>
        public List<BsonDocument> GetData(string pDataBase, string pTable, FilterDefinition<BsonDocument> pFilter, string pFiledName)
        {
            try
            {
                IMongoCollection<BsonDocument> Collection = Conn.GetDatabase(pDataBase).GetCollection<BsonDocument>(pTable);
                List<BsonDocument> dataset = Collection.Find(pFilter).Sort(Builders<BsonDocument>.Sort.Ascending(pFiledName)).Limit(50000).ToList();

                return dataset;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}

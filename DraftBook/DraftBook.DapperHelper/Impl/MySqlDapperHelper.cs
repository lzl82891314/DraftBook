using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

using DraftBook.DapperHelper.Interface;
using DraftBook.DapperHelper.Common;
using MySql.Data.MySqlClient;

namespace DraftBook.DapperHelper.Impl
{
    public class MySqlDapperHelper : IDapperHelper
    {
        public IDbConnection CreateQueryObj(string connName)
        {
            var collectionObj = ConfigurationManager.ConnectionStrings[connName] ?? new ConnectionStringSettings();
            return new MySqlConnection(collectionObj.ConnectionString);
        }

        public string JointDeleteSql(string deleteTable, string deleteWhere)
        {
            return $"DELETE { deleteTable} WHERE { deleteWhere}";
        }

        public string JointInsertSql(string insertTable, string insertField, string valueField)
        {
            return $"INSERT INTO { insertTable}({ insertField}) VALUES({ valueField})";
        }

        public string JointInsertSql<T>()
        {
            var tableName = CommonMethod.GetTableName<T>();
            var insertSql = CommonMethod.GetInsertSql<T>();
            return $"INSERT INTO { tableName}({ insertSql[0]}) VALUES({ insertSql[1]})";
        }

        public string JointQuerySql(string sqlTable, string sqlWhere = "", string sqlCloumn = "*", string orderBy = "")
        {
            if (!string.IsNullOrWhiteSpace(sqlWhere))
            {
                sqlWhere = " WHERE " + sqlWhere;
            }
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                orderBy = " OrderBy " + orderBy;
            }
            return $"SELECT { sqlCloumn} FROM { sqlTable} { sqlWhere} { orderBy}";
        }

        public string JointQuerySql<T>(string sqlWhere, string orderBy = "")
        {
            var querySql = CommonMethod.GetQuerySql<T>();
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                orderBy = " OrderBy " + orderBy;
            }
            return $"SELECT { querySql[1]} FROM { querySql[0]} WHERE { sqlWhere} { orderBy}";
        }

        public string JointQuerySqlForPaged(string sqlTable, string sqlWhere, string sqlColumn, string orderBy, int pageIndex, int pageSize)
        {
            return $"SELECT { sqlColumn} FROM { sqlTable} WHERE { sqlWhere} OrderBy { orderBy} LIMIT { pageIndex},{ pageSize}";
        }

        public string JointUpdateSql(string updateTable, string updateWhere, string updateField)
        {
            return $"UPDATE { updateTable} SET { updateField} WHERE { updateWhere}";
        }

        public string JointUpdateSql<T>()
        {
            var tableName = CommonMethod.GetTableName<T>();
            var updateSql = CommonMethod.GetUpdateSql<T>();
            return $"UPDATE { tableName} SET { updateSql} WHERE Id=@Id";
        }
    }
}

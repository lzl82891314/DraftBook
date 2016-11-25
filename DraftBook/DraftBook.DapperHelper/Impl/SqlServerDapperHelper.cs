using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DraftBook.DapperHelper.Interface;
using DraftBook.DapperHelper.Common;

namespace DraftBook.DapperHelper.Impl
{
    public class SqlServerDapperHelper : IDapperHelper
    {
        public IDbConnection CreateQueryObj(string connName)
        {
            var collectionObj = ConfigurationManager.ConnectionStrings[connName] ?? new ConnectionStringSettings();
            return new SqlConnection(collectionObj.ConnectionString);
        }

        public string JointDeleteSql(string deleteTable, string deleteWhere)
        {
            return $"DELETE { deleteTable} WHERE { deleteWhere}";
        }

        public string JointInsertSql(string insertTable, string insertField, string valueField)
        {
            var tempField = string.IsNullOrWhiteSpace(insertField) ? "" : $"({ insertField})";
            return $"INSERT INTO { insertTable}{ tempField} VALUES({ valueField})";
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
            return $"SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY { orderBy}) AS RowNumber, { sqlColumn} FROM { sqlTable} WHERE { sqlWhere}) AS Total WHERE RowNumber >= { (pageIndex - 1) * pageSize + 1} AND RowNumber <= { pageIndex * pageSize}";
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

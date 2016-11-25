using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftBook.DapperHelper.Interface
{
    public interface IDapperHelper
    {
        string JointQuerySql<T>(string sqlWhere, string orderBy = "");

        string JointQuerySql(string sqlTable, string sqlWhere = "", string sqlCloumn = "*", string orderBy = "");

        string JointQuerySqlForPaged(string sqlTable, string sqlWhere, string sqlColumn, string orderBy, int pageIndex, int pageSize);

        string JointInsertSql<T>();

        string JointInsertSql(string insertTable, string insertField, string valueField);

        string JointUpdateSql<T>();

        string JointUpdateSql(string updateTable, string updateWhere, string updateField);

        string JointDeleteSql(string deleteTable, string deleteWhere);

        IDbConnection CreateQueryObj(string connName);
    }
}

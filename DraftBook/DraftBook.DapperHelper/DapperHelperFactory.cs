using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DraftBook.DapperHelper.Interface;
using DraftBook.DapperHelper.Impl;

namespace DraftBook.DapperHelper
{
    public class DapperHelperFactory
    {
        public static IDapperHelper CreateDapperHelper(DatabaseTypeEnum type)
        {
            switch (type)
            {
                case DatabaseTypeEnum.SqlServer: return new SqlServerDapperHelper();
                case DatabaseTypeEnum.MySql: return new MySqlDapperHelper();
                default: throw new NotSupportedException();
            }
        }
    }
}

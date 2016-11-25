using System;

namespace DraftBook.DapperHelper.Common
{
    /// <summary>
    /// 绑定模型类对应数据库表信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TableInfoAttribute : Attribute
    {
        /// <summary>
        /// 数据库表名
        /// </summary>
        public string TableName { get; private set; } = "";

        /// <summary>
        /// 表名简写
        /// </summary>
        public string AbbrName { get; private set; } = "";

        /// <summary>
        /// 数据库模型类表名信息特性
        /// </summary>
        /// <param name="tableName">模型类对应表名</param>
        /// <param name="abbrName">模型类对应表名简写</param>

        public TableInfoAttribute(string tableName, string abbrName = "")
        {
            TableName = tableName;
            AbbrName = abbrName;
        }
    }
}

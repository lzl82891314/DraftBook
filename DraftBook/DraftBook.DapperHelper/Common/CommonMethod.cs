using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftBook.DapperHelper.Common
{
    internal class CommonMethod
    {
        internal static string GetTableName<T>()
        {
            var type = typeof(T);
            var attrArr = type.GetCustomAttributes(typeof(TableInfoAttribute), false);
            if (attrArr != null && attrArr.Length > 0)
            {
                var attr = attrArr[0] as TableInfoAttribute;
                if (attr != null)
                {
                    return attr.TableName;
                }
            }
            return string.Empty;
        }

        internal static string[] GetInsertSql<T>()
        {
            var type = typeof(T);
            StringBuilder fieldSql = new StringBuilder();
            StringBuilder valueSql = new StringBuilder();

            foreach (var prop in type.GetProperties())
            {
                var attrArr = prop.GetCustomAttributes(typeof(DataMappingAttribute), false);
                if (attrArr != null && attrArr.Length > 0)
                {
                    var attr = attrArr[0] as DataMappingAttribute;
                    if (attr != null)
                    {
                        fieldSql.Append($"{ attr.MapplingName},");
                    }
                }
                else
                {
                    fieldSql.Append($"{ prop.Name},");
                }
                valueSql.Append($"@{ prop.Name},");
            }

            return new string[]
            {
                fieldSql.ToString().TrimEnd(','),
                valueSql.ToString().TrimEnd(',')
            };
        }

        internal static string GetUpdateSql<T>()
        {
            var type = typeof(T);
            StringBuilder fieldSql = new StringBuilder();

            foreach (var prop in type.GetProperties())
            {
                if (prop.Name.ToUpper() == "ID")
                {
                    continue;
                }
                var attrArr = prop.GetCustomAttributes(typeof(DataMappingAttribute), false);
                if (attrArr != null && attrArr.Length > 0)
                {
                    var attr = attrArr[0] as DataMappingAttribute;
                    if (attr != null)
                    {
                        fieldSql.Append($"{ attr.MapplingName}=@{ prop.Name},");
                    }
                }
                else
                {
                    fieldSql.Append($"{ prop.Name}=@{ prop.Name},");
                }
            }
            return fieldSql.ToString().TrimEnd(',');
        }

        internal static string[] GetQuerySql<T>()
        {
            var type = typeof(T);
            string tableName = string.Empty;
            string abbrName = string.Empty;

            var tableInfoAttrArr = type.GetCustomAttributes(typeof(TableInfoAttribute), false);
            if (tableInfoAttrArr != null && tableInfoAttrArr.Length > 0)
            {
                var attr = tableInfoAttrArr[0] as TableInfoAttribute;
                if (attr != null)
                {
                    tableName = attr.TableName;
                    abbrName = string.IsNullOrWhiteSpace(attr.AbbrName) ? attr.TableName : attr.AbbrName;
                }
            }

            StringBuilder queryField = new StringBuilder();
            foreach (var prop in type.GetProperties())
            {
                var attrArr = prop.GetCustomAttributes(typeof(DataMappingAttribute), false);
                if (attrArr != null && attrArr.Length > 0)
                {
                    var attr = attrArr[0] as DataMappingAttribute;
                    if (attr != null)
                    {
                        queryField.Append($"{ abbrName}.{ attr.MapplingName} AS { prop.Name},");
                    }
                }
                else
                {
                    queryField.Append($"{ abbrName}.{ prop.Name},");
                }
            }

            return new string[]
            {
                $"{ tableName} AS { abbrName}",
                queryField.ToString().TrimEnd(',')
            };
        }
    }
}

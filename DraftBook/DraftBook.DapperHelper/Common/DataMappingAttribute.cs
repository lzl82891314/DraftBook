using System;

namespace DraftBook.DapperHelper.Common
{
    /// <summary>
    /// 数据映射特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DataMappingAttribute : Attribute
    {
        /// <summary>
        /// 映射数据库名称，是数据库表对应字段的名称
        /// </summary>
        public string MapplingName { get; private set; }
        /// <summary>
        /// 当需要进行联表查询时，可能会出现相同的字段同时查询出结果的情况，此时需要使用此特性进行数据列明映射
        /// </summary>
        /// <param name="mappingName">数据库表名对应的值</param>
        public DataMappingAttribute(string mappingName)
        {
            MapplingName = mappingName;
        }
    }
}

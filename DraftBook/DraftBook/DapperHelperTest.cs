using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;
using DraftBook.DapperHelper;
using DraftBook.DapperHelper.Common;

namespace DraftBook.Console
{
    public class DapperHelperTest
    {
        public static void Main(string[] args)
        {
            //SqlServerTest1();
            //SqlServerTest2();
            //MySqlTest1();
            MySqlTest2();
            System.Console.ReadKey();
        }

        static void SqlServerTest1()
        {
            var dapperHelper = DapperHelperFactory.CreateDapperHelper(DatabaseTypeEnum.SqlServer);
            var connObj = dapperHelper.CreateQueryObj("EOP_WRITE");
            var resultList = connObj.Query<PerformanceRoyalty_Caller>(dapperHelper.JointQuerySql("PerformanceRoyalty_Caller", "RoyaltyTime='2016-11-01'"));
        }

        static void SqlServerTest2()
        {
            var dapperHelper = DapperHelperFactory.CreateDapperHelper(DatabaseTypeEnum.SqlServer);
            var connObj = dapperHelper.CreateQueryObj("EOP_WRITE");
            var resultList = connObj.Query<PerformanceRoyalty_Caller>(dapperHelper.JointQuerySql<PerformanceRoyalty_Caller>("RoyaltyTime='2016-11-01'"));
        }

        static void MySqlTest1()
        {
            var dapperHelper = DapperHelperFactory.CreateDapperHelper(DatabaseTypeEnum.MySql);
            var connObj = dapperHelper.CreateQueryObj("MySqlLocal");
            var resultList = connObj.Query<MySqlTestModel>(dapperHelper.JointQuerySql("TestTable"));
        }

        static void MySqlTest2()
        {
            var dapperHelper = DapperHelperFactory.CreateDapperHelper(DatabaseTypeEnum.MySql);
            var connObj = dapperHelper.CreateQueryObj("MySqlLocal");
            var resultList = connObj.Query<MySqlTestModel>(dapperHelper.JointQuerySql<MySqlTestModel>("1=1"));
        }
    }

    [TableInfo("TestTable")]
    public class MySqlTestModel
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        [DataMapping("Gender")]
        public int Sex { get; set; } = 0;
        public int Age { get; set; } = 0;
        public DateTime CreateTime { get; set; } = new DateTime(1900, 01, 01);
        public int IsDel { get; set; } = 0;
    }

    /// <summary>
	/// 数据实体类:PerformanceRoyalty_Caller
	/// </summary>
    [TableInfo("PerformanceRoyalty_Caller")]
	public class PerformanceRoyalty_Caller
    {
        public Int32 Id
        {
            get { return this._Id; }
            set { this._Id = value; }
        }
        private Int32 _Id = 0;
        /// <summary>
        /// 人员基础信息表PerformanceRoyalty_UserInfo对应Id
        /// </summary>
        public Int32 UserInfoId
        {
            get { return this._UserInfoId; }
            set { this._UserInfoId = value; }
        }
        private Int32 _UserInfoId = 0;
        /// <summary>
		/// 当月装修保绩效
		/// </summary>
        public decimal ZXBAchievement
        {
            get { return this._ZXBAchievement; }
            set { this._ZXBAchievement = value; }
        }
        private decimal _ZXBAchievement = 0m;
        /// <summary>
		/// 当月派单绩效
		/// </summary>
        public decimal DispatchAchievement
        {
            get { return this._DispatchAchievement; }
            set { this._DispatchAchievement = value; }
        }
        private decimal _DispatchAchievement = 0m;
        /// <summary>
        /// 装修保提成
        /// </summary>
        public decimal ZXBRoyalty
        {
            get { return this._ZXBRoyalty; }
            set { this._ZXBRoyalty = value; }
        }
        private decimal _ZXBRoyalty = 0.0M;
        /// <summary>
        /// 派单提成
        /// </summary>
        public decimal DispatchRoyalty
        {
            get { return this._DispatchRoyalty; }
            set { this._DispatchRoyalty = value; }
        }
        private decimal _DispatchRoyalty = 0.0M;
        /// <summary>
        /// 缓发提成金额
        /// </summary>
        public decimal TotalDelayedRoyalty
        {
            get { return this._TotalDelayedRoyalty; }
            set { this._TotalDelayedRoyalty = value; }
        }
        private decimal _TotalDelayedRoyalty = 0.0M;
        /// <summary>
        /// 本月缓发提成金额
        /// </summary>
        public decimal CurrentDelayedRoyalty
        {
            get { return this._CurrentDelayedRoyalty; }
            set { this._CurrentDelayedRoyalty = value; }
        }
        private decimal _CurrentDelayedRoyalty = 0.0M;
        /// <summary>
        /// 本月应发缓发金额
        /// </summary>
        public decimal PayingDelayedRoyalty
        {
            get { return this._PayingDelayedRoyalty; }
            set { this._PayingDelayedRoyalty = value; }
        }
        private decimal _PayingDelayedRoyalty = 0.0M;
        /// <summary>
        /// 备注
        /// </summary>
        public string Description
        {
            get { return this._Description; }
            set { this._Description = value; }
        }
        private string _Description = "0.0";
        /// <summary>
        /// 提成对应时间
        /// </summary>
        public DateTime RoyaltyTime
        {
            get { return this._RoyaltyTime; }
            set { this._RoyaltyTime = value; }
        }
        private DateTime _RoyaltyTime = DateTime.Parse("1900-01-01");
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return this._CreateTime; }
            set { this._CreateTime = value; }
        }
        private DateTime _CreateTime = DateTime.Now;
        /// <summary>
        /// 逻辑删除
        /// </summary>
        public Int32 IsDel
        {
            get { return this._IsDel; }
            set { this._IsDel = value; }
        }
        private Int32 _IsDel = 0;
    }
}

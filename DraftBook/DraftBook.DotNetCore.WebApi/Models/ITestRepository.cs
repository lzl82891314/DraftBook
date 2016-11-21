using System;
using System.Collections.Generic;

namespace DraftBook.DotNetCore.WebApi.Models
{
    /// <summary>
    /// 测试CURD仓库
    /// repository 类是一个封装了数据层的类，包含了获取数据并映射到实体模型类的业务逻辑
    /// </summary>
    public interface ITestRepository
    {
        void Add(TestModel item);
        IEnumerable<TestModel> GetAll();
        TestModel Find(string key);
        TestModel Remove(string key);
        void Update(TestModel item);
    }
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DraftBook.DotNetCore.WebApi.Models
{
    public class TestRepository : ITestRepository
    {
        static ConcurrentDictionary<string, TestModel> _testList = new ConcurrentDictionary<string, TestModel>();

        public TestRepository()
        {
            Add(new TestModel() { Name = "TestItem1" });
        }

        public void Add(TestModel item)
        {
            item.Key = Guid.NewGuid().ToString();
            _testList[item.Key] = item;
        }

        public TestModel Find(string key)
        {
            TestModel resultModel;
            _testList.TryGetValue(key, out resultModel);
            return resultModel;
        }

        public IEnumerable<TestModel> GetAll()
        {
            return _testList.Values;
        }

        public TestModel Remove(string key)
        {
            TestModel resultModel;
            _testList.TryRemove(key, out resultModel);
            return resultModel;
        }

        public void Update(TestModel item)
        {
            _testList[item.Key] = item;
        }
    }
}

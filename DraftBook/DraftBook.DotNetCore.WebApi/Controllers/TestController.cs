using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using DraftBook.DotNetCore.WebApi.Models;

namespace DraftBook.DotNetCore.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        public ITestRepository TestRepo { get; set; }
        public TestController(ITestRepository testRepo)
        {
            TestRepo = testRepo;
        }

        public IEnumerable<TestModel> GetAll()
        {
            return TestRepo.GetAll();
        }

        [HttpGet("{id}", Name = "GetTest")]
        public IActionResult GetById(string id)
        {
            var testModel = TestRepo.Find(id);
            if (testModel == null)
            {
                return NotFound();
            }
            return new ObjectResult(testModel);
        }
    }
}
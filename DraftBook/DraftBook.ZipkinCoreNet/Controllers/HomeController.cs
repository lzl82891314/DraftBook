using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using zipkin4net.Transport.Http;

namespace DraftBook.ZipkinCoreNet.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ZipkinSend()
        {
            var applicationName = _configuration["Appsettings:applicationName"];

            var requestUrl = "http://124.251.48.19:60304/pc/company/getjcstorelistex/?aduitstate=-1&bparentorgid=0&orgid=56723&rootorgid=22938969&suborgids=56723&subcompanyids=501428,501429,501474";

            using (var httpClient = new HttpClient(new TracingHandler(applicationName)))
            {
                var response = await httpClient.GetAsync(requestUrl);
                var content = await response.Content.ReadAsStringAsync();

                return Content(content);
            }
        }
    }
}
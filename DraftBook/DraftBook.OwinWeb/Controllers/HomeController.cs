using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Medidata.ZipkinTracer.Core;
using Medidata.ZipkinTracer.Core.Handlers;

namespace DraftBook.OwinWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public async Task<ActionResult> BaseClientUse()
        {
            var zipkinServerUrl = ConfigurationManager.AppSettings["ZipkinServerUrl"] ?? "http://localhost:9411";
            var context = System.Web.HttpContext.Current.GetOwinContext();

            var config = new ZipkinConfig // you can use Dependency Injection to get the same config across your app.
            {
                Domain = request => request.Uri,
                ZipkinBaseUri = new Uri(zipkinServerUrl),
                SpanProcessorBatchSize = 10,
                SampleRate = 1
            };

            var client = new ZipkinClient(config, context);

            using (var httpClient = new HttpClient(new ZipkinMessageHandler(client)))
            {
                var response = await httpClient.GetAsync("http://www.google.com");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                }
            }
            return View();
        }

        public async Task<ActionResult> ExtensionClientUse()
        {
            var zipkinServerUrl = ConfigurationManager.AppSettings["ZipkinServerUrl"] ?? "http://localhost:9411";
            var context = System.Web.HttpContext.Current.GetOwinContext();
            var config = new ZipkinConfig // you can use Dependency Injection to get the same config across your app.
            {
                Domain = request => request.Uri,
                ZipkinBaseUri = new Uri(zipkinServerUrl),
                SpanProcessorBatchSize = 10,
                SampleRate = 1
            };
            var zipkinClient = new ZipkinClient(config, context);
            var url = "http://124.251.48.19:60304";
            var requestUri = "/pc/company/getjcstorelistex/?aduitstate=-1&bparentorgid=0&orgid=56723&rootorgid=22938969&suborgids=56723&subcompanyids=501428,501429,501474";
            HttpResponseMessage result;
            using (var client = new HttpClient(new ZipkinMessageHandler(zipkinClient)))
            {
                client.BaseAddress = new Uri(url);
                // start client trace
                var span = zipkinClient.StartClientTrace(new Uri(client.BaseAddress, requestUri), "GET", zipkinClient.TraceProvider);

                zipkinClient.Record(span, "A description which will gets recorded with a timestamp.");

                result = await client.GetAsync(requestUri);

                // Record the total memory used after the call
                zipkinClient.RecordBinary(span, "client.memory", GC.GetTotalMemory(false));

                // end client trace
                zipkinClient.EndClientTrace(span, (int)result.StatusCode);
            }

            return View();
        }
    }
}
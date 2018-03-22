using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Medidata.ZipkinTracer.Core;
using Medidata.ZipkinTracer.Core.Middlewares;
using System.Configuration;

namespace DraftBook.OwinWeb
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var zipkinServerUrl = ConfigurationManager.AppSettings["ZipkinServerUrl"] ?? "http://localhost:9411";
            var zipkinConfig = new ZipkinConfig
            {
                Domain = request => request.Uri, // or, you might like to derive a value from the request, like r => new Uri($"{r.Scheme}{Uri.SchemeDelimiter}{r.Host}"),
                ZipkinBaseUri = new Uri(zipkinServerUrl),
                SpanProcessorBatchSize = 10,
                SampleRate = 1
            };

            app.UseZipkin(zipkinConfig);
        }
    }
}
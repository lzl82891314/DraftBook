using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DraftBook.Console
{
    public class SchemeTest
    {
        static void Main()
        {

        }

        static void Test()
        {
            var schemeName = "WebActivity";

            var schemeParam = SchemeCreateFactory.CreateScheme(schemeName);
        }
    }

    public class SchemeClass<TScheme> where TScheme : SchemeBase
    {
        [JsonProperty(PropertyName = "scheme")]
        public string SchemeType { get; set; }

        [JsonProperty(PropertyName = "scheme_params")]
        public TScheme Params { get; set; }
    }

    public class SchemeBase
    {
        public string SchemeName { get; private set; }

        public void BindingParam(dynamic param)
        {
            var type = typeof(SchemeBase);
        }

        public SchemeBase(string schemeName)
        {
            SchemeName = schemeName;
        }
    }

    public class WebActivityScheme : SchemeBase
    {
        public WebActivityScheme() : base("ChatActivity")
        {

        }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }

    public class ChatActivityScheme : SchemeBase
    {
        public ChatActivityScheme() : base("ChatActivity")
        {

        }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }

    public class SchemeCreateFactory
    {
        private static IDictionary<string, Type> _schemeParamDic;

        public SchemeCreateFactory()
        {
            _schemeParamDic = new Dictionary<string, Type>()
            {
                { "WebActivity", typeof(WebActivityScheme) },
                { "ChatActivity", typeof(ChatActivityScheme) }
            };
        }

        public static SchemeBase CreateScheme(string schemeName)
        {
            if (!_schemeParamDic.TryGetValue(schemeName, out var type))
            {
                throw new NotImplementedException();
            }

            return Activator.CreateInstance(type) as SchemeBase;
        }
    }
}

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SAED.Web.Utilities
{
    public class Session
    {
        public static T GetSession<T>(HttpContext httpContext, string nome)
        {
            return JsonConvert.DeserializeObject<T>(httpContext.Session.GetString(nome));
        }
    }
}

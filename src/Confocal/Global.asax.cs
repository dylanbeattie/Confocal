using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Confocal {
    public class MvcApplication : System.Web.HttpApplication {
        private const string COOKIE_NAME = "confocal.user";
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest() {
            var cookie = Request.Cookies[COOKIE_NAME];
            Guid guid;
            if (cookie == null || !Guid.TryParse(cookie.Value, out guid)) {
                guid = Guid.NewGuid();
                cookie = new HttpCookie(COOKIE_NAME, guid.ToString()) { Expires = DateTime.Now.AddYears(1) };
                Response.Cookies.Add(cookie);
            }
            Context.Items.Add("user-key", guid);
        }
    }
}

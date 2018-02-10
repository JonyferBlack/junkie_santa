using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;
using System.Web.UI;

namespace JunkieSanta
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings, new BugFixFriendlyUrlResolver());
        }
    }

    public class BugFixFriendlyUrlResolver : Microsoft.AspNet.FriendlyUrls.Resolvers.WebFormsFriendlyUrlResolver
    {
        protected override bool TrySetMobileMasterPage(HttpContextBase httpContext, Page page, string mobileSuffix)
        {
            return false;
            //return base.TrySetMobileMasterPage(httpContext, page, mobileSuffix);
        }
    }
}

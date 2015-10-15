using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakingSense.AspNet.Abstractions;
using MakingSense.AspNet.HypermediaApi.Linking;
using MakingSense.AspNet.HypermediaApi.Seed.Controllers;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Internal;

namespace MakingSense.AspNet.HypermediaApi.Seed
{
    public class SeedLinkHelper : BaseLinkHelper
    {
        public SeedLinkHelper([NotNull] IHttpContextAccessor httpContextAccessor, IActionContextAccessor actionContextAccessor, IUrlHelper urlHelper)
            : base(httpContextAccessor, actionContextAccessor, urlHelper)
        { }

        public override Maybe<Link> ToHomeAccount()
        {
            var username = HttpContext?.User?.Identity?.Name;
            return
                username == null ? ToAction<HomeController>(x => x.AccountHome(TemplateParameter.Create<string>()))
                : ToAction<HomeController>(x => x.AccountHome(username));
        }
    }
}

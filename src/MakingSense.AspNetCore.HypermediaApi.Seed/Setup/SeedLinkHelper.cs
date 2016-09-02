using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakingSense.AspNetCore.Abstractions;
using MakingSense.AspNetCore.HypermediaApi.Linking;
using MakingSense.AspNetCore.HypermediaApi.Seed.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Framework.Internal;

namespace MakingSense.AspNetCore.HypermediaApi.Seed
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

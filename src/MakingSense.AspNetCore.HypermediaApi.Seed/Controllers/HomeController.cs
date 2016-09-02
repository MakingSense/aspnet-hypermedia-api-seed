using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakingSense.AspNetCore.HypermediaApi.Linking;
using MakingSense.AspNetCore.HypermediaApi.Model;
using MakingSense.AspNetCore.HypermediaApi.Seed.Relations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Framework.Internal;

namespace MakingSense.AspNetCore.HypermediaApi.Seed.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly ILinkHelper _link;

        public HomeController([NotNull] ILogger<HomeController> logger, [NotNull] ILinkHelper linkHelper)
        {
            _logger = logger;
            _link = linkHelper;
        }

        private LinkCollection GenerateIndex()
        {
            return new LinkCollection()
            {
                _link.ToSelf(),
                _link.ToHomeAccount()
            };
        }

        [GetIndexRelation(Template = "/")]
        [AllowAnonymous]
        public MessageResult Index()
        {
            return new MessageResult()
            {
                message = "Welcome to Seed API!"
            }
            .AddLinks(GenerateIndex());
        }

        [GetAccountHomeRelation(Template = "/accounts/{accountName}")]
        public MessageResult AccountHome(string accountName)
        {
            return new MessageResult()
            {
                message = "Welcome to Seed API!"
            }
            .AddLinks(GenerateIndex());
        }
    }
}

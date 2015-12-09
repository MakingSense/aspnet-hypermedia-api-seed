using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakingSense.AspNet.HypermediaApi.Linking;
using MakingSense.AspNet.HypermediaApi.Model;
using MakingSense.AspNet.HypermediaApi.Seed.Relations;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Framework.Internal;

namespace MakingSense.AspNet.HypermediaApi.Seed.Controllers
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
        public MessageModel Index()
        {
            return new MessageModel()
            {
                message = "Welcome to Seed API!"
            }
            .AddLinks(GenerateIndex());
        }

        [GetAccountHomeRelation(Template = "/accounts/{accountName}")]
        public MessageModel AccountHome(string accountName)
        {
            return new MessageModel()
            {
                message = "Welcome to Seed API!"
            }
            .AddLinks(GenerateIndex());
        }
    }
}

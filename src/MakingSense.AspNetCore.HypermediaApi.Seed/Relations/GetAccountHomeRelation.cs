using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakingSense.AspNetCore.HypermediaApi.Linking;
using MakingSense.AspNetCore.HypermediaApi.Metadata;
using MakingSense.AspNetCore.HypermediaApi.Model;

namespace MakingSense.AspNetCore.HypermediaApi.Seed.Relations
{
    public class GetAccountHomeRelation : ActionRelationAttribute
    {
        public override HttpMethod? Method => HttpMethod.GET;
        public override Type OutputModel => typeof(MessageResult);
        public override string Description { get; set; } = "Get account home";
    }
}

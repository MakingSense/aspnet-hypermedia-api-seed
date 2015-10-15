using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakingSense.AspNet.HypermediaApi.Linking;
using MakingSense.AspNet.HypermediaApi.Metadata;
using MakingSense.AspNet.HypermediaApi.Model;

namespace MakingSense.AspNet.HypermediaApi.Seed.Relations
{
    public class GetIndexRelation : ActionRelationAttribute
    {
        public override HttpMethod? Method => HttpMethod.GET;
        public override Type OutputModel => typeof(MessageModel);
        public override string Description { get; set; } = "Get API index";
    }
}

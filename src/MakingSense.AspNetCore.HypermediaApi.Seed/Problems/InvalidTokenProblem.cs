using MakingSense.AspNetCore.HypermediaApi.Problems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakingSense.AspNetCore.HypermediaApi.Seed.Problems
{
    public class InvalidTokenProblem : AuthenticationProblem
    {
        public override string title => "Invalid token";
        public override string detail => "Authentication Token is not valid";
        public override int errorCode => 1;
    }
}

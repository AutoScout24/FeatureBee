using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using FeatureBee.Configuration;

namespace FeatureBee.Evaluators
{
    internal class ClaimConditionEvaluator : IConditionEvaluator<WebApplicationContext>
    {
        public string Name { get { return "claim"; } }
        public bool IsFulfilled(string[] values)
        {
            var principal = Thread.CurrentPrincipal as ClaimsPrincipal;
            return principal != null &&
                   values.Any(claimType => principal.HasClaim(Match(claimType)));
        }

        private static Predicate<Claim> Match(string claimType)
        {
            return claim => claim.Type.Equals(claimType, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
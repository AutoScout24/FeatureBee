using System.Linq;
using System.Web;
using FeatureBee.Configuration;

namespace FeatureBee.Evaluators
{
    internal class BrowserConditionEvaluator : IConditionEvaluator<WebApplicationContext>
    {
        public string Name { get { return "browser"; } }
        public bool IsFulfilled(string[] values)
        {
            var currentContext = HttpContext.Current;
            return currentContext != null &&
                   values.Any(condition => currentContext.Request.Browser.Browser.ToLowerInvariant().Contains(condition));
        }
    }
}
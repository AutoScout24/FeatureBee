using System;
using System.Linq;
using System.Threading;
using System.Web;
using FeatureBee.Configuration;

namespace FeatureBee.Evaluators
{
    internal class CultureConditionEvaluator : IConditionEvaluator
    {
        public string Name { get { return "culture"; } }
        public bool IsFulfilled(string[] values)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
            return values.Any(x => x.Equals(currentCulture, StringComparison.InvariantCultureIgnoreCase));
        }
    }

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
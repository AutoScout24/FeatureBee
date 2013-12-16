using System;

namespace FeatureBee.Client.Evaluators
{
    internal class BrowserConditionEvaluator : IConditionEvaluator<WebApplicationContext>
    {
        public string Name { get { return "BrowserConditionEvaluator"; } }
        public bool IsFulfilled(object value)
        {
            throw new NotImplementedException();
        }
    }
}
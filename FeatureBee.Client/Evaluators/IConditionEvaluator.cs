using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureBee.Client.Evaluators
{
    public interface IConditionEvaluator<T> : IConditionEvaluator
    {
        
    }

    public interface IConditionEvaluator
    {
        string Name { get; }
        bool IsFulfilled(object value);
    }
}

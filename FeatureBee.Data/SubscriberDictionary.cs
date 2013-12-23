namespace FeatureBee.Data
{
    using System.Collections.Generic;

    internal class SubscriberDictionary : Dictionary<string, ISubscribe>
    {
    }
}
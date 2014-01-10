namespace FeatureBee.Server.Domain.Infrastruture
{
    class FeatureConditionValueHandler
    {
        public static string Concat(params string[] values)
        {
            return string.Join("-", values);
        }
    }
}
namespace FeatureBee.Server.Domain.Infrastruture
{
    class FeatureConditionValueHandler
    {
        public static string Concat(params string[] values)
        {
            return values != null ? string.Join("-", values) : string.Empty;
        }
    }
}
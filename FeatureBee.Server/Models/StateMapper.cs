namespace FeatureBee.Server.Models
{
    public class StateMapper
    {
        public string Map(int index)
        {
            switch (index)
            {
                case 1:
                    return "Released";
                case 2:
                    return "Under Test";
                case 0:
                default:
                    return "In Development";
            }
        }
    }
}
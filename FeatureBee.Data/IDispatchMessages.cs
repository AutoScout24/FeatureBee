namespace FeatureBee.Data
{
    public interface IDispatchMessages
    {
        void DispatchCommit<TCommit>(TCommit commit);
    }
}
namespace FeatureBee.Data
{
    public interface ISubscribe
    {
        void Notify(object @event);
    }
}
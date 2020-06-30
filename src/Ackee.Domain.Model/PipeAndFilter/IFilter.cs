namespace Ackee.Domain.Model.PipeAndFilter
{
    public interface IFilter<T> where T : class
    {
        void Invoke(T message);
    }
}
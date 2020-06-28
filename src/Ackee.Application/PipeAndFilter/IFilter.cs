namespace Ackee.Application.PipeAndFilter
{
    public interface IAckeeFilter<T> where T :class
    {
        void Invoke(T data);
    }
}
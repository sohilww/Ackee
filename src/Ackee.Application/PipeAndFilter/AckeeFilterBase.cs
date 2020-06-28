using System;

namespace Ackee.Application.PipeAndFilter
{
    public abstract class AckeeFilterBase<T> : IAckeeFilter<T> where T : class
    {
        private readonly IAckeeFilter<T> _nextFilter;
        private readonly Action<T> _done;

        protected AckeeFilterBase(IAckeeFilter<T> nextFilter, Action<T> done)
        {
            _nextFilter = nextFilter;
            _done = done;
        }

        public virtual void Invoke(T data)
        {
            Do(data);
            if (_nextFilter != null)
                _nextFilter.Invoke(data);
            else
                _done.Invoke(data);
        }

        protected abstract void Do(T data);

    }
}
using System;

namespace Ackee.Domain.Model.PipeAndFilter
{
    public abstract class FilterBase<T> : IFilter<T> where T : class
    {
        private readonly IFilter<T> _nextFilter;
        private readonly Action<T> _done;

        protected FilterBase(IFilter<T> nextFilter, Action<T> done)
        {
            _nextFilter = nextFilter;
            _done = done;
        }

        public virtual void Invoke(T message)
        {
            Do(message);
            if (_nextFilter != null)
                _nextFilter.Invoke(message);
            else
                _done.Invoke(message);
        }

        protected abstract void Do(T message);

    }
}
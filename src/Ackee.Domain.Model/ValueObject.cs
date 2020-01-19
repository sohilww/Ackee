using Ackee.Core;

namespace Ackee.Domain.Model
{
    public abstract class ValueObject
    {
        public override int GetHashCode()
        {
            return HashCodeBuilder.ReflectionHashCode(this);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return EqualsBuilder.ReflectionEquals(obj, this);
        }
    }
    
}
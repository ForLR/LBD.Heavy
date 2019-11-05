using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Domain.Core.Model
{
    public abstract class ValueObject<T> where T:ValueObject<T>
    {
        public override bool Equals(object obj)
        {
            var compareTo = obj as ValueObject<T>;
            return !ReferenceEquals(this,obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }
    }
}

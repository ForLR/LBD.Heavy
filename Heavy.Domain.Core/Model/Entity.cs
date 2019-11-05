using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Domain.Core.Model
{
    public abstract class Entity
    {

        public Guid Id { get { return Guid.NewGuid(); } set { Id = value; } }

        public DateTime CreateDateTime { get { return DateTime.Now; } set { this.CreateDateTime = value; } }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;
            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;
            return Id.Equals(compareTo.Id);    
        }
        public static bool operator ==(Entity left,Entity right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null)) return true;

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }
        public static bool operator !=(Entity left, Entity right)
        {
            return !(left==right);
        }
        public override int GetHashCode()
        {
            return (this.GetHashCode() * 907) + Id.GetHashCode();
        }


        public override string ToString()
        {
            return this.GetType().Name + $"Id={this.Id}";
        }
    }
}

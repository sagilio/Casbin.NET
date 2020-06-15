using System;
using System.Collections;
using System.Collections.Generic;

namespace NetCasbin.Model
{
    public class Rule : List<string>
    {
        protected bool Equals(Rule other)
        {
            if (Count != other.Count)
            {
                return false;
            }

            for (var i = 0; i < Count; i++)
            {
                if (!this[i].Equals(other[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Rule)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 0;
                foreach (var item in this)
                {
                    if (hashCode == 0)
                    {
                        hashCode = item != null ? item.GetHashCode() : 0;
                    }
                    hashCode = (hashCode  * 397) ^ (item != null ? item.GetHashCode() : 0);
                }
                return hashCode;
            }
        }

        public static bool operator ==(Rule left, Rule right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Rule left, Rule right)
        {
            return !Equals(left, right);
        }
    }
}
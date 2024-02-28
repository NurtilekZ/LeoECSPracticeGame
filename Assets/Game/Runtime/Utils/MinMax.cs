using System;

namespace Game.Runtime.Utils
{
    [Serializable]
    public class MinMaxFloat : IEquatable<MinMaxFloat>
    {
        public float Min;
        public float Max;

        public MinMaxFloat(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public bool Equals(MinMaxFloat other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Min.Equals(other.Min) && Max.Equals(other.Max);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MinMaxFloat)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Min, Max);
        }
    }
}
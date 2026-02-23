namespace QuantityMeasurementApp.Models
{
    public class Inches
    {
        public double Value { get; }

        public Inches(double value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Inches other = (Inches)obj;
            return Math.Abs(Value - other.Value) < 0.0001;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}

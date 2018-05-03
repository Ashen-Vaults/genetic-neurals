using System;

namespace AshenCode.NeuralNetwork
{
    public class Fitness : IComparable<Fitness>
    {
        private float _value;
        public float Value
        {
            get { return _value; }
        }

        public void Set(float value)
        {
            _value = value;
        }

        public void Modify(float value)
        {
            _value += value;
        }

        public int CompareTo(Fitness obj)
        {
            if (obj == null) return 1;

            if (Value > obj.Value)
            {
                return 1;
            }
            else if (Value < obj.Value)
            {
                return -1;
            }
            else return 0;
        }
    }
}
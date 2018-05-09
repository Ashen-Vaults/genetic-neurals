using System;
using UnityEngine;

namespace AshenCode.NeuralNetwork
{
    [Serializable]
    public class Fitness : IComparable<Fitness>
    {
        [SerializeField]
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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
using System; 
using System.Linq;
using System.Collections; 
using System.Collections.Generic; 
using UnityEngine;

namespace AshenCode.FloopyBirb.World
{
    public class Obstacle : MonoBehaviour
    {
        private List<IEffectable> _effects;

        public Transform top;
        public Transform bottom;

        public void Activate(GameObject g)
        {
            if(_effects != null)
            {
                _effects.ForEach(e => e.Run(g));
            }
        }
    }
}
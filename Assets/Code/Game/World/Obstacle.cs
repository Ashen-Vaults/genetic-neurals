using System; 
using System.Linq;
using System.Collections; 
using System.Collections.Generic; 
using UnityEngine;
using Effects;

public class Obstacle:MonoBehaviour
{
    private List<IEffectable> _effects;

    public void Activate(GameObject g)
    {
        if(_effects != null)
        {
            _effects.ForEach(e => e.Run(g));
        }
    }
}
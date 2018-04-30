using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{

    public class Stop : CollisionEffect
    {

        public override void Run(GameObject g)
        {
            g.GetComponent<Renderer>().material.color = Color.red;
        }

    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AshenCode.FloopyBirb.World
{

    public class Stop : CollisionEffect
    {

        public override void Run(GameObject g)
        {
            g.GetComponent<Renderer>().material.color = Color.red;
            //TEMP
            g.GetComponent<Agents.BirdView>().Death();
        }

    }

}


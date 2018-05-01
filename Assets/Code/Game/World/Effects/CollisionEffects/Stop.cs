using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{

    public class Stop : CollisionEffect
    {

        public override void Run(GameObject g)
        {
            Rigidbody2D rigidbody = g.GetComponent<Rigidbody2D>();
            if(rigidbody != null)
            {
              //  rigidbody.velocity += new Vector2(-1, 5);
            }
            g.GetComponent<Renderer>().material.color = Color.red;
        }

    }

}


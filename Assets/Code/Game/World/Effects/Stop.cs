using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace effects
{

    public class Stop : IEffectable
    {

		//TODO: actually effect the gameobject speed
		//color is for testing
		public void Run(GameObject g)
		{
            g.GetComponent<Renderer>().material.color = Color.red;
        }

    }

}


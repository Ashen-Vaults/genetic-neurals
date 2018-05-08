using System;
using UnityEngine;

namespace AshenCode.FloopyBirb.Agents
{
    public class BirdHumanController : IControllable
    {

        

        public void Control(Transform transform, Action callback)
        {
            //TODO: Listen for input
            //get duration of spacebar being held down

            if (Input.GetKeyDown("space"))
            {
                Debug.Log(this+ " jump");
                if(callback != null)
                {
                    callback();
                }
            }
        }
    }
}
using System;
using UnityEngine;

namespace AshenCode.FloopyBirb.Bird
{
    public class BirdHumanController : IControllable
    {

        

        public void Control(Transform transform, Action callback)
        {
            //TODO: Listen for input
            //get duration of spacebar being held down
            if(callback != null)
            {
                callback();
            }
        }
    }
}
using UnityEngine;

namespace AshenCode.FloopyBirb.Bird
{
    public class Bird : MonoBehaviour
    {
        
        private IControllable controller;

        void Update()
        {
            if(controller != null)
            {
                controller.Control(this.transform);
            }
        }

    }
}
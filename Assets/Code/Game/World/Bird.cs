using System;
using System.Collections.Generic;
using UnityEngine;

namespace AshenCode.FloopyBirb.Bird
{
    public class Bird : MonoBehaviour
    {
        
        private IControllable controller;

        public Action onControl;

        void Awake()
        {
            Subscribe();
        }

        void OnDestroy()
        {
            Death();
        }

        void Subscribe()
        {
            onControl += Jump;
        }

        void Unsubscribe()
        {
            onControl -= Jump;
        }

        void Update()
        {
            if(controller != null)
            {
                controller.Control(this.transform, onControl);
            }
        }

        void Death()
        {
            Unsubscribe(); 
        }


        public void Jump()
        {

        }

    }
}
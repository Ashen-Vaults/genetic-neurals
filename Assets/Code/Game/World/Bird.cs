using System;
using System.Collections.Generic;
using UnityEngine;

namespace AshenCode.FloopyBirb.Bird
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bird : MonoBehaviour
    {
        
        private  Rigidbody2D _rigidBody;

        private IControllable controller;

        public Action onControl;

        void Awake()
        {
            _rigidBody = this.GetComponent<Rigidbody2D>();
            Subscribe();
        }

        void OnDestroy()
        {
            Death();
        }

        public void Init(IControllable controller)
        {
            this.controller = controller;
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
            print(this+ " jump");
            _rigidBody.AddForce(Vector2.up * 25, ForceMode2D.Force);
        }

    }
}
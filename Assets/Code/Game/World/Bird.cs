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

        [SerializeField]
        private float _tiltSmooth = 5f;

        [SerializeField]
        private float _force;

        void Awake()
        {
            _rigidBody = this.GetComponent<Rigidbody2D>();
            Subscribe();
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

            Fall();
        }

        public void Death()
        {
            Unsubscribe();
            this.gameObject.SetActive(false);
            Destroy(this); 
        }


        void OnCollisionEnter2D(Collision2D other)
        {
            Death();
        }


        public void Jump()
        {
            print(this+ " jump");
            _rigidBody.AddForce(Vector2.up * _force, ForceMode2D.Force);
        }

        private void Fall()
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0,-90),_tiltSmooth * Time.deltaTime);
        }

    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AshenCode.FloopyBirb.Agents
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bird : MonoBehaviour
    {
        
        private  Rigidbody2D _rigidBody;

        public IControllable controller;

        public Action onControl;

        public Action onDeath;

        [SerializeField]
        private float _tiltSmooth = 5f;

        [SerializeField]
        private float _force;

        public float score;

        Coroutine _scoreRoutine;

        void Awake()
        {
            _rigidBody = this.GetComponent<Rigidbody2D>();
            Subscribe();
        }

        public void Init(IControllable controller, Action<IControllable> callback)
        {
            this.controller = controller;

            onDeath += () => { callback(this.controller);};

            _scoreRoutine = StartCoroutine(UpdateScore());           
        }

        void Subscribe()
        {
            onControl += Jump;
        }

        void Unsubscribe()
        {
            onControl -= Jump;
            onDeath = null;
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

            if(onDeath != null)
                onDeath();

            this.gameObject.SetActive(false);

            if(_scoreRoutine != null)
            {
                StopCoroutine(_scoreRoutine);
            }

            Unsubscribe();

//            Destroy(this.gameObject); 
        }


        void OnCollisionEnter2D(Collision2D other)
        {
            Death();
        }

        IEnumerator UpdateScore()
        {
            score += 100;
            yield return new WaitForSeconds(0.1f);
        }


        public void Jump()
        {
            _rigidBody.AddForce(Vector2.up * _force, ForceMode2D.Force);
        }

        private void Fall()
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0,-90),_tiltSmooth * Time.deltaTime);
        }

    }
}
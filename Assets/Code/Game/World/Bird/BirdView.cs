using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AshenCode.FloopyBirb.Agents
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class BirdView : MonoBehaviour
    {
        Rigidbody2D _rigidBody;

        Coroutine _scoreRoutine;
        Coroutine _loopRoutine;

        public Bird bird;

        void Awake()
        {
            _rigidBody = this.GetComponent<Rigidbody2D>();
            bird = new Bird();
        }

        public void Init(IEnumerator Score, IEnumerator loop)
        {
            _scoreRoutine = StartCoroutine(Score);   
            _loopRoutine = StartCoroutine(loop);    
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            Death();
        }


        public void Death()
        {
            this.gameObject.SetActive(false);

            if(_scoreRoutine != null)
            {
                StopCoroutine(_scoreRoutine);
            }

            if(_loopRoutine != null)
            {
                StopCoroutine(_loopRoutine);
            }

            Destroy(this.gameObject);     
        }
        
        public void Jump(float force)
        {
            _rigidBody.AddForce(Vector2.up * force, ForceMode2D.Force);
        }

        public void Fall(float tiltSmooth)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0,-90),tiltSmooth * Time.deltaTime);
        }
        
        
    }
}
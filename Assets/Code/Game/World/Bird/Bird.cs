using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AshenCode.FloopyBirb.Agents
{
    [Serializable]
    public class Bird
    {
        
        public IControllable controller;

        public BirdView view;

        public Action onControl;

        [SerializeField]
        private float _tiltSmooth = 5f;

        [SerializeField]
        private float _force = 25;

        public float score;


        public void Init(BirdView view, IControllable controller, Action<IControllable> callback)
        {
            Subscribe();
            this.view = view;
            this.view.Init(UpdateScore(), Update(),  () => { callback(this.controller); Unsubscribe(); });
            this.controller = controller;
                   
        }

        void Subscribe()
        {
            onControl += () => { view.Jump(this._force);};
        }

        void Unsubscribe()
        {
            onControl = null;
        }

        IEnumerator Update()
        {
            while(true)
            {
                if(controller != null)
                {
                    controller.Control(view.transform, onControl);
                }

                view.Fall(_tiltSmooth);
                yield return null;
            }
        }

        public IEnumerator UpdateScore()
        {
            score += 100;
            yield return new WaitForSeconds(0.1f);
        }

    }
}
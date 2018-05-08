using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AshenCode.FloopyBirb.World
{

    public class CollisionEffect : MonoBehaviour, IEffectable
    {
        public virtual void Run(GameObject g){}

        /// <summary>
        /// OnCollisionEnter is called when this collider/rigidbody has begun
        /// touching another rigidbody/collider.
        /// </summary>
        /// <param name="other">The Collision data associated with this collision.</param>
        /// <summary>
        /// Sent when an incoming collider makes contact with this object's
        /// collider (2D physics only).
        /// </summary>
        /// <param name="other">The Collision2D data associated with this collision.</param>
        void OnCollisionEnter2D(Collision2D other)
        {
            if (other != null && other.gameObject != null)
            {
                this.Run(other.gameObject);
            }
        }
    }

}
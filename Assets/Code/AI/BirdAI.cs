using UnityEngine;
using System.Collections.Generic;

using AshenCode.NeuralNetwork;
using AshenCode.FloopyBirb.World;
using System;

namespace AshenCode.FloopyBirb.Bird
{
    public class BirdAIController : IControllable
    {
        NeuralNetwork.NeuralNetwork network;

        List<Action> _actions;

        UnityEngine.Random rand;


        public BirdAIController()
        {

            rand = new UnityEngine.Random();
            List<int> inputs = new List<int>()
            {
                4, 4, 2
               //transform.position.y,
               // obstacle.transform.position.x,
               // obstacle.top.position.y,
               // obstacle.bottom.position.y
            };
            this.network = new NeuralNetwork.NeuralNetwork(inputs); 
        }

        public void Control(Transform transform, Action callback)
        {
            this.Think(transform, FindClosestObstacle(transform), callback);
        }


        public void Subscribe(List<Action> actions)
        {
            if(actions != null)
            {
                _actions = actions;
            }
        }

        public void Think(Transform transform, Obstacle obstacle, Action callback)
        {
            ///  y location of bird
            ///  x location the closest pipe
            ///  y location of the top pipe
            ///  y location of bottom pipe
            // normalize data

            /*
                UnityEngine.Random.Range(0f,1f),
                UnityEngine.Random.Range(0f,1f),
                UnityEngine.Random.Range(0f,1f),
                UnityEngine.Random.Range(0f,1f)
             */

             

            if(obstacle != null)
            {

                //obstacle.GetComponentInChildren<SpriteRenderer>().color = Color.blue;

                float[] inputs = new float[]
                {
                    transform.position.y,
                    obstacle.top.position.y,
                    obstacle.bottom.position.y,
                    obstacle.transform.position.x
                };

   

            //   Debug.Log(transform.position.y + "\n" 
            //    +  obstacle.transform.position.x + " " + obstacle.top.position.y + " "+ obstacle.bottom.position.y );

                float[] output = network.FeedForward(inputs);

              //  Debug.Log(output[0]);

                if(output[0] > output[1])
                {
                    //Spacebar pressed
                    if(callback != null)
                    {
                        callback();
                    }
                }
            }
        }

        private Obstacle FindClosestObstacle(Transform transform)
        {


            Transform closest = null;
            float closestDistance = Mathf.Infinity;
            Obstacle closestObstacle = null;

            for(int i = 0; i< Parallax.poolObjects.Length; i++)
            {
                float distance = Parallax.poolObjects[i].transform.position.x - transform.position.x;
                if(distance < closestDistance && distance > 0)
                {
                    closest = Parallax.poolObjects[i].transform;
                    closestDistance = distance;
                    if(closest.GetComponent<Obstacle>() != null)
                    {
                        closestObstacle = closest.GetComponent<Obstacle>();
                    }
                }
            }

            return closestObstacle;

        }
    }
}
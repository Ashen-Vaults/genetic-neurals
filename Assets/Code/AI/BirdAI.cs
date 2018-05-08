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

        public void Control(Transform transform, Action callback)
        {
            this.Think(transform, FindClosestObstacle(), callback);
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
            float[] inputs = new float[]
            {
                transform.position.y,
                obstacle.transform.position.x,
                obstacle.top.position.y,
                obstacle.bottom.position.y
            };

            float[] output = network.FeedForward(inputs);

            if(output[0] > 0.0f)
            {
                Debug.Log("AI DONE IT");
                //Spacebar pressed
                if(callback != null)
                {
                    callback();
                }
            }
        }

        public Obstacle FindClosestObstacle()
        {
            return null;
        }
    }
}
using UnityEngine;
using System.Collections.Generic;

using AshenCode.NeuralNetwork;
using AshenCode.FloopyBirb.World;

namespace AshenCode.FloopyBirb.Bird
{
    public class BirdAIController : IControllable
    {
        NeuralNetwork.NeuralNetwork network;

        public void Control(Transform transform)
        {
            this.Think(transform, FindClosestObstacle());
        }

        public void Think(Transform transform, Obstacle obstacle )
        {
            ///  y location of bird
            ///  x location the closest pipe
            ///  y location of the top pipe
            ///  y location of bottom pipe
            // normalize data
            float[] inputs = new float[]
            {
                transform.position.y,
                obstacle.transform.position.y,
                obstacle.transform.position.y,
                obstacle.transform.position.y
            };

            float[] output = network.FeedForward(inputs);

            if(output[0] > 0.5f)
            {
                //jump
            }
        }

        public Obstacle FindClosestObstacle()
        {
            return null;
        } 

    }
}
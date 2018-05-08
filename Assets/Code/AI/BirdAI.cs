using UnityEngine;
using System.Collections.Generic;

using AshenCode.NeuralNetwork;
using AshenCode.FloopyBirb.World;

namespace AshenCode.FloopyBirb.Bird
{
    public class BirdAI : Bird
    {

        NeuralNetwork.NeuralNetwork network;

        void Update()
        {
            Think(FindClosestObstacle());
        }

        public void Think( Obstacle obstacle )
        {
            ///  y location of bird
            ///  x location the closest pipe
            ///  y location of the top pipe
            ///  y location of bottom pipe
            // normalize data
            float[] inputs = new float[]
            {
                this.transform.position.y,
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
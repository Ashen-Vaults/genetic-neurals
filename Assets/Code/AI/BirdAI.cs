using UnityEngine;
using System.Collections.Generic;

using AshenCode.NeuralNetwork;
using AshenCode.FloopyBirb.World;
using AshenCode.World.Level;
using System;

namespace AshenCode.FloopyBirb.Agents
{
    public class BirdAIController : IControllable
    {
        [SerializeField]
        NeuralNetwork.NeuralNetwork network;

        List<Action> _actions;

        UnityEngine.Random rand;


        public BirdAIController(NeuralNetwork.NeuralNetwork net = null)
        {

            if(net == null)
            {
                rand = new UnityEngine.Random();
                List<int> inputs = new List<int>()
                {
                    4, 4, 2
                };
                this.network = new NeuralNetwork.NeuralNetwork(inputs); 
            }
            else
            {
                this.network = net;
            }

        }

        public void Control(Transform transform, Action callback)
        {
            this.Think(transform, FindClosestObstacle(transform), callback);
        }

        public NeuralNetwork.NeuralNetwork GetNetwork()
        {
            return this.network;
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
            if(obstacle != null)
            {
                float[] inputs = new float[]
                {
                    transform.position.y,
                    obstacle.top.position.y,
                    obstacle.bottom.position.y,
                    obstacle.transform.position.x
                };

                float[] output = network.FeedForward(inputs);

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

        public void UpdateFitness(float fitness)
        {
            if(this.network != null && this.network.fitness != null)
            {
                this.network.fitness.Modify(fitness);
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
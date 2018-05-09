using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using AshenCode.FloopyBirb.Agents;

namespace AshenCode.FloopyBirb.World
{
    public class Game : MonoBehaviour
    {
        [SerializeField]
        private int _seed;

        [SerializeField]
        private int _birdCount;

        [SerializeField]
        private List<Agents.Bird> _birds;

        [SerializeField]
        private GameObject _birdPrefab;

        private bool _aiControlled;

        [SerializeField]
        private Transform _startPos;

        private Action gameOver;

        [SerializeField]
        private List<NeuralNetwork.NeuralNetwork> _birdDNA = new List<NeuralNetwork.NeuralNetwork>();

        [SerializeField]
        private float _mutateRate = 1000f;

        [SerializeField]
        private int _generation = 0;

        [SerializeField]
        private Parallax _parallax;

        void Awake()
        {
            InitSimulation();
        }

        void InitSimulation(List<NeuralNetwork.NeuralNetwork> nets = null)
        {
            _generation += 1;
            UnityEngine.Random.InitState(_seed);
            _parallax.Init();

            for(int i = 0; i < _birdCount; i++)
            {
                GameObject bird = Instantiate(_birdPrefab);
                bird.transform.position =_startPos.position;
                Agents.Bird b = bird.GetComponent<Agents.Bird>();

                if(nets != null)
                {
                    b.Init(new BirdAIController(nets[i]), c => 
                    {
                        OnBirdDeath(b);
                    } );
                }
                else
                {
                    b.Init(new BirdAIController(), c => 
                    {
                        OnBirdDeath(b);
                    } );
                }

                _birds.Add(b);
            }
        }

        void OnBirdDeath(Bird b)
        {
            _birds.Remove(b);
            _birdDNA.Add(b.controller.GetNetwork());
            EvolveAgents();
            if(NewGeneration(_birds)) NextGeneration();
        }

        void SaveBirdDNA(IControllable controller)
        {
            NeuralNetwork.NeuralNetwork n = new NeuralNetwork.NeuralNetwork(controller.GetNetwork());
            _birdDNA.Add(n);
        }

        bool NewGeneration(List<Agents.Bird> birds)
        {
            if(birds.Count == 0)
            {
                return true;
            }
            return false;
        }

        void NextGeneration()
        {

            UpdateFitness(_birds);
            InitSimulation( EvolveAgents());
            _birdDNA.Clear();

        }

        void UpdateFitness(List<Bird> birds)
        {
            float sum = 0f;
            sum = birds.Sum( b => b.score);
            birds.ForEach(b => b.controller.UpdateFitness(b.score / sum));
        }

        List<NeuralNetwork.NeuralNetwork> EvolveAgents()
        {
            List<NeuralNetwork.NeuralNetwork> nets = _birdDNA;//.Select( b => b.controller.GetNetwork()).ToList();

            nets.Sort();

            for(int i =0; i < _birdDNA.Count / 2; i++)
            {

                nets[i] = new NeuralNetwork.NeuralNetwork(nets[i+(_birdDNA.Count / 2)]);
                nets[i].Mutate();
                nets[i + (_birdDNA.Count / 2)] = new NeuralNetwork.NeuralNetwork(nets[i + (_birdDNA.Count / 2)]);

              //  _birdDNA[i] = new NeuralNetwork.NeuralNetwork(_birds[i+(_birdCount / 2)].controller.GetNetwork());
              //  _birdDNA[i].Mutate();
              //  _birdDNA[i + (_birdCount / 2)] = new NeuralNetwork.NeuralNetwork(_birds[i + (_birdCount / 2)].controller.GetNetwork());
            }
            //_birds.ForEach(b => b.controller.GetNetwork().fitness.Set(0));

            return nets;
        }
    }
}
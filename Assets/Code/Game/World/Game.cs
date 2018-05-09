using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using AshenCode.FloopyBirb.Agents;
using AshenCode.World.Level;

namespace AshenCode.FloopyBirb.World
{
    public class Game : MonoBehaviour
    {
        [SerializeField]
        private int _seed;

        [SerializeField]
        private int _birdCount;

        [SerializeField]
        private GameObject _birdPrefab;

        private bool _aiControlled;

        [SerializeField]
        private Transform _startPos;

        private Action gameOver;


        [SerializeField]
        private float _mutateRate = 1000f;

        [SerializeField]
        private int _generation = 0;

        [SerializeField]
        private Parallax _parallax;

        [SerializeField]
        private List<Bird> _birds;

       [SerializeField]
        private List<Bird> _savedBirds = new List<Bird>();

        void Awake()
        {
            InitSimulation();
        }

        void InitSimulation(List<NeuralNetwork.NeuralNetwork> nets = null)
        {
            _birds.Clear();
            _generation += 1;
            UnityEngine.Random.InitState(_seed);
            _parallax.Init();

            for(int i = 0; i < _birdCount; i++)
            {
                GameObject bird = Instantiate(_birdPrefab);
                bird.transform.position =_startPos.position;
                Agents.BirdView view = bird.GetComponent<Agents.BirdView>();
                Agents.Bird b = view.bird;

                if(nets != null)
                {
                    b.Init(view, new BirdAIController(nets[i]), c => 
                    {
                        OnBirdDeath(b);
                    } );
                }
                else
                {
                    b.Init(view, new BirdAIController(), c => 
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
            _savedBirds.Add(b);
           // EvolveAgents();
            if(NewGeneration(_birds)) NextGeneration();
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

            UpdateFitness(_savedBirds);
            InitSimulation();
            //InitSimulation( EvolveAgents()); //TODO: fix Breaks next generations
            _savedBirds.Clear();

        }

        void UpdateFitness(List<Bird> birds)
        {
            float sum = 0f;
            sum = birds.Sum( b => b.score);
            birds.ForEach(b => b.controller.UpdateFitness(b.score / sum));
        }

        List<NeuralNetwork.NeuralNetwork> EvolveAgents()
        {
            List<NeuralNetwork.NeuralNetwork> nets = _savedBirds.Select( b => b.controller.GetNetwork()).ToList();

            nets.Sort();

            for(int i =0; i < _savedBirds.Count / 2; i++)
            {

                nets[i] = new NeuralNetwork.NeuralNetwork(nets[i+(_savedBirds.Count / 2)]);
                nets[i].Mutate();
                nets[i + (_savedBirds.Count / 2)] = new NeuralNetwork.NeuralNetwork(nets[i + (_savedBirds.Count / 2)]);

              //  _birdDNA[i] = new NeuralNetwork.NeuralNetwork(_birds[i+(_birdCount / 2)].controller.GetNetwork());
              //  _birdDNA[i].Mutate();
              //  _birdDNA[i + (_birdCount / 2)] = new NeuralNetwork.NeuralNetwork(_birds[i + (_birdCount / 2)].controller.GetNetwork());
            }
            //_birds.ForEach(b => b.controller.GetNetwork().fitness.Set(0));

            return nets;
        }
    }
}
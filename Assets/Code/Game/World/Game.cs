using System;
using System.Collections.Generic;
using UnityEngine;

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

        void Awake()
        {
            InitSimulation();
        }

        void InitSimulation()
        {
            UnityEngine.Random.InitState(_seed);

            for(int i = 0; i < _birdCount; i++)
            {
                GameObject bird = Instantiate(_birdPrefab, _startPos);
                Agents.Bird b = bird.GetComponent<Agents.Bird>();
                b.Init(new BirdAIController(), SaveBirdDNA );
                _birds.Add(b);
            }
        }

        void SaveBirdDNA(IControllable controller)
        {
            NeuralNetwork.NeuralNetwork n = new NeuralNetwork.NeuralNetwork(controller.GetNetwork());
            _birdDNA.Add(n);
        }
    }
}
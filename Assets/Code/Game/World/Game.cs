using System;
using System.Collections.Generic;
using UnityEngine;

using AshenCode.FloopyBirb.Bird;

namespace AshenCode.FloopyBirb.World
{
    public class Game : MonoBehaviour
    {
        [SerializeField]
        private int _seed;

        [SerializeField]
        private int _birdCount;

        [SerializeField]
        private List<Bird.Bird> _birds;

        [SerializeField]
        private GameObject _birdPrefab;

        void Awake()
        {
            Init();
        }

        void Init()
        {
            UnityEngine.Random.InitState(_seed);

            for(int i = 0; i < _birdCount; i++)
            {
                GameObject bird = Instantiate(_birdPrefab);
                Bird.Bird b = bird.GetComponent<Bird.Bird>();
                b.Init(new BirdAIController());
                _birds.Add(b);
            }
        }
    }
}
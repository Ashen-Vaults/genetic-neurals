using System;
using UnityEngine;

namespace genetic_neurals.Assets.Code.Game.World
{
    public class Game : MonoBehaviour
    {
        [SerializeField]
        private int seed;

        void Awake()
        {
            Init();
        }

        void Init()
        {
            UnityEngine.Random.InitState(seed);
        }
    }
}
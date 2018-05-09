using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace AshenCode.NeuralNetwork
{

    [Serializable]
    public class NeuralNetwork : IComparable<NeuralNetwork>, INeurable
    {
        //Feed in layers
        [SerializeField]
        List<int> _layers;
        [SerializeField]
        float[][] _neurons;
        float[][][] _weights;
        System.Random _random;
        public Fitness fitness;
        [SerializeField]
        private List<Mutation> _mutations;

        /// <summary>
        ///  y location of bird
        ///  x location the closest pipe
        ///  y location of the top pipe
        ///  y location of bottom pipe
        /// 
        ///  4 inputs
        /// 
        ///  4 hidden nodes
        ///  
        ///  1 output (jump or not)
        /// </summary>
        /// <param name="layers"></param>
        public NeuralNetwork(List<int> layers)
        {
            fitness = new Fitness();
            _mutations = new List<Mutation>();
            _random = new System.Random(System.DateTime.Today.Millisecond);
            _layers = layers;
            _neurons = CreateNeurons(_layers);
            _weights = CreateWeights(_layers);   

            //TODO: Store in json or external file
            _mutations.Add(new Mutation( x => x <= 2, f => {return f *= -1f;} ));
            _mutations.Add(new Mutation( x => x <= 4, f => {return UnityEngine.Random.Range(-0.5f, 0.5f);} ));
            _mutations.Add(new Mutation( x => x <= 6, f => {return f *= UnityEngine.Random.Range(0f, 1f) + 1f;} ));
            _mutations.Add(new Mutation( x => x <= 8, f => {return f *= UnityEngine.Random.Range(0f, 1f);} ));
            _mutations.Add(new Mutation( x => true  , f => {return 0;} ));    
        }

        private float[][] CreateNeurons(List<int> layers)
        {
            List<float[]> neurons = new List<float[]>();

            for (int i = 0; i < layers.Count; i++)
            {
                neurons.Add(new float[layers[i]]);
            }

            return neurons.ToArray();
        }

        private float[][][] CreateWeights(List<int> layers)
        {
            List<float[][]> weights = new List<float[][]>();

            for (int i = 1; i < layers.Count;i++)
            {
                List<float[]> layerWeights = new List<float[]>();

                int prevNeurons = layers[i - 1];

                for (int j = 0; j < _neurons[i].Length; j++)
                {
                    float[] neuronWeights = new float[prevNeurons];

                    //Set random weights -1 -> 1
                    for (int k = 0; k < prevNeurons; k++)
                    {
                        neuronWeights[k] = UnityEngine.Random.Range(-0.5f, 0.5f);
                        //(float)_random.NextDouble() - 0.5f;
                    }

                    layerWeights.Add(neuronWeights);
                }
                weights.Add(layerWeights.ToArray());
            }
            return weights.ToArray();
        }

        public NeuralNetwork(NeuralNetwork copy)
        {
            this._layers = new List<int>();
            this._layers.AddRange(copy._layers);
            this._neurons = CreateNeurons(this._layers);
            this._weights = CreateWeights(this._layers);
            this.CopyWeights(copy._weights);
        }

        private void CopyWeights(float[][][] copy)
        {
            for (int i = 0; i < _weights.Length; i++)
            {
                for (int j = 0; j < _weights[i].Length; j++)
                {
                    for (int k = 0; k < _weights[i][j].Length; k++)
                    {
                        _weights[i][j][k] = copy[i][j][k];
                    }
                }
            }
        }

        /// <summary>
        ///  Guess -1 or 1 based on input values
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public float[] FeedForward(float[] inputs)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                _neurons[0][i] = inputs[i];
            }
            for (int i = 1; i < _layers.Count; i++)
            {
                for (int j = 0; j < _neurons[i].Length; j++)
                {

                    float value = 0f;

                    for (int k = 0; k < _neurons[i-1].Length; k++)
                    {
                        value += _weights[i - 1][j][k] * _neurons[i - 1][k];
                    }

                    _neurons[i][j] = (float)Math.Tanh(value);
                }
            }
            //Return the output layer
            return _neurons[_neurons.Length-1];
        }

        /// <summary>
        /// Modifys the weight for a specific 
        /// layer. There is a very small chance
        /// (0.8%) that a mutation will occur
        /// </summary>
        public void Mutate()
        {
            if(_random == null) _random = new System.Random();
            for (int i = 0; i < _weights.Length; i++)
            {
                for (int j = 0; j < _weights[i].Length; j++)
                {
                    for (int k = 0; k < _weights[i][j].Length; k++)
                    {
                        float weight = _weights[i][j][k];

                        float randomNumber = (float)_random.NextDouble() * 1000f;

                        if(_mutations != null && _mutations.Count > 0)
                        {
                            _mutations.First(m => m.predicate(randomNumber)).mutator(weight);
                        } 
                    }
                }   
            }
        }

        /// <summary>
        /// Returns the
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(NeuralNetwork other)
        {
            if (fitness == null) return 1;
            else if (other == null) return 1;
            else return fitness.CompareTo(other.fitness);
        }
        
    }
} 
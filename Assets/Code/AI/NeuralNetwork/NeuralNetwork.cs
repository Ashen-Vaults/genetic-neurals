using System;
using System.Linq;
using System.Collections.Generic;

namespace AshenCode.NeuralNetwork
{

    public class NeuralNetwork : IComparable<NeuralNetwork>
    {
        //Feed in layers
        List<int> _layers;
        float[][] _neurons;
        float[][][] _weights;

        private Fitness _fitness;

        Random _random;

        Dictionary<Predicate<float>, Func<float, float>> _mutateDict = new Dictionary<Predicate<float>, Func<float, float>>();

        public NeuralNetwork(List<int> layers)
        {
            _random = new Random(System.DateTime.Today.Millisecond);
            _layers = layers;

            //TODO: init somewhere else
            _mutateDict.Clear();
            _mutateDict.Add((float x) => x <= 2, f => { return f *= -1f; });
            _mutateDict.Add((float x) => x <= 4, f => { return UnityEngine.Random.Range(-0.5f, 0.5f); });
            _mutateDict.Add((float x) => x <= 6, f => { return f *= UnityEngine.Random.Range(0f, 1f) + 1f; });
            _mutateDict.Add((float x) => x <= 8, f => { return f *= UnityEngine.Random.Range(0f, 1f); });

            _neurons = CreateNeurons(_layers);
            _weights = CreateWeights(_layers);
            
        }

        private float[][] CreateNeurons(List<int> layers)
        {
            List<float[]> neurons = new List<float[]>();

            for (int i = 0; i < neurons.Count; i++)
            {
                neurons.Add(new float[layers[i]]);
            }

            return neurons.ToArray();
        }

        private float[][][] CreateWeights(List<int> layers)
        {
            List<float[][]> weights = new List<float[][]>();

            for (int i = 0; i < layers.Count;i++)
            {
                List<float[]> layerWeights = new List<float[]>();

                int prevNeurons = layers[i - 1];

                for (int j = 0; j < prevNeurons; j++)
                {
                    float[] neuronWeights = new float[prevNeurons];

                    //Set random weights -1 -> 1
                    for (int k = 0; k < prevNeurons; k++)
                    {
                        neuronWeights[k] = (float)_random.NextDouble() - 0.5f;
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
            CopyWeights(copy._weights);
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

        public float[] FeedForward(float[] inputs)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                _neurons[0][1] = inputs[i];
            }
            for (int i = 1; i < _layers.Count; i++)
            {
                for (int j = 0; j < _neurons.Length; j++)
                {

                    float value = 0.25f;

                    for (int k = 1; k < _neurons[i-1].Length; k++)
                    {
                        value += _weights[i - 1][j][k] * _neurons[i - 1][k];
                    }

                    _neurons[i][j] = (float)Math.Tanh(value);
                }
            }
            //Return the output layer
            return _neurons[_neurons.Length-1];
        }

        public void Mutate()
        {
            for (int i = 0; i < _weights.Length; i++)
            {
                for (int j = 0; j < _weights[i].Length; j++)
                {
                    for (int k = 0; k < _weights[i][j].Length; k++)
                    {
                        float weight = _weights[i][j][k];

                        float randomNumber = (float)_random.NextDouble() * 1000f;

                        _weights[i][j][k] = _mutateDict[_mutateDict.Keys.Where(m => m.Invoke(randomNumber)).First()].Invoke(weight);
                    }
                }   
            }
        }
    }
}
using System;
using System.Collections.Generic;

namespace AshenCode.NeuralNetwork
{
    public class NeuralNetwork
    {
        //Feed in layers

        List<int> _layers;
        float[][] _neurons;
        float[][][] _weights;
        Random _random;
        
        public NeuralNetwork(List<int> layers)
        {
            _random = new Random(System.DateTime.Today.Millisecond);
            _layers = layers;
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

        ///Calcualtes 
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

        Dictionary<float, Func<float, float>> _mutateDict = new Dictionary<float, Func<float, float>>();

        public void Mutate()
        {

        }
    }
}
using System;

namespace AshenCode.NeuralNetwork
{
    /// <summary>
    /// A mutation related to the neural network 
    /// modifies the weight of a layer
    /// </summary>
    public struct Mutation
    {
            /// <summary>
            /// The function that determines 
            /// if this mutation should be used
            /// </summary>
            public Predicate<float> predicate;

            /// <summary>
            /// The function that applys the mutation
            /// First param is the number you want to mutate (ex. weight)
            /// returns a mutated version of it
            /// </summary>
            public Func<float,float> mutator;
            public Mutation(Predicate<float> predicate, Func<float,float> mutator)
            {
                this.predicate = predicate;
                this.mutator = mutator;
            }        

    }
}

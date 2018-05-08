namespace AshenCode.NeuralNetwork
{
    ///
    /// A standard interface for neural networks
    /// so that we can build our own as well as import others
    /// and use on the same project
    ///
    public interface INeurable
    {
    
        void Mutate();
        
        float[] FeedForward(float[] inputs);


    }
}
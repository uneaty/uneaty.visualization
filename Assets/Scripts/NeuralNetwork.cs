using UnityEngine;
using SharpNeat.Genomes.Neat;

public class NeuralNetwork : MonoBehaviour
{
    private NeatGenome _genome;

    public NeatGenome Genome
    {
        get { return _genome; }
        set
        {
            _genome = value;
            GenomeUpdated();
        }
    }

    void GenomeUpdated()
    {
        name = "Network: " + Genome.Id;
    }
}
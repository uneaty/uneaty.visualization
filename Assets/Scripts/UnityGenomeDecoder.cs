using System.Collections.Generic;
using UnityEngine;
using SharpNeat.Genomes.Neat;

public class UnityGenomeDecoder : MonoBehaviour
{
    public Transform Container;
    public GameObject NodePrototype;
    public GameObject NetworkPrototype;
    public GameObject ConnectionPrototype;
    public GenomeProvider GenomeProvider;

    private NeatGenome _lastGenome;
    private NeuralNetwork _lastNeuralNetwork;

    // Update is called once per frame
    void Update()
    {
        if (GenomeProvider == null)
        {
            return;
        }
        NeatGenome genome = GenomeProvider.Get();
        if (genome != null && _lastGenome != genome)
        {
            if (_lastNeuralNetwork != null)
            {
                Destroy(_lastNeuralNetwork.gameObject);
            }

            NeuralNetwork network = ProcessGenome(genome);
            network.transform.parent = Container;
            network.transform.localPosition = Vector3.zero;
            _lastGenome = genome;
            _lastNeuralNetwork = network;
        }
    }

    public NeuralNetwork ProcessGenome(NeatGenome genome)
    {
        GameObject go = (GameObject) Instantiate(NetworkPrototype, Vector3.zero, Quaternion.identity);
        Dictionary<uint, Transform> transformMap = new Dictionary<uint, Transform>();

        NeuralNetwork network = go.GetComponent<NeuralNetwork>();
        network.Genome = genome;

        foreach (NeuronGene neuronGene in genome.NeuronGeneList)
        {
            go = (GameObject) Instantiate(NodePrototype, Vector3.zero, Quaternion.identity);
            NeuralNetworkNode node = go.GetComponent<NeuralNetworkNode>();
            node.transform.parent = network.transform;
            node.Gene = neuronGene;
            transformMap[neuronGene.Id] = node.transform;
        }

        foreach (ConnectionGene connectionGene in genome.ConnectionList)
        {
            go = (GameObject) Instantiate(ConnectionPrototype, Vector3.zero, Quaternion.identity);
            NeuralNetworkConnection node = go.GetComponent<NeuralNetworkConnection>();
            node.transform.parent = network.transform;
            node.Gene = connectionGene;
            node.Source = transformMap[connectionGene.SourceNodeId];
            node.Destination = transformMap[connectionGene.TargetNodeId];
        }

        return network;
    }
}
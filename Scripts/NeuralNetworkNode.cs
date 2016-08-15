using UnityEngine;
using SharpNeat.Genomes.Neat;
using SharpNeat.Network;

public class NeuralNetworkNode : MonoBehaviour
{
    private NeuronGene _gene;
    private Material NodeMaterial;

    public NeuronGene Gene
    {
        get { return _gene; }
        set
        {
            _gene = value;
            GeneUpdated();
        }
    }

    void GeneUpdated()
    {
        name = "Node: " + _gene.Id;
        transform.localPosition = Random.onUnitSphere * 2f;
    }

    public void Start()
    {
        NodeMaterial = GetComponent<Renderer>().material;
    }

    public void Update()
    {
        Color color = Color.white;
        switch (Gene.NodeType)
        {
            case NodeType.Bias:
                color = Color.blue;
                break;
            case NodeType.Input:
                color = Color.green;
                break;
            case NodeType.Output:
                color = Color.red;
                break;
            case NodeType.Hidden:
                color = Color.Lerp(Color.red, Color.blue, .5f);
                break;
        }
        NodeMaterial.SetColor("_EmissionColor", color);
    }
}
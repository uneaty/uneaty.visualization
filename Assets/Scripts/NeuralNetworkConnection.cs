using UnityEngine;
using SharpNeat.Genomes.Neat;

[RequireComponent(typeof(LineRenderer))]
public class NeuralNetworkConnection : MonoBehaviour
{
    private ConnectionGene _gene;
    private LineRenderer _lr;
    public Transform Source;
    public Transform Destination;

    public ConnectionGene Gene
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
        name = "From: " + Gene.SourceNodeId + " To: " + Gene.TargetNodeId;
    }

    public void Start()
    {
        _lr = GetComponent<LineRenderer>();
    }

    public void Update()
    {
        Color color = Color.white;
        if (Gene.IsMutated)
        {
            color = Color.red;
        }

        float size = Mathf.Abs((float) Gene.Weight) / 100;

        _lr.SetColors(color, color);
        _lr.SetWidth(size, size);
        _lr.SetPosition(0, Source.position);
        _lr.SetPosition(1, Destination.position);
    }
}
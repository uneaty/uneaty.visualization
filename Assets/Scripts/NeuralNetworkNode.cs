using System;
using UnityEngine;
using SharpNeat.Genomes.Neat;
using SharpNeat.Network;
using Random = UnityEngine.Random;

public class NeuralNetworkNode : MonoBehaviour
{
    private NeuronGene _gene;
    private Material _nodeMaterial;
    private Color _cachedColor;
    private NodeType _cachedNodeType;

    public NeuronGene Gene
    {
        get { return _gene; }
        set
        {
            _gene = value;
            GeneUpdated();
            _cachedNodeType = Gene.NodeType;
        }
    }

    void GeneUpdated()
    {
        name = "Node: " + _gene.Id + " " + Gene.NodeType;
        if (!_cachedNodeType.Equals(Gene.NodeType))
        {
            Render();
        }
    }

    public void Start()
    {
        _nodeMaterial = GetComponent<Renderer>().material;
    }

    public virtual void Render()
    {
        Vector3 position = GetPositionFromName();
        Color color = Color.white;
        Vector3 scaling = Vector3.one * 0.1f;

        switch (Gene.NodeType)
        {
            case NodeType.Input:
                position.x = Math.Abs(position.x) * 2;
                color = Color.green;
                break;
            case NodeType.Output:
                position.x = Math.Abs(position.x) * -2;
                color = Color.red;
                break;
            case NodeType.Bias:
                position.z = Math.Abs(position.z) * -1f;
                color = Color.yellow;
                break;
            case NodeType.Hidden:
                position.z = Math.Abs(position.z);
                color = Color.blue;
                break;
            default:
                Debug.Log("Encountered node: " + Gene.NodeType);
                break;
        }
        _cachedColor = color;
        transform.localPosition = position;
        transform.localScale = scaling;
    }

    public virtual float UpdateValue()
    {
        float value = 0f;
        if (Gene.AuxState != null)
        {
            foreach (double val in Gene.AuxState)
            {
                value += Mathf.Clamp((float) val, float.MinValue, float.MaxValue);
            }
        }
        return value;
    }

    public virtual Vector3 GetPositionFromName()
    {
        Random.InitState(name.GetHashCode());
        return new Vector3(Random.value, Random.value, Random.value);
    }

    public void Update()
    {
        _nodeMaterial.SetColor("_Color", _cachedColor);
        _nodeMaterial.SetColor("_EmissionColor", _cachedColor);
    }
}
using UnityEngine;

public class EvolutionUI : MonoBehaviour
{
    public Optimizer Optimizer;

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 40), "Start EA"))
        {
            Optimizer.StartEA();
        }
        if (GUI.Button(new Rect(10, 60, 100, 40), "Stop EA"))
        {
            Optimizer.StopEA();
        }
        if (GUI.Button(new Rect(10, 110, 100, 40), "Run Best"))
        {
            Optimizer.RunBest();
        }
        if (GUI.Button(new Rect(10, 160, 100, 40), "Reset"))
        {
            Optimizer.Reset();
        }

        GUI.Button(new Rect(10, Screen.height - 70, 100, 60),
            string.Format("Generation: {0}\nFitness: {1:0.00000000}", Optimizer.Generation, Optimizer.Fitness));
    }
}
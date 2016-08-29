using UnityEngine;

public class RotationsPerMinute : MonoBehaviour
{
    public float RPMs = 10.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 6.0f*RPMs*Time.deltaTime, 0);
    }
}
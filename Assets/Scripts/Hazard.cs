using UnityEngine;

public class LaserRotate : MonoBehaviour
{
    public float rotationSpeed = 100f;

    public Vector3 rotationAxis = Vector3.up;

    void Update()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
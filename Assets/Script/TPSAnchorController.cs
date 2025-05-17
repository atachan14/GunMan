using UnityEngine;

public class TPSAnchorController : MonoBehaviour
{
    public bool isRotating = false;
    public float rotationSpeed = 30f;

    void Update()
    {
        if (isRotating)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

    }
}

using UnityEngine;

public class EyePointContoroller : MonoBehaviour
{
    [SerializeField] Transform SightPoint;

    void Update()
    {
        transform.LookAt(SightPoint);

    }
}

using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class ReticleTargetWall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Vector3.zero);
        transform.Rotate(0f,090f,0f);
    }
}

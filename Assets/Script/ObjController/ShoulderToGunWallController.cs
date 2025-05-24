using UnityEngine;
using UnityEngine.Animations;

public class ShoulderToGunWallController : MonoBehaviour
{
    [SerializeField] Transform gun;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(gun);
    }
}

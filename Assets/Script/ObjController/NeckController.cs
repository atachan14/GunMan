using UnityEngine;

public class NeckController : MonoBehaviour
{
    [SerializeField] Transform neck;
    [SerializeField] Transform sight;
    // Update is called once per frame
    void Update()
    {
        if (RoundManager.Instance.IsFPS)
        {
            neck.LookAt(sight);
        }


    }
}

using UnityEngine;

public class TPSAnchorController : MonoBehaviour
{
    public bool isRotating = false;
    public float rotationSpeed;

   
    private void OnEnable()
    {
        RoundManager.OnModeChanged += HandleRoundModeChanged;
    }

    private void OnDisable()
    {
        RoundManager.OnModeChanged -= HandleRoundModeChanged;
    }

    private void HandleRoundModeChanged(RoundMode mode)
    {
        
    }

    void Update()
    {
       
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        
    }

}

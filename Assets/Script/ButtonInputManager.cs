using UnityEngine;

public class ButtonInputManager : MonoBehaviour
{
    [SerializeField] MouseNaviController mouseNavi;
    [SerializeField] CameraController cameraController;
    void Update()
    {
        if (mouseNavi.IsOnGetGun && Input.GetMouseButtonDown(1))
        {
            cameraController.SwitchToFPS();
        }
        if (cameraController.IsFPS && Input.GetMouseButtonUp(1))
        {
            cameraController.SwitchToTPS();
        }
    }
}

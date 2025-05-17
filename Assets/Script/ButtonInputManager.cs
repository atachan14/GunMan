using UnityEngine;

public class ButtonInputManager : MonoBehaviour
{
    [SerializeField] MouseNaviController mouseNavi;
    [SerializeField] CameraController cameraController;
    [SerializeField] Transform gun;
    [SerializeField] Transform hand;
    void Update()
    {
        if (mouseNavi.IsOnGetGun && Input.GetMouseButtonDown(1))
        {
            GrabGun();
            cameraController.SwitchToFPS();
        }
        if (cameraController.IsFPS && Input.GetMouseButtonUp(1))
        {
            cameraController.SwitchToTPS();
        }
    }

    void GrabGun()
    {
        gun.SetParent(hand);
    }
}

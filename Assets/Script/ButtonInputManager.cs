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
        }
    }

    void GrabGun()
    {
        gun.SetParent(hand);
    }
}

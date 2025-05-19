using UnityEngine;

public class ButtonInputManager : MonoBehaviour
{
    [SerializeField] ArmNaviController mouseNavi;
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
        RoundManager.Instance.Mode = RoundMode.FPS;
    }
}

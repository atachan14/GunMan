using UnityEngine;

public class ButtonInputManager : MonoBehaviour
{
    [SerializeField] ArmNaviController mouseNavi;
    [SerializeField] Transform gun;
    Rigidbody gunRb;

    [SerializeField] Transform hand;

    [SerializeField] Vector3 handHoldPos = new Vector3(0.05f, 0.12f, -0.04f);
    Quaternion handHoldRot = new Quaternion(0.48394f, 0.45825f, 0.58045f, -0.46785f);



    private void Start()
    {
        gunRb = gun.GetComponent<Rigidbody>();
    }
    void Update()
    {
        Debug.Log("Pos: " + gun.localPosition);
        Debug.Log("Rot: " + gun.localRotation);
        if (mouseNavi.IsOnGetGun && Input.GetMouseButtonDown(1))
        {
            GrabGun();
        }

        if (RoundManager.Instance.IsFPS && Input.GetMouseButtonUp(1))
        {
            DropGun();
        }
    }

    void GrabGun()
    {
        gun.SetParent(hand, false);
        
        gunRb.isKinematic = true;
        gunRb.useGravity = false;
        

        gun.localPosition = handHoldPos;
        gun.localRotation = handHoldRot;

        RoundManager.Instance.Mode = RoundMode.FPS;
    }

    void DropGun()
    {
        gun.transform.parent = null; // 完全に親から外す
        gunRb.isKinematic = false;   // 物理演算ON
        gunRb.useGravity = true;     // 重力ON
        gunRb.velocity = Vector3.zero;
        gunRb.angularVelocity = Vector3.zero;
        gun.transform.position += Vector3.up * 0.2f; // 地面めり込み防止
        gunRb.WakeUp();              // 強制で動かす

        RoundManager.Instance.Mode = RoundMode.TPS;
    }
}

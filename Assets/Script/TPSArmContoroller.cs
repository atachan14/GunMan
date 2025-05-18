using Unity.Burst.Intrinsics;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    [SerializeField] Transform shoulder; // arm_stretch.r
    [SerializeField] Transform shoulderVectorStart;
    [SerializeField] Transform mouseNavi;   // MouseNaviのTransform（CanvasじゃなくWorld位置のやつ）
    [SerializeField] CameraController cameraController;

    [SerializeField] float sensitivity = 1f;
    float previousRotationX = 0f;
    float previousRotationY = 0f;

    void LateUpdate()
    {
        if (cameraController.IsTPS)
        {
            TPSArmControll();
        }

        if (cameraController.IsFPS)
        {
            FPSArmControll();
        }
    }

    void TPSArmControll()
    {
        Vector3 dir = (mouseNavi.position - shoulder.position).normalized;
        Quaternion targetRot = Quaternion.LookRotation(dir);
        // Xを正面にするためにY軸で-90度ずらす
        targetRot *= Quaternion.Euler(0, 0, 0);
        shoulder.rotation = targetRot;
    }

    void FPSArmControll()
    {

        // 入力から腕の回転量を作る（マウスやゲームパッドから）
        float inputX = Input.GetAxis("Mouse Y"); // 上下（反転させるかも）
        float inputY = Input.GetAxis("Mouse X"); // 左右

        // 適当な感度とか制限つけて回転量決定
        float rotationX = Mathf.Clamp(previousRotationX + inputX * sensitivity, -90f, 90f);
        float rotationY = previousRotationY + inputY * sensitivity;

        // 肩のローカル回転を変更（ここは腕のローカル回転軸に合わせて調整）
        shoulder.localEulerAngles = new Vector3(-rotationX, rotationY, 0f);

        // 体のTransformは変更しない！！！
    }
}

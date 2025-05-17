using UnityEngine;

public class ArmController : MonoBehaviour
{
    [SerializeField] Transform shoulder; // arm_stretch.r
    [SerializeField] Transform target;   // MouseNaviのTransform（CanvasじゃなくWorld位置のやつ）
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
        // 肩からターゲットへの方向ベクトル
        Vector3 dir = target.position - shoulder.position;

        // ローカル空間に変換して、回転角度だけをXとして使う
        Vector3 localDir = shoulder.parent.InverseTransformDirection(dir.normalized);

        // YZ平面上の角度だけをXに使う（ピッチ方向）
        float angleX = Mathf.Atan2(localDir.y, localDir.z) * Mathf.Rad2Deg;

        // 回転を直接反映（ZやYは今は無視）
        shoulder.localRotation = Quaternion.Euler(-angleX, 0, 0);
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

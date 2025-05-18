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
        if (RoundManager.Instance.IsTPS)
        {
            TPSArmControll();
        }

        if (RoundManager.Instance.IsFPS)
        {
            FPSArmControll();
        }
    }

    void TPSArmControll()
    {
        // 肩からマウス目標への方向ベクトル
        Vector3 dir = mouseNavi.position - shoulder.position;

        // ローカル座標に変換（肩の軸基準で方向を見る）
        Vector3 localDir = shoulder.parent.InverseTransformDirection(dir);

        // X軸だけ回したいので、XZ平面での角度を求める（ピッチ角）
        float pitchAngle = Mathf.Atan2(-localDir.y, localDir.z) * Mathf.Rad2Deg;

        // 肩のX軸だけ回転（他軸はそのまま）
        shoulder.localRotation = Quaternion.Euler(pitchAngle+90, 0, 0f);
    }
    void FPSArmControll()
    {

       
    }
}

using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class AimController : MonoBehaviour
{
    [SerializeField] Transform shoulder; // arm_stretch.r
    [SerializeField] Transform body;
    [SerializeField] Transform hand;
    [SerializeField] Transform neck;
    [SerializeField] Transform mouseNavi;   // MouseNaviのTransform（CanvasじゃなくWorld位置のやつ）
    [SerializeField] CameraController cameraController;

    //fps↓
    [SerializeField] float sensitivity = 5f; // 好きに調整して
    float shoulderRotationX = 0f;
    float handRotationY = 0f;
    float BodyRotationY = 0f;
    float NeckRotationY = 0f;


    void LateUpdate()
    {

        if (RoundManager.Instance.IsTPS)
        {
            TPSArmControll();
        }

        if (RoundManager.Instance.IsFPS)
        {
            FPSArmControll();
            FPSHandControll();
            FPSBodyControll();
            FPSNeckControll();
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
        shoulder.localRotation = Quaternion.Euler(pitchAngle + 90, 0, 0f);
    }
    void FPSArmControll()
    {
        float mouseY = InputMouse.Instance.Y;
        shoulderRotationX += mouseY * sensitivity;
        shoulderRotationX = Mathf.Clamp(shoulderRotationX, -180f, 90f);

        Vector3 current = shoulder.localEulerAngles;
        shoulder.localEulerAngles = new Vector3(shoulderRotationX, current.y, current.z);
    }

    void FPSHandControll()
    {
        float mouseY = InputMouse.Instance.Y;
        handRotationY -= mouseY * sensitivity;
        handRotationY = Mathf.Clamp(handRotationY, -180f, 180f);

        Vector3 current = hand.localEulerAngles;
        hand.localEulerAngles = new Vector3(current.x, handRotationY, current.z);
    }


    void FPSBodyControll()
    {
        float mouseX = InputMouse.Instance.X;

        BodyRotationY += mouseX * sensitivity;

        // 回転をY軸方向に反映（体は地面に対して水平回転）
        body.localRotation = Quaternion.Euler(0f, BodyRotationY, 0f);
    }

    void FPSNeckControll()
    {
        float mouseY = InputMouse.Instance.Y;

        NeckRotationY += mouseY * sensitivity;

        // 回転をY軸方向に反映（体は地面に対して水平回転）
        neck.localRotation = Quaternion.Euler(0f, NeckRotationY, 0f);
    }


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
        if (mode == RoundMode.FPS)
        {
            shoulderRotationX = NormalizeAngle(shoulder.localEulerAngles.x);
            handRotationY = NormalizeAngle(hand.localEulerAngles.y);
        }
    }
    float NormalizeAngle(float angle)
    {
        angle %= 360;
        if (angle > 180) angle -= 360;
        return angle;
    }
}

using Unity.Burst.Intrinsics;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class AimController : MonoBehaviour
{
    [SerializeField] Transform shoulder; // arm_stretch.r
    [SerializeField] Transform body;
    [SerializeField] Transform mouseNavi;   // MouseNaviのTransform（CanvasじゃなくWorld位置のやつ）
    [SerializeField] CameraController cameraController;

    //fps↓
    [SerializeField] float sensitivity = 5f; // 好きに調整して
    float xRotation = 0f;
    float yRotation = 0f;


    void LateUpdate()
    {
        if (RoundManager.Instance.IsTPS)
        {
            TPSArmControll();
        }

        if (RoundManager.Instance.IsFPS)
        {
            FPSArmControll();
            FPSBodyControll();
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

        xRotation += mouseY * sensitivity; 
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // ここで上下の制限

        // 水平軸はカメラの右方向ベクトル
        Vector3 right = cameraController.transform.right;

        // Quaternionで制限付きの回転を適用
        shoulder.localRotation = Quaternion.AngleAxis(xRotation, right);

    }

    void FPSBodyControll()
    {
        float mouseX = InputMouse.Instance.X;

        yRotation += mouseX * sensitivity;

        // 回転をY軸方向に反映（体は地面に対して水平回転）
        body.localRotation = Quaternion.Euler(0f, yRotation, 0f);
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
            Vector3 currentEuler = shoulder.localEulerAngles;
            xRotation = NormalizeAngle(currentEuler.x); // ← これで初期値を今の腕の角度に揃える
        }
    }
    float NormalizeAngle(float angle)
    {
        angle %= 360;
        if (angle > 180) angle -= 360;
        return angle;
    }
}

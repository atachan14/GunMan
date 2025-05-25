using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.XR;

public class AimManager : MonoBehaviour
{
    [SerializeField] Transform arm_stretch; // arm_stretch.r
    [SerializeField] Transform body;
    [SerializeField] Transform hand;
    [SerializeField] Transform neck;
    [SerializeField] Transform mouseNavi;   // MouseNavi��Transform�iCanvas����Ȃ�World�ʒu�̂�j
    [SerializeField] CameraController cameraController;

    //fps��
    [SerializeField] float sensitivity = 5f; // �D���ɒ�������
    float armRotationX = 0f;
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

            FPSBodyControll();
            //FPSNeckControll();
        }
    }



    void TPSArmControll()
    {
        // ������}�E�X�ڕW�ւ̕����x�N�g��
        Vector3 dir = mouseNavi.position - arm_stretch.position;

        // ���[�J�����W�ɕϊ��i���̎���ŕ���������j
        Vector3 localDir = arm_stretch.parent.InverseTransformDirection(dir);

        // X�������񂵂����̂ŁAXZ���ʂł̊p�x�����߂�i�s�b�`�p�j
        float pitchAngle = Mathf.Atan2(-localDir.y, localDir.z) * Mathf.Rad2Deg;

        // ����X��������]�i�����͂��̂܂܁j
        arm_stretch.localRotation = Quaternion.Euler(pitchAngle + 90, 0, 0f);
    }
    void FPSArmControll()
    {
        float mouseY = InputMouse.Instance.Y;
        armRotationX += mouseY * sensitivity;
        armRotationX = Mathf.Clamp(armRotationX, -180f, 90f);

        Vector3 current = arm_stretch.localEulerAngles;
        arm_stretch.localEulerAngles = new Vector3(armRotationX, current.y, current.z);
    }

    //void FPSHandControll()
    //{
    //    float mouseY = InputMouse.Instance.Y;
    //    handRotationY -= mouseY * sensitivity;
    //    handRotationY = Mathf.Clamp(handRotationY, -180f, 180f);

    //    Vector3 current = hand.localEulerAngles;
    //    hand.localEulerAngles = new Vector3(current.x, handRotationY, current.z);
    //}


    void FPSBodyControll()
    {
        float mouseX = InputMouse.Instance.X;

        BodyRotationY += mouseX * sensitivity;

        // ��]��Y�������ɔ��f�i�̂͒n�ʂɑ΂��Đ�����]�j
        body.localRotation = Quaternion.Euler(0f, BodyRotationY, 0f);
    }

    void FPSNeckControll()
    {
        float mouseY = InputMouse.Instance.Y;

        NeckRotationY += mouseY * sensitivity;

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
        if (mode == RoundMode.TPS)
        {
            TPSHandSetup();
            Debug.Log("tps");
        }

        if (mode == RoundMode.FPS)
        {
            armRotationX = NormalizeAngle(arm_stretch.localEulerAngles.x);
            FPSHandSetup();
        }
    }
    void TPSHandSetup()
    {
        hand.localRotation = Quaternion.identity;
    }
    void FPSHandSetup()
    {
        hand.localRotation = Quaternion.Euler(0, -90, 0);
    }


    float NormalizeAngle(float angle)
    {
        angle %= 360;
        if (angle > 180) angle -= 360;
        return angle;
    }
}

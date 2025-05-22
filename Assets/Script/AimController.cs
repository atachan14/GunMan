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
    [SerializeField] Transform mouseNavi;   // MouseNavi��Transform�iCanvas����Ȃ�World�ʒu�̂�j
    [SerializeField] CameraController cameraController;

    //fps��
    [SerializeField] float sensitivity = 5f; // �D���ɒ�������
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
        // ������}�E�X�ڕW�ւ̕����x�N�g��
        Vector3 dir = mouseNavi.position - shoulder.position;

        // ���[�J�����W�ɕϊ��i���̎���ŕ���������j
        Vector3 localDir = shoulder.parent.InverseTransformDirection(dir);

        // X�������񂵂����̂ŁAXZ���ʂł̊p�x�����߂�i�s�b�`�p�j
        float pitchAngle = Mathf.Atan2(-localDir.y, localDir.z) * Mathf.Rad2Deg;

        // ����X��������]�i�����͂��̂܂܁j
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

        // ��]��Y�������ɔ��f�i�̂͒n�ʂɑ΂��Đ�����]�j
        body.localRotation = Quaternion.Euler(0f, BodyRotationY, 0f);
    }

    void FPSNeckControll()
    {
        float mouseY = InputMouse.Instance.Y;

        NeckRotationY += mouseY * sensitivity;

        // ��]��Y�������ɔ��f�i�̂͒n�ʂɑ΂��Đ�����]�j
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

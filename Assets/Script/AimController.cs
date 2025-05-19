using Unity.Burst.Intrinsics;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class AimController : MonoBehaviour
{
    [SerializeField] Transform shoulder; // arm_stretch.r
    [SerializeField] Transform body;
    [SerializeField] Transform mouseNavi;   // MouseNavi��Transform�iCanvas����Ȃ�World�ʒu�̂�j
    [SerializeField] CameraController cameraController;

    //fps��
    [SerializeField] float sensitivity = 5f; // �D���ɒ�������
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

        xRotation += mouseY * sensitivity; 
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // �����ŏ㉺�̐���

        // �������̓J�����̉E�����x�N�g��
        Vector3 right = cameraController.transform.right;

        // Quaternion�Ő����t���̉�]��K�p
        shoulder.localRotation = Quaternion.AngleAxis(xRotation, right);

    }

    void FPSBodyControll()
    {
        float mouseX = InputMouse.Instance.X;

        yRotation += mouseX * sensitivity;

        // ��]��Y�������ɔ��f�i�̂͒n�ʂɑ΂��Đ�����]�j
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
            xRotation = NormalizeAngle(currentEuler.x); // �� ����ŏ����l�����̘r�̊p�x�ɑ�����
        }
    }
    float NormalizeAngle(float angle)
    {
        angle %= 360;
        if (angle > 180) angle -= 360;
        return angle;
    }
}

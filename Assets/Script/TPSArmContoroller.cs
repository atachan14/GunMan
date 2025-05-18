using Unity.Burst.Intrinsics;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    [SerializeField] Transform shoulder; // arm_stretch.r
    [SerializeField] Transform shoulderVectorStart;
    [SerializeField] Transform mouseNavi;   // MouseNavi��Transform�iCanvas����Ȃ�World�ʒu�̂�j
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
        // X�𐳖ʂɂ��邽�߂�Y����-90�x���炷
        targetRot *= Quaternion.Euler(0, 0, 0);
        shoulder.rotation = targetRot;
    }

    void FPSArmControll()
    {

        // ���͂���r�̉�]�ʂ����i�}�E�X��Q�[���p�b�h����j
        float inputX = Input.GetAxis("Mouse Y"); // �㉺�i���]�����邩���j
        float inputY = Input.GetAxis("Mouse X"); // ���E

        // �K���Ȋ��x�Ƃ��������ĉ�]�ʌ���
        float rotationX = Mathf.Clamp(previousRotationX + inputX * sensitivity, -90f, 90f);
        float rotationY = previousRotationY + inputY * sensitivity;

        // ���̃��[�J����]��ύX�i�����͘r�̃��[�J����]���ɍ��킹�Ē����j
        shoulder.localEulerAngles = new Vector3(-rotationX, rotationY, 0f);

        // �̂�Transform�͕ύX���Ȃ��I�I�I
    }
}

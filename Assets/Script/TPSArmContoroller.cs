using UnityEngine;

public class ArmController : MonoBehaviour
{
    [SerializeField] Transform shoulder; // arm_stretch.r
    [SerializeField] Transform target;   // MouseNavi��Transform�iCanvas����Ȃ�World�ʒu�̂�j
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
        // ������^�[�Q�b�g�ւ̕����x�N�g��
        Vector3 dir = target.position - shoulder.position;

        // ���[�J����Ԃɕϊ����āA��]�p�x������X�Ƃ��Ďg��
        Vector3 localDir = shoulder.parent.InverseTransformDirection(dir.normalized);

        // YZ���ʏ�̊p�x������X�Ɏg���i�s�b�`�����j
        float angleX = Mathf.Atan2(localDir.y, localDir.z) * Mathf.Rad2Deg;

        // ��]�𒼐ڔ��f�iZ��Y�͍��͖����j
        shoulder.localRotation = Quaternion.Euler(-angleX, 0, 0);
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

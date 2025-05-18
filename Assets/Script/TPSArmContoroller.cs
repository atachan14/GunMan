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
        // ������}�E�X�ڕW�ւ̕����x�N�g��
        Vector3 dir = mouseNavi.position - shoulder.position;

        // ���[�J�����W�ɕϊ��i���̎���ŕ���������j
        Vector3 localDir = shoulder.parent.InverseTransformDirection(dir);

        // X�������񂵂����̂ŁAXZ���ʂł̊p�x�����߂�i�s�b�`�p�j
        float pitchAngle = Mathf.Atan2(-localDir.y, localDir.z) * Mathf.Rad2Deg;

        // ����X��������]�i�����͂��̂܂܁j
        shoulder.localRotation = Quaternion.Euler(pitchAngle+90, 0, 0f);
    }
    void FPSArmControll()
    {

       
    }
}

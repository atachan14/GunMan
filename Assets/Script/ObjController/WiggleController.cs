using UnityEngine;

public class WiggleController : MonoBehaviour
{

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // ���Ƃ肠�����e�X�g�p�ɁuD�L�[�����Ă�Ԃ���WiggleRight��ԁv���Ċ�����
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("IsWigR", true);  // Wiggle�E������
            Debug.Log("WigR�͂����");
        }
        else
        {
            anim.SetBool("IsWigR", false); // �����ĂȂ��Ƃ��͌��ɖ߂�
        }
    }
}



using UnityEngine;

public class WiggleController : MonoBehaviour
{

    Animator anim;
    const string isN = "IsNeut";
    const string isR = "IsWigR";
    const string isL = "IsWigL";
    const string isJumR = "IsJumR";
    const string isJumL = "IsJumL";

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (anim.GetBool(isN) && Input.GetKey(KeyCode.D))
        {
            anim.SetBool(isR, true);
            anim.SetBool(isN, false);
        }
        else
        {
            anim.SetBool(isR, false);
            anim.SetBool(isN, true);
        }
        if (anim.GetBool(isN) && Input.GetKey(KeyCode.A))
        {
            anim.SetBool(isL, true);
            anim.SetBool(isN, false);

        }
        else
        {
            anim.SetBool(isL, false);
            anim.SetBool(isN, true);
        }
        if (anim.GetBool(isR) && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool(isJumR, true);
            anim.SetBool(isR, false);
        }



    }
}



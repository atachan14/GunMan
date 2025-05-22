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
        // ↓とりあえずテスト用に「Dキー押してる間だけWiggleRight状態」って感じに
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("IsWigR", true);  // Wiggle右発動中
            Debug.Log("WigRはちゅど");
        }
        else
        {
            anim.SetBool("IsWigR", false); // 押してないときは元に戻す
        }
    }
}



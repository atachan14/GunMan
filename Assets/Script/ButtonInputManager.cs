using System.Collections;
using UnityEngine;

public class ButtonInputManager : MonoBehaviour
{
    [SerializeField] ArmNaviController mouseNavi;
    [SerializeField] GunController gunCon;





    private void Start()
    {
    }
    void Update()
    {
        if (mouseNavi.IsOnGetGun && Input.GetMouseButtonDown(1))
        {
            gunCon.GrabGun();
            RoundManager.Instance.Mode = RoundMode.FPS;
        }

        if (RoundManager.Instance.IsFPS && Input.GetMouseButtonUp(1))
        {
            StartCoroutine(gunCon.DropGun());
            RoundManager.Instance.Mode = RoundMode.TPS;
        }
    }




}

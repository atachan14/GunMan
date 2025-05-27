using System.Collections;
using UnityEngine;

public class ButtonReceiver : MonoBehaviour
{
    [SerializeField] private MonoBehaviour inputSource;
    private IPlayerInput input => inputSource as IPlayerInput;

    [SerializeField] ArmNaviController mouseNavi;
    [SerializeField] GunController gunCon;


    void Update()
    {
        if (mouseNavi.IsOnGetGun && input.RightDown)
        {
            gunCon.GrabGun();
            RoundManager.Instance.Mode = RoundMode.FPS;
        }

        if (RoundManager.Instance.IsFPS && input.RightUp)
        {
            StartCoroutine(gunCon.DropGun());
            RoundManager.Instance.Mode = RoundMode.TPS;
        }


    }




}

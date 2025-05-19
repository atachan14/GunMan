using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum NaviMode
{
    TooFar,
    OnArmReach,
    OnGunReach,
    NotTPS
}

public class ArmNaviController : MonoBehaviour
{
    [SerializeField] Transform Gun;
    public Image MouseNaviImage;
    readonly Dictionary<NaviMode, Color> modeColors = new()
    {
        { NaviMode.OnGunReach, new Color(0f, 1f, 0f, 0.3f) },     // 緑・半透明
        { NaviMode.OnArmReach, new Color(0f, 0f, 1f, 0.3f) },   // 青・半透明
        { NaviMode.TooFar, new Color(1f, 1f, 1f, 0.3f) },       // 白・半透明
        { NaviMode.NotTPS, Color.clear }                       // 完全透明
    };

    public NaviMode Mode { get; private set; }
    public bool IsOnGetGun => Mode == NaviMode.OnGunReach;
    public bool IsOnArmReach => Mode == NaviMode.OnArmReach;





    void Update()
    {
        if (!RoundManager.Instance.IsTPS)
        {
            Mode = NaviMode.NotTPS;
        }

        FollowMouse();
        UpdateColor();
    }

    void FollowMouse()
    {
        MouseNaviImage.transform.position = InputMouse.Instance.Position;

        Vector3 screenPos = InputMouse.Instance.Position;
        screenPos.z = Mathf.Abs(Camera.main.transform.position.z - Gun.position.z); // ここ！zを先に決める！

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        transform.position = worldPos;
    }

    void UpdateColor()
    {
        MouseNaviImage.color = modeColors[Mode];
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("GunReach"))
        {
            Mode = NaviMode.OnGunReach;
        }
        else if (other.gameObject.CompareTag("ArmReach"))
        {
            Mode = NaviMode.OnArmReach;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GunReach") || other.CompareTag("ArmReach"))
        {
            Mode = NaviMode.TooFar;
        }
    }

}

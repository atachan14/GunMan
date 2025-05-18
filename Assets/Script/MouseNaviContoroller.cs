using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum NaviMode
{
    TooFar,
    OnArmReach,
    OnGetGun,
    NotTPS
}

public class MouseNaviController : MonoBehaviour
{
    [SerializeField] Transform Shoulder;
    public SpriteRenderer sr;
    readonly Dictionary<NaviMode, Color> modeColors = new()
    {
        { NaviMode.OnGetGun, new Color(0f, 1f, 0f, 0.3f) },     // 緑・半透明
        { NaviMode.OnArmReach, new Color(1f, 1f, 1f, 0.3f) },   // 白・半透明
        { NaviMode.TooFar, new Color(0f, 0f, 1f, 0.3f) },       // 青・半透明
        { NaviMode.NotTPS, Color.clear }                       // 完全透明
    };

    public NaviMode Mode { get; private set; }
    public bool IsOnGetGun => Mode == NaviMode.OnGetGun;
    public bool IsOnArmReach => Mode == NaviMode.OnArmReach;





    void Update()
    {
        FollowMouse();
        UpdateColor();
    }

    void FollowMouse()
    {
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = Mathf.Abs(Camera.main.transform.position.z - Shoulder.position.z); // ここ！zを先に決める！

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        transform.position = worldPos;

    }

    void UpdateColor()
    {
        sr.color = modeColors[Mode];
    }
}

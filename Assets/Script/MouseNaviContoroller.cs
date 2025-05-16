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
    [SerializeField] CameraController cameraController;
    Camera mainCam;

    public Image cursorImage;
    public RectTransform rectTransform;
    readonly Dictionary<NaviMode, Color> modeColors = new()
    {
        { NaviMode.OnGetGun, new Color(0f, 1f, 0f, 0.3f) },     // 緑・半透明
        { NaviMode.OnArmReach, new Color(1f, 1f, 1f, 0.3f) },   // 白・半透明
        { NaviMode.TooFar, new Color(0f, 0f, 1f, 0.3f) },       // 青・半透明
        { NaviMode.NotTPS, Color.clear }                       // 完全透明
    };

    public NaviMode CurrentMode { get; private set; }
    public bool IsOnGetGun => CurrentMode == NaviMode.OnGetGun;
    public bool IsOnArmReach => CurrentMode == NaviMode.OnArmReach;




    void Start()
    {
        Cursor.visible = false;
        mainCam = Camera.main;
    }

    void Update()
    {
        FollowMouse();
        UpdateMode();
        UpdateColor();
    }

    void FollowMouse()
    {
        rectTransform.position = Input.mousePosition;
    }

    void UpdateMode()
    {
        if (cameraController.IsTPS)
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.CompareTag("GetGun"))
                    CurrentMode = NaviMode.OnGetGun;
                else if (hit.collider.CompareTag("ArmReach"))
                    CurrentMode = NaviMode.OnArmReach;
                else
                    CurrentMode = NaviMode.TooFar;
            }
            else
            {
                CurrentMode = NaviMode.TooFar;
            }
        }
        else
        {
            CurrentMode = NaviMode.NotTPS;
        }
    }


    void UpdateColor()
    {
        cursorImage.color = modeColors[CurrentMode];
    }
}

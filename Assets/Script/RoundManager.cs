using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;
public enum RoundMode
{
    None,
    Opening,
    TPS,
    FPS,
    ShotFire,
    BulletTime,
    GotShot,
    Replay,
    Result
}
public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance;
    [SerializeField] OpeningActor openingActor;

    public static event Action<RoundMode> OnModeChanged;

    private RoundMode _mode = RoundMode.None;
    public RoundMode Mode
    {
        get => _mode;
        set
        {
            if (_mode == value) return;
            _mode = value;
            Debug.Log("mode•ÏX”­‰ÎI");
            OnModeChanged?.Invoke(_mode);
        }
    }

    public bool IsTPS => _mode == RoundMode.TPS;
    public bool IsFPS => _mode == RoundMode.FPS;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Mode = RoundMode.Opening;
    }


}

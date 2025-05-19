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
            Debug.Log("mode変更発火！");
            OnModeChanged?.Invoke(_mode);
        }
    }

    public bool IsOpening => _mode == RoundMode.Opening;
    public bool IsTPS => _mode == RoundMode.TPS;
    public bool IsFPS => _mode == RoundMode.FPS;
    public bool IsReplay => _mode == RoundMode.Replay; 

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"[RoundManager] 2個目生成されたのでDestroyします。Editorに戻って構造確認せぇ！");
            Destroy(gameObject); // 複数あったら潰す
            return;
        }
        Instance = this;
    }

    void Start()
    {
        Mode = RoundMode.Opening;
    }


}

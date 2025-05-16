using System.Collections;
using UnityEngine;

public enum CameraMode { None, Opening, TPS, FPS, TransitionToFPS, TransitionToTPS }

public class CameraController : MonoBehaviour
{
    [Header("Anchor Points")]
    public Transform tpsAnchor;
    public Transform eyeAnchor;
    public Transform openingAnchor;

    [Header("Look Targets")]
    public Transform centerPoint;
    public Transform muzzlePoint;

    [Header("Settings")]
    public float transilateSpeed = 5f;

    public CameraMode mode = CameraMode.None;
    private Transform lookTarget;
    private Coroutine transitionRoutine;

    public bool IsOpening => mode == CameraMode.Opening;
    public bool IsTPS => mode == CameraMode.TPS;
    public bool IsFPS => mode == CameraMode.FPS;

    void Update()
    {
       
    }

    public void SwitchToMode(CameraMode newMode)
    {
        if (transitionRoutine != null)
        {
            StopCoroutine(transitionRoutine);
        }

        mode = newMode;

        switch (mode)
        {
            case CameraMode.Opening:
                SnapToParent(openingAnchor, centerPoint);
                break;
            case CameraMode.TPS:
                SnapToParent(tpsAnchor, centerPoint);
                break;
            case CameraMode.FPS:
                SnapToParent(eyeAnchor, muzzlePoint);
                break;
            case CameraMode.TransitionToFPS:
                transitionRoutine = StartCoroutine(TransitionTo(eyeAnchor, muzzlePoint, CameraMode.FPS));
                break;
            case CameraMode.TransitionToTPS:
                transitionRoutine = StartCoroutine(TransitionTo(tpsAnchor, centerPoint, CameraMode.TPS));
                break;
        }
    }

    void SnapToParent(Transform parentTarget, Transform look)
    {
        transform.SetParent(parentTarget);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        lookTarget = look;
    }

    IEnumerator TransitionTo(Transform targetPos, Transform look, CameraMode nextMode)
    {
        transform.SetParent(null);
        lookTarget = look;

        while (Vector3.Distance(transform.position, targetPos.position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos.position, transilateSpeed * Time.deltaTime);
            transform.LookAt(lookTarget);
            yield return null;
        }

        // 完全に移動完了後、次のモードにスイッチ
        SwitchToMode(nextMode);
    }

    // ショートカット用
    public void SwitchToOpening() => SwitchToMode(CameraMode.Opening);
    public void SwitchToFPS() => SwitchToMode(CameraMode.TransitionToFPS);
    public void SwitchToTPS() => SwitchToMode(CameraMode.TransitionToTPS);
}

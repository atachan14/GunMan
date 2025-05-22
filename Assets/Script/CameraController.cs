using System.Collections;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    [Header("Anchor Points")]
    public Transform tpsAnchor;
    public Transform FPSAnchor;
    public Transform openingAnchor;

    [Header("Look Targets")]
    public Transform centerPoint;
    public Transform muzzlePoint;

    [Header("Settings")]
    public float transilateSpeed = 5f;

    private Transform lookTarget;
    private Coroutine transitionRoutine;

    private void OnEnable()
    {
        RoundManager.OnModeChanged += OnModeChanged;
    }

    private void OnDisable()
    {
        RoundManager.OnModeChanged -= OnModeChanged;
    }

    public void OnModeChanged(RoundMode newMode)
    {
        if (transitionRoutine != null)
        {
            StopCoroutine(transitionRoutine);
        }

        switch (newMode)
        {
            case RoundMode.Opening:
                SnapToParent(openingAnchor, centerPoint);
                break;

            case RoundMode.TPS:
                transitionRoutine = StartCoroutine(TransitionTo(tpsAnchor, centerPoint));
                break;

            case RoundMode.FPS:
                transitionRoutine = StartCoroutine(TransitionTo(FPSAnchor, centerPoint));
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

    IEnumerator TransitionTo(Transform targetPos, Transform look)
    {
        transform.SetParent(null);
        lookTarget = look;

        while (Vector3.Distance(transform.position, targetPos.position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos.position, transilateSpeed * Time.deltaTime);
            transform.LookAt(lookTarget);
            yield return null;
        }

        SnapToParent(targetPos, look);
    }

}

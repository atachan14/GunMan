using System.Collections;
using UnityEngine;

public enum CameraMode { None, Opening, TPS, FPS, TransitionToFPS, TransitionToTPS }

public class CameraController : MonoBehaviour
{
    public Transform tpsAnchor;
    public Transform eyeAnchor;
    public Transform openingAnchor;
    public Transform centerPoint;
    public Transform muzzlePoint;


    public float moveSpeed = 5f;
    private CameraMode mode = CameraMode.None;

    void Update()
    {
        switch (mode)
        {
            case CameraMode.Opening:
                SetParentAndLook(openingAnchor, centerPoint);
                break;
            case CameraMode.TPS:
                SetParentAndLook(tpsAnchor, centerPoint);
                break;
            case CameraMode.FPS:
                SetParentAndLook(eyeAnchor, muzzlePoint);
                break;
            case CameraMode.TransitionToFPS:
                MoveTo(eyeAnchor.position, eyeAnchor);
                break;
            case CameraMode.TransitionToTPS:
                MoveTo(tpsAnchor.position, centerPoint);
                break;
        }
    }


    void SetParentAndLook(Transform target, Transform lookPoint)
    {

        if (transform.parent != target)
        {
            transform.SetParent(target);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
        transform.LookAt(lookPoint);

    }

    void MoveTo(Vector3 targetPos, Transform lookPoint)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        transform.LookAt(lookPoint);

        // äÆëSÇ…à⁄ìÆäÆóπÇµÇΩÇÁÉÇÅ[ÉhÇêÿÇËë÷Ç¶
        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            mode = (targetPos == eyeAnchor.position) ? CameraMode.FPS : CameraMode.TPS;
        }
    }
    public void SwitchToOpening() => mode = CameraMode.Opening;
    public void SwitchToFPS() => mode = CameraMode.TransitionToFPS;
    public void SwitchToTPS() => mode = CameraMode.TransitionToTPS;
}

using System.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] CameraController cameraController;
    [SerializeField] TPSAnchorController tpsAnchor;
    IEnumerator Start()
    {
        yield return StartCoroutine(PlayStartCutscene());
        PlayStartTPS();
    }

    IEnumerator PlayStartCutscene()
    {
       
        cameraController.SwitchToOpening();
        yield return new WaitForSeconds(3f);
    }

    void PlayStartTPS()
    {
        cameraController.SwitchToTPS();
        tpsAnchor.isRotating = true;
    }


}

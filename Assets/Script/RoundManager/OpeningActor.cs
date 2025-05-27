using System.Collections;
using UnityEngine;

public class OpeningActor : MonoBehaviour
{
    private void OnEnable()
    {
        RoundManager.OnModeChanged += HandleRoundModeChanged;
    }

    private void OnDisable()
    {
        RoundManager.OnModeChanged -= HandleRoundModeChanged;
    }

    private void HandleRoundModeChanged(RoundMode mode)
    {
        if (mode == RoundMode.Opening)
        {
            Debug.Log("OpeningActor");
            StartCoroutine(Act());
        }
    }

    IEnumerator Act()
    {

        yield return new WaitForSeconds(3);
        RoundManager.Instance.Mode=RoundMode.TPS;
        Debug.Log("OpeningActor end");
    }
}

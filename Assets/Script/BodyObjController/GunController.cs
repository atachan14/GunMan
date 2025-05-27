using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] Transform hand;

    [SerializeField] Vector3 handHoldPos = new Vector3(0.05f, 0.12f, -0.04f);
    Quaternion handHoldRot = new Quaternion(0.48394f, 0.45825f, 0.58045f, -0.46785f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GrabGun()
    {
        transform.SetParent(hand, false);

        transform.localPosition = handHoldPos;
        transform.localRotation = handHoldRot;

    }

    public IEnumerator DropGun()
    {
        transform.parent = null;
   
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(startPos.x, 0f, startPos.z);
        float speed = 2.0f;

        while (Vector3.Distance(transform.position, endPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = endPos;
        yield break;
    }
}

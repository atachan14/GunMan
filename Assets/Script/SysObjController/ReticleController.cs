using UnityEngine;
using UnityEngine.Windows;

public class ReticleController : MonoBehaviour
{
    [SerializeField] Transform BarrelStart;
    [SerializeField] Transform BarrelEnd;
    [SerializeField] LayerMask ReticleTargetWall;
    [SerializeField] RectTransform Reticle;

    // Update is called once per frame
    void Update()
    {
        if (RoundManager.Instance.IsTPS)
        {
            Reticle.gameObject.SetActive(false);
        }
        if (RoundManager.Instance.IsFPS)
        {
            Reticle.gameObject.SetActive(true);
            DisplayReticle();
        }
    }

    void DisplayReticle()
    {
        Vector3 dir = (BarrelEnd.position - BarrelStart.position).normalized;
        Ray ray = new Ray(BarrelStart.position, dir);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ReticleTargetWall))
        {
            transform.position = hit.point;



            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

            // スクリーン座標 → Canvas上のローカル座標に変換
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                Reticle.parent as RectTransform, // 親UIのRectTransform
                screenPos,
                null,
                out Vector2 localPos
            );

            // UIの位置を更新
            Reticle.localPosition = localPos;
        }

    }
}

using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    public static ReplayManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"[ReplayManager] 2個目生成されたのでDestroyします。Editorに戻って構造確認せぇ！");
            Destroy(gameObject); // 複数あったら潰す
            return;
        }
        Instance = this;
    }
    public void RecordPosition(Vector3 position)
    {

    }

    //public Vector3 PlayPosition()
    //{
    //    return null;

    //}
}

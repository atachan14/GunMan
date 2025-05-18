using UnityEngine;

public class InputPosition : MonoBehaviour
{
    public static InputPosition Instance;
    public Vector3 Position { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"[InputPosition] 2個目生成されたのでDestroyします。Editorに戻って構造確認せぇ！");
            Destroy(gameObject); // 複数あったら潰す
            return;
        }
        Instance = this;
    }
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // リアルタイム中だけ記録（リプレイ中はスキップ）
        if (!RoundManager.Instance.IsReplay)
        {
            Position = Input.mousePosition;
            //ReplayManager.Instance.RecordPosition(position);
        }
        else
        {
            //position = ReplayManager.Instance.PlayPosition();
        }
    }
}

using UnityEngine;

public class InputMouse : MonoBehaviour
{
    public static InputMouse Instance;
    public Vector3 Position { get; private set; }

    public Vector2 Axis { get; private set; }
    public float X => Axis.x;
    public float Y => Axis.y;


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
            Axis = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            //ReplayManager.Instance.RecordPosition(position);
        }
        else
        {
            //position = ReplayManager.Instance.PlayPosition();
        }
    }
}

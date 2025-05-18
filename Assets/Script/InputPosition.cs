using UnityEngine;

public class InputPosition : MonoBehaviour
{
    public static InputPosition Instance;
    public Vector3 Position { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"[InputPosition] 2�ڐ������ꂽ�̂�Destroy���܂��BEditor�ɖ߂��č\���m�F�����I");
            Destroy(gameObject); // ������������ׂ�
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
        // ���A���^�C���������L�^�i���v���C���̓X�L�b�v�j
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

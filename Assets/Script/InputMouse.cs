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
            Axis = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            //ReplayManager.Instance.RecordPosition(position);
        }
        else
        {
            //position = ReplayManager.Instance.PlayPosition();
        }
    }
}

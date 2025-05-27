using UnityEngine;

public class HumanPlayerInput : MonoBehaviour, IPlayerInput
{
    public Vector3 AimPosition { get; private set; }

    public Vector2 AimAxis { get; private set; }
    public float AimX => AimAxis.x;
    public float AimY => AimAxis.y;

    public bool LeftClick { get; private set; }
    public bool RightDown { get; private set; }
    public bool RightUp { get; private set; }

    public bool WUp { get; private set; }
    public bool WDown { get; private set; }
    public bool AUp { get; private set; }
    public bool ADown { get; private set; }
    public bool SUp { get; private set; }
    public bool SDown { get; private set; }
    public bool DUp { get; private set; }
    public bool DDown { get; private set; }
    public bool SpaceClick { get; private set; }


    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        AimPosition = Input.mousePosition;
        AimAxis = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        LeftClick = Input.GetMouseButtonDown(0);
        RightDown = Input.GetMouseButtonDown(1);
        RightUp = Input.GetMouseButtonUp(1);

        WDown = Input.GetKeyDown(KeyCode.W);
        WUp = Input.GetKeyUp(KeyCode.W);
        ADown = Input.GetKeyDown(KeyCode.A);
        AUp = Input.GetKeyUp(KeyCode.A);
        SDown = Input.GetKeyDown(KeyCode.S);
        SUp = Input.GetKeyUp(KeyCode.S);
        DDown = Input.GetKeyDown(KeyCode.D);
        DUp = Input.GetKeyUp(KeyCode.D);
        SpaceClick = Input.GetKeyDown(KeyCode.Space);

    }
}

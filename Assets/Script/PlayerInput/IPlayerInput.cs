using UnityEngine;

public interface IPlayerInput 
{
    // �ʒu�E���_
    Vector3 AimPosition { get; }
    Vector2 AimAxis { get; }
    float AimX { get; }
    float AimY { get; }

    // �{�^��
    bool LeftClick { get; }
    bool RightDown { get; }
    bool RightUp { get; }

    bool WUp { get; }
    bool WDown { get; }
    bool AUp { get; }
    bool ADown { get; }
    bool SUp { get; }
    bool SDown { get; }
    bool DUp { get; }
    bool DDown { get; }
    bool SpaceClick { get; }
}

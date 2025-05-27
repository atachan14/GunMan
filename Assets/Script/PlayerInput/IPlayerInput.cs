using UnityEngine;

public interface IPlayerInput 
{
    // 位置・視点
    Vector3 AimPosition { get; }
    Vector2 AimAxis { get; }
    float AimX { get; }
    float AimY { get; }

    // ボタン
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

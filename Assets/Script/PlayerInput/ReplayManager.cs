using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    public static ReplayManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"[ReplayManager] 2�ڐ������ꂽ�̂�Destroy���܂��BEditor�ɖ߂��č\���m�F�����I");
            Destroy(gameObject); // ������������ׂ�
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

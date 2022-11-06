using UnityEngine;

public class LevelChecker : MonoBehaviour
{
    [SerializeField] private Vector3 _halfExtents;

    public bool CheckCollisions()
    {
        return Physics.CheckBox(transform.position, _halfExtents);
    }
}

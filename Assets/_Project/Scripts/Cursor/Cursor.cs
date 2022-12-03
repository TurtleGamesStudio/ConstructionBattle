using UnityEngine;
using System;

public class Cursor : MonoBehaviour
{
    [SerializeField] private Transform _building;
    [SerializeField] private LevelChecker _levelChecker;
    [SerializeField] private float _offset;
    [SerializeField] private Collider[] _walls;

    private Detail _detail;

    public event Action<Vector3> Dropped;

    public int Level { get; private set; } = 1;

    public void Move(Vector3 translation)
    {
        _detail.Move(translation);
    }

    public void Init(Detail detail)
    {
        _detail = detail;
        _detail.gameObject.SetActive(true);
    }

    public void UpdateLevel()
    {
        while (_levelChecker.CheckCollisions())
        {
            Level++;
            transform.position = new Vector3(transform.position.x, transform.position.y + _offset, transform.position.z);
        }
    }

    public void Drop()
    {
        _detail.transform.parent = _building.transform;
        _detail.Drop();
        Dropped?.Invoke(_detail.transform.position);
    }

    public void ActivateWalls()
    {
        foreach (Collider collider in _walls)
        {
            collider.enabled = true;
        }
    }

    public void DeactivateWalls()
    {
        foreach (Collider collider in _walls)
        {
            collider.enabled = false;
        }
    }
}

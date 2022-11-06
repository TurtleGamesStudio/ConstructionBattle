using System.Collections;
using UnityEngine;
using System;

public class TowardsMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Coroutine _moving;

    public event Action Completed;

    public void MoveTo(Vector3 target)
    {
        if (_moving != null)
        {
            StopCoroutine(_moving);
        }

        _moving = StartCoroutine(Moving(target));
    }

    private IEnumerator Moving(Vector3 target)
    {
        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
            yield return null;
        }

        Completed?.Invoke();
    }
}
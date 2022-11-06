using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupFader : MonoBehaviour
{
    [SerializeField] private float _speed;

    private CanvasGroup _canvasGroup;
    private Coroutine _changing;

    public event Action Completed;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ChangeTransparency(float target)
    {
        if (_changing != null)
        {
            StopCoroutine(_changing);
        }

        _changing = StartCoroutine(Changing(target));
    }

    private IEnumerator Changing(float target)
    {
        while (_canvasGroup.alpha != target)
        {
            _canvasGroup.alpha = Mathf.MoveTowards(_canvasGroup.alpha, target, _speed * Time.deltaTime);
            yield return null;
        }

        Completed?.Invoke();
    }
}

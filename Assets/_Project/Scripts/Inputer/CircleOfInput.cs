using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class CircleOfInput : MonoBehaviour
{
    [SerializeField] private Inputer _inputer;
    [SerializeField] private RectTransform _borderRectTransform;

    private RectTransform _rectTransform;
    private float _radius;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _radius = _borderRectTransform.rect.width / 2;
    }

    private void OnDisable()
    {
        Reset();
    }

    private void Update()
    {
        _rectTransform.anchoredPosition = _inputer.Direction * _inputer.Share * _radius;
    }

    private void Reset()
    {
        _rectTransform.anchoredPosition = Vector2.zero;
    }
}

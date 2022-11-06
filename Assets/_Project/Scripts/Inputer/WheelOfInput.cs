using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class WheelOfInput : MonoBehaviour
{
    [SerializeField] private Inputer _inputer;
    [SerializeField] private RectTransform _parent;

    private RectTransform _rectTransform;
    private Vector2 _screenCenter;
    private Vector2 _modifier;

    private void Awake()
    {
        _rectTransform = (RectTransform)transform;
        _screenCenter = _parent.rect.size / 2f;
        _modifier = new Vector2(_screenCenter.x / (Screen.width * 0.5f), _screenCenter.y / (Screen.height * 0.5f));
    }

    private void OnEnable()
    {
        _rectTransform.localPosition = _inputer.CenterPoint * _modifier - _screenCenter;
        _inputer.TouchFinished += Deactivate;
    }

    private void OnDisable()
    {
        _inputer.TouchFinished -= Deactivate;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

using UnityEngine;

[RequireComponent(typeof(RectMover))]
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]
public class MovablePanel : MonoBehaviour
{
    [SerializeField] private float _animationTime;
    [SerializeField] private GameObject _panel;

    private Vector2 _startPosition;
    private Vector2 _endPosition;
    private RectMover _rectMover;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _rectMover = GetComponent<RectMover>();
        RectTransform rectTransform = GetComponent<RectTransform>();
        _startPosition = rectTransform.anchoredPosition;
        _endPosition = new Vector2(rectTransform.anchoredPosition.x, -rectTransform.anchoredPosition.y);

        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnDisable()
    {
        _rectMover.Completed -= OnCompleted;
    }

    public void ActivatePanel()
    {
        _panel.SetActive(true);
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        _rectMover.MoveTo(_endPosition, _animationTime);
    }

    public void DeactivatePanel()
    {
        _rectMover.Completed += OnCompleted;
        _rectMover.MoveTo(_startPosition, _animationTime);
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    private void OnCompleted()
    {
        _rectMover.Completed -= OnCompleted;
        _panel.SetActive(false);
    }
}

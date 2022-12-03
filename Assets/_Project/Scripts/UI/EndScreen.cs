using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private RectMover _upperPanel;
    [SerializeField] private RectMover _lowerPanel;

    [SerializeField] private float _animationTime;

    public void Show()
    {
        Appear(_upperPanel);
        Appear(_lowerPanel);
        OnShow();
    }

    protected virtual void OnShow()
    {
    }

    private void Appear(RectMover panel)
    {
        panel.gameObject.SetActive(true);
        Vector2 endPosition = GetEndPosition(panel.RectTransform);
        panel.MoveTo(endPosition, _animationTime);
    }

    private Vector2 GetEndPosition(RectTransform rectTransform)
    {
        Vector2 startPosition = rectTransform.anchoredPosition;
        Vector2 endPosition = new Vector2(startPosition.x, -startPosition.y);
        return endPosition;
    }
}

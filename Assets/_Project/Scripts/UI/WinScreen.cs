using UnityEngine;

public class WinScreen : EndScreen
{
    [SerializeField] private LevelView _levelView;
    [SerializeField] private CanvasGroupFader _canvasGroupFader;

    protected override void OnShow()
    {
        _levelView.UpdateText();
        _canvasGroupFader.gameObject.SetActive(true);
        _canvasGroupFader.ChangeTransparency(1f);
    }
}

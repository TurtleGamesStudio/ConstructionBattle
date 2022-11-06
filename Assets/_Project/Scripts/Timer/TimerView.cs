using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroupFader))]
public class TimerView : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private Image _image;

    private CanvasGroupFader _canvasGroupfader;

    private void Awake()
    {
        _canvasGroupfader = GetComponent<CanvasGroupFader>();
    }

    private void OnEnable()
    {
        _timer.Started += OnStarted;
        _timer.Updated += OnUpdated;
        _timer.Completed += OnCompleted;
        _timer.Stopped += OnCompleted;
    }

    private void OnDisable()
    {
        _timer.Started -= OnStarted;
        _timer.Updated -= OnUpdated;
        _timer.Completed -= OnCompleted;
        _timer.Stopped -= OnCompleted;
    }

    private void OnStarted()
    {
        _canvasGroupfader.ChangeTransparency(1f);
    }

    private void OnCompleted()
    {
        _canvasGroupfader.ChangeTransparency(0f);
    }

    private void OnUpdated()
    {
        _image.fillAmount = _timer.TimeLeft / _timer.TotalTime;
    }
}

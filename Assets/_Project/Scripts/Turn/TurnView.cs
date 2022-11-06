using UnityEngine;

public class TurnView : MonoBehaviour
{
    [SerializeField] private Turn _turn;
    [SerializeField] private MovablePanel _movablePanel;

    private void OnEnable()
    {
        _turn.Started += OnStarted;
        _turn.Finished += OnFinished;
    }

    private void OnDisable()
    {
        _turn.Started -= OnStarted;
        _turn.Finished -= OnFinished;
    }

    private void OnStarted()
    {
        _movablePanel.ActivatePanel();
    }

    private void OnFinished()
    {
        _movablePanel.DeactivatePanel();
    }
}

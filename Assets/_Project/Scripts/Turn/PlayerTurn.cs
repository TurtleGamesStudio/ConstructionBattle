using UnityEngine;
using System.Collections;

public class PlayerTurn : Turn
{
    [SerializeField] private DetailChoosingPanel _detailChoosingPanel;
    [SerializeField] private DropButton _dropButton;

    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Inputer _inputer;

    private IInterTurnsBehaviour _interTurnsBehaviour;
    private Detail _currentDetail;

    private void OnEnable()
    {
        _detailChoosingPanel.Clicked += OnDetailButtonClicked;
        _dropButton.Clicked += OnDropButtonClicked;
    }

    private void OnDisable()
    {
        Unscribe();
    }

    public void Init(IInterTurnsBehaviour interTurnsBehaviour)
    {
        _interTurnsBehaviour = interTurnsBehaviour;
        _detailChoosingPanel.Init();
    }

    protected override void OnStart()
    {
        _detailChoosingPanel.ActivatePanel();
    }

    private void Unscribe()
    {
        _detailChoosingPanel.Clicked -= OnDetailButtonClicked;
        _dropButton.Clicked -= OnDropButtonClicked;

        if (_currentDetail != null)
        {
            _currentDetail.ScaleCompleted -= OnDetailScaleCompleted;
        }
    }

    private void OnDetailButtonClicked(Detail detail)
    {
        _currentDetail = detail;
        _currentDetail.ScaleCompleted += OnDetailScaleCompleted;
        _inputer.enabled = true;
        _dropButton.Activate();
    }

    private void OnDetailScaleCompleted()
    {
        _currentDetail.ScaleCompleted -= OnDetailScaleCompleted;
        _playerMovement.enabled = true;
    }

    public void Lose()
    {
        Unscribe();
        _detailChoosingPanel.DeactivatePanel();
        OnDropButtonClicked();
    }

    private void OnDropButtonClicked()
    {
        _playerMovement.enabled = false;
        _inputer.enabled = false;
        _dropButton.Deactivate();
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        _interTurnsBehaviour.StartBehaviour(_currentDetail.CollisionEventsSenders);
    }
}

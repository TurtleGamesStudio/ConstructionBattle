using UnityEngine;

public class PlayerTurn : Turn
{
    [SerializeField] private DetailChoosingPanel _detailChoosingPanel;
    [SerializeField] private DropButton _dropButton;
    [SerializeField] private InterTurnsBehaviour _interTurnsBehaviour;


    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Inputer _inputer;

    private Detail _currentDetail;

    private void OnEnable()
    {
        _detailChoosingPanel.Clicked += OnDetailButtonClicked;
        _dropButton.Clicked += OnDropButtonClicked;
    }

    private void OnDisable()
    {
        _detailChoosingPanel.Clicked -= OnDetailButtonClicked;
        _dropButton.Clicked -= OnDropButtonClicked;

        if (_currentDetail != null)
        {
            _currentDetail.ScaleCompleted -= OnDetailScaleCompleted;
        }
    }

    public void Init()
    {
        _detailChoosingPanel.Init();
    }

    protected override void OnStart()
    {
        _detailChoosingPanel.ActivatePanel();
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

    private void OnDropButtonClicked()
    {
        _playerMovement.enabled = false;
        _inputer.enabled = false;
        _dropButton.Deactivate();
        _interTurnsBehaviour.StartBehaviour(_currentDetail.CollisionEventsSenders);
    }
}

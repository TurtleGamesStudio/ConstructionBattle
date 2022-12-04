using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _cooperativeBehaviour;
    [SerializeField] private TurnsSequence _turnsSequence;
    [SerializeField] private PlayerTurn _playerTurn;
    [SerializeField] private OpponentTurn _opponentTurn;

    public event Action Won;
    public event Action Lose;

    private IInterTurnsBehaviour _interTurnsBehaviour => (IInterTurnsBehaviour)_cooperativeBehaviour;

    private void OnValidate()
    {
        if (_cooperativeBehaviour != null && _cooperativeBehaviour is not IInterTurnsBehaviour)
        {
            Debug.LogError($"{nameof(_cooperativeBehaviour)} needs to implement {nameof(IInterTurnsBehaviour)}");
            _cooperativeBehaviour = null;
        }
    }

    private void OnEnable()
    {
        _interTurnsBehaviour.Failed += OnFailed;
        _interTurnsBehaviour.Completed += OnComplete;
    }

    private void OnDisable()
    {
        Unscribe();
    }

    private void Start()
    {
        _interTurnsBehaviour.Init();
        _playerTurn.Init(_interTurnsBehaviour);
        _opponentTurn.Init(_interTurnsBehaviour);

        _playerTurn.StartTurn();
    }

    private void Unscribe()
    {
        _interTurnsBehaviour.Failed -= OnFailed;
        _interTurnsBehaviour.Completed -= OnComplete;
    }

    private void OnFailed()
    {
        Unscribe();
        _turnsSequence.CurrentTurn.Finish();

        if (_turnsSequence.CurrentTurn == _playerTurn)
        {
            _playerTurn.Lose();
            Lose?.Invoke();
        }
        else
        {
            Won?.Invoke();
        }
    }

    private void OnComplete()
    {
        StartNextTurn();
    }

    private void StartNextTurn()
    {
        _turnsSequence.CurrentTurn.Finish();

        _turnsSequence.GiveTurnToNextPlayer();
        _turnsSequence.CurrentTurn.StartTurn();
    }
}

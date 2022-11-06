using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    [SerializeField] private InterTurnsBehaviour _interTurnsBehaviour;
    [SerializeField] private TurnsSequence _turnsSequence;
    [SerializeField] private PlayerTurn _playerTurn;

    public event Action Won;
    public event Action Lose;

    private void OnEnable()
    {
        _interTurnsBehaviour.Failed += OnFailed;
        _interTurnsBehaviour.Completed += OnComplete;
    }

    private void OnDisable()
    {
        _interTurnsBehaviour.Failed -= OnFailed;
        _interTurnsBehaviour.Completed -= OnComplete;
    }

    private void Start()
    {
        _interTurnsBehaviour.Init();
        _playerTurn.Init();

        _playerTurn.StartTurn();
    }

    private void OnFailed()
    {
        _turnsSequence.CurrentTurn.Finish();

        if (_turnsSequence.CurrentTurn == _playerTurn)
        {
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

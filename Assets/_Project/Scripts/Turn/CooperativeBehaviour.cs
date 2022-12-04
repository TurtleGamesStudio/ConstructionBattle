using System.Collections.Generic;
using UnityEngine;
using System;

public class CooperativeBehaviour : MonoBehaviour, IInterTurnsBehaviour
{
    [SerializeField] private CollisionEventsSender[] _ground;

    private DropHandler _dropHandler;
    private DropHandler _groundDropHandler;

    public event Action Completed;
    public event Action Failed;

    private void OnDisable()
    {
        if (_groundDropHandler != null)
        {
            _groundDropHandler.Dropped -= OnCrush;
        }

        if (_dropHandler != null)
        {
            _dropHandler.Dropped -= OnDropped;
        }
    }

    public void Init()
    {
        _dropHandler = new DropHandler();

        _groundDropHandler = new DropHandler();
        _groundDropHandler.AddRange(_ground);
        _groundDropHandler.Dropped += OnCrush;
    }

    public void StartBehaviour(IEnumerable<CollisionEventsSender> collisionEventsSenders)
    {
        _dropHandler.AddRange(collisionEventsSenders);
        _dropHandler.Dropped += OnDropped;
    }

    private void OnDropped()
    {
        _dropHandler.Dropped -= OnDropped;
        _dropHandler.Unscribe();
        Completed?.Invoke();
    }

    private void OnCrush()
    {
        _groundDropHandler.Dropped -= OnCrush;
        _dropHandler.Dropped -= OnDropped;
        Failed?.Invoke();
    }
}

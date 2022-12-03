using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InterTurnsBehaviour : MonoBehaviour, IInterTurnsBehaviour
{
    [SerializeField] private float _stabilizationInterval;
    [SerializeField] private Timer _crushTimer;
    [SerializeField] private float _survivalInterval;
    [SerializeField] private CollisionEventsSender[] _groundEventsSenders;
    [SerializeField] private CollisionEventsSender[] _ground;

    private DropHandler _dropHandler;
    private DropHandler _groundDropHandler;
    private IEnumerable<CollisionEventsSender> _newDetail;

    public event Action Completed;
    public event Action Failed;

    private void OnDisable()
    {
        _groundDropHandler.Dropped -= OnCrush;
        _dropHandler.Dropped -= OnDropped;
        _dropHandler.Dropped -= OnCrush;
        _crushTimer.Completed -= OnSucsessDrop;
    }

    public void Init()
    {
        _dropHandler = new DropHandler();
        _dropHandler.AddRange(_groundEventsSenders);
        _groundDropHandler = new DropHandler();
        _groundDropHandler.AddRange(_ground);
        _groundDropHandler.Dropped += OnCrush;
    }

    public void StartBehaviour(IEnumerable<CollisionEventsSender> collisionEventsSenders)
    {
        _newDetail = collisionEventsSenders;
        _dropHandler.Dropped += OnDropped;
    }

    private void OnDropped()
    {
        _dropHandler.Dropped -= OnDropped;
        StartCoroutine(SubscribeOnCrush());
        _crushTimer.Completed += OnSucsessDrop;
        _crushTimer.StartTimer(_survivalInterval);
    }

    private IEnumerator SubscribeOnCrush()
    {
        yield return new WaitForSeconds(_stabilizationInterval);
        _dropHandler.Dropped += OnCrush;
    }

    private void OnCrush()
    {
        _groundDropHandler.Dropped -= OnCrush;
        OnTurnEnd();
        _crushTimer.Stop();
        Failed?.Invoke();
    }

    private void OnSucsessDrop()
    {
        OnTurnEnd();
        _dropHandler.AddRange(_newDetail);
        Completed?.Invoke();
    }

    private void OnTurnEnd()
    {
        _dropHandler.Dropped -= OnCrush;
        _crushTimer.Completed -= OnSucsessDrop;
    }
}

using System.Collections.Generic;
using UnityEngine;
using System;

public class DropHandler
{
    private List<CollisionEventsSender> _collisionEventsSenders;

    public event Action Dropped;

    public DropHandler()
    {
        _collisionEventsSenders = new List<CollisionEventsSender>();
    }

    public void AddRange(IEnumerable<CollisionEventsSender> collisionEventsSenders)
    {
        _collisionEventsSenders.AddRange(collisionEventsSenders);

        foreach (CollisionEventsSender collisionEventsSender in collisionEventsSenders)
        {
            collisionEventsSender.Entered += OnEntered;
        }
    }

    public void Unscribe()
    {
        foreach (CollisionEventsSender collisionEventsSender in _collisionEventsSenders)
        {
            collisionEventsSender.Entered -= OnEntered;
        }

        _collisionEventsSenders.Clear();
    }

    private void OnEntered(Collision collision)
    {
        Dropped?.Invoke();
    }

    ~DropHandler()
    {
        Unscribe();
    }
}

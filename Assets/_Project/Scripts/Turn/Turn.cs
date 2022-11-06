using UnityEngine;
using System;

public abstract class Turn : MonoBehaviour
{
    public event Action Started;
    public event Action Finished;

    public void StartTurn()
    {
        Started?.Invoke();
        OnStart();
    }

    public void Finish()
    {
        Finished?.Invoke();
    }

    protected abstract void OnStart();
}

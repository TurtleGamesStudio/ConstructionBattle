using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private Coroutine _ticking;

    public event Action Started;
    public event Action Stopped;
    public event Action Completed;
    public event Action Updated;

    public float TotalTime { get; private set; }
    public float TimeLeft { get; private set; }

    public void StartTimer(float interval)
    {
        if (_ticking != null)
        {
            StopCoroutine(_ticking);
        }

        TotalTime = interval;
        TimeLeft = interval;
        Started?.Invoke();

        _ticking = StartCoroutine(Ticking());
    }

    public void Stop()
    {
        if (_ticking != null)
        {
            StopCoroutine(_ticking);
        }

        TimeLeft = 0;
        Stopped?.Invoke();
    }

    private IEnumerator Ticking()
    {
        while (TimeLeft >= 0)
        {
            TimeLeft -= Time.deltaTime;
            Updated?.Invoke();
            yield return null;
        }

        Completed?.Invoke();
    }
}

using System.Collections;
using UnityEngine;
using System;

public class LerpScaler : MonoBehaviour
{
    [SerializeField] private AnimationCurve _dependencyOfProgressByTimeShare;

    private Coroutine _moving;

    public event Action Completed;

    public void ScaleLerp(Vector3 target, float requireTime)
    {
        if (_moving != null)
        {
            StopCoroutine(_moving);
        }

        _moving = StartCoroutine(ScalingLerp(target, requireTime));
    }

    private IEnumerator ScalingLerp(Vector3 target, float requireTime)
    {
        float time = 0;
        float progress;
        Vector3 startScale = transform.localScale;

        while (transform.localScale != target)
        {
            time += Time.deltaTime;

            if (time > requireTime)
            {
                time = requireTime;
            }

            progress = _dependencyOfProgressByTimeShare.Evaluate(time / requireTime);
            transform.localScale = Vector3.Lerp(startScale, target, progress);
            yield return null;
        }

        Completed?.Invoke();
    }
}
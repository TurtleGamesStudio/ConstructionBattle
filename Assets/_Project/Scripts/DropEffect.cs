using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Detail))]
public class DropEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _clouds;

    private Detail _detail;

    private void Awake()
    {
        _detail = GetComponent<Detail>();
    }

    private void OnEnable()
    {
        _detail.Dropping += OnDropping;
    }

    private void OnDisable()
    {
        _detail.Dropping -= OnDropping;

        foreach (CollisionEventsSender collisionEventsSender in _detail.CollisionEventsSenders)
        {
            collisionEventsSender.Entered -= OnEnter;
        }
    }

    private void OnDropping()
    {
        _detail.Dropping -= OnDropping;
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        Subscribe();
    }

    private void Subscribe()
    {
        foreach (CollisionEventsSender collisionEventsSender in _detail.CollisionEventsSenders)
        {
            collisionEventsSender.Entered += OnEnter;
        }
    }

    private void OnEnter(Collision collision)
    {
        foreach (CollisionEventsSender collisionEventsSender in _detail.CollisionEventsSenders)
        {
            collisionEventsSender.Entered -= OnEnter;
        }

        _clouds.Play();
    }
}

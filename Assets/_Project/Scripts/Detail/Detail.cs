using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(LerpScaler))]
[RequireComponent(typeof(TowardsMover))]
public class Detail : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private CollisionEventsSender[] _collisionEventsSenders;
    [SerializeField] private Vector3 _targetScale = Vector3.one;
    [SerializeField] private float _intervalOfScaling = 0.5f;

    private LerpScaler _lerpScaler;
    private TowardsMover _towardsMover;

    public IReadOnlyList<CollisionEventsSender> CollisionEventsSenders => _collisionEventsSenders;

    public event Action ScaleCompleted;
    public event Action MoveCompleted;

    public Sprite Sprite => _sprite;

    private void Awake()
    {
        _lerpScaler = GetComponent<LerpScaler>();
        _towardsMover = GetComponent<TowardsMover>();
    }

    private void OnEnable()
    {
        ScaleTo(_targetScale, _intervalOfScaling);
    }

    public void Move(Vector3 translation)
    {
        _rigidbody.velocity = translation;
    }

    public void MoveTowards(Vector3 target)
    {
        _towardsMover.Completed += OnMoveCompleted;
        _towardsMover.MoveTo(target);
    }

    public void ScaleTo(Vector3 target, float requireTime)
    {
        _lerpScaler.Completed += OnScaleCompleted;
        _lerpScaler.ScaleLerp(target, requireTime);
    }

    public void Drop()
    {
        Destroy(_rigidbody);

        foreach (CollisionEventsSender collisionEventsSender in _collisionEventsSenders)
        {
            collisionEventsSender.gameObject.AddComponent<Rigidbody>();

            if (collisionEventsSender.TryGetComponent(out FixedJointCreator creator))
            {
                creator.Connect();
            }
        }
    }

    private void OnScaleCompleted()
    {
        ScaleCompleted?.Invoke();
    }

    private void OnMoveCompleted()
    {
        MoveCompleted?.Invoke();
    }
}

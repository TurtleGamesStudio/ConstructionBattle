using UnityEngine;
using System;

public class Inputer : MonoBehaviour
{
    [SerializeField] private float _radius;

    private Vector2 _screenPoint;
    private Vector2 _centerPoint;
    private Vector2 _direction;
    private float _share;

    public event Action TouchStarted;
    public event Action TouchFinished;

    public Vector2 CenterPoint => _centerPoint;
    public float Share => _share;
    public Vector2 Direction => _direction;
    public float Radius => _radius;

    public static Inputer Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void OnDisable()
    {
        FinishTouch();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _centerPoint = Input.mousePosition;
            TouchStarted?.Invoke();
        }

        if (Input.GetMouseButton(0))
        {
            _screenPoint = Input.mousePosition;

            if (_screenPoint != _centerPoint)
            {
                Calculate();
            }
            else
            {
                _share = 0;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            FinishTouch();
        }
    }

    private void FinishTouch()
    {
        _share = 0;
        TouchFinished?.Invoke();
    }

    private void Calculate()
    {
        Vector2 translation = _screenPoint - _centerPoint;
        _direction = translation.normalized;
        float distance = translation.magnitude;
        _share = Mathf.Clamp(distance, 0, _radius) / _radius;
    }
}

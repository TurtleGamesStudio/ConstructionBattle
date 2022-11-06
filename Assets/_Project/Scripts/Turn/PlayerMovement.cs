using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Cursor _cursor;
    [SerializeField] private Inputer _inputer;
    [SerializeField] private float _speed;

    private Quaternion _cameraRotation;
    private Coroutine _coroutine;

    private void Awake()
    {
        float angle = Camera.main.transform.rotation.eulerAngles.y;
        _cameraRotation = Quaternion.Euler(0, angle, 0);
    }

    private void OnEnable()
    {
        _inputer.TouchStarted += OnTouchStarted;
        _inputer.TouchFinished += OnTouchFinished;
        _cursor.ActivateWalls();
    }

    private void OnDisable()
    {
        _inputer.TouchStarted -= OnTouchStarted;
        _inputer.TouchFinished -= OnTouchFinished;
        OnTouchFinished();
        _cursor.DeactivateWalls();
    }

    private void OnTouchStarted()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(OnTouching());
    }

    private IEnumerator OnTouching()
    {
        while (true)
        {
            Vector3 translation = GetTranslation();
            _cursor.Move(translation);
            yield return null;
        }
    }

    private void OnTouchFinished()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _cursor.Move(Vector3.zero);
    }

    private Vector3 GetTranslation()
    {
        Vector3 direction = _cameraRotation * (new Vector3(_inputer.Direction.x, 0, _inputer.Direction.y));
        Vector3 translation = direction * _inputer.Share * _speed * Time.deltaTime;
        return translation;
    }
}

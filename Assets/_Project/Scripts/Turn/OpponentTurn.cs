using UnityEngine;

public class OpponentTurn : Turn
{
    [SerializeField] private Cursor _cursor;
    [SerializeField] private Detail[] _templates;
    [SerializeField] private float _aimDeflection;
    [SerializeField] private InterTurnsBehaviour _interTurnsBehaviour;

    private Detail _detail;
    private Vector3 _lastCursorPosition;

    private void OnEnable()
    {
        _cursor.Dropped += OnDropped;
    }

    private void OnDisable()
    {
        _cursor.Dropped -= OnDropped;

        if (_detail != null)
        {
            _detail.ScaleCompleted -= OnDetailScaleComleted;
        }
    }

    protected override void OnStart()
    {
        CreateRandomDetail();
    }

    public void CreateRandomDetail()
    {
        _cursor.UpdateLevel();
        Detail template = _templates[Random.Range(0, _templates.Length)];
        _detail = Instantiate(template, _cursor.transform);
        _detail.ScaleCompleted += OnDetailScaleComleted;
        _cursor.Init(_detail);
    }

    private void OnDetailScaleComleted()
    {
        _detail.ScaleCompleted -= OnDetailScaleComleted;
        Vector3 target = ChooseCursorTargetPoint();
        MoveTo(target);
    }

    private void OnDropped(Vector3 lastPosition)
    {
        _lastCursorPosition = lastPosition;
    }

    private Vector3 ChooseCursorTargetPoint()
    {
        float xOffset = Random.Range(-_aimDeflection, _aimDeflection);
        float zOffset = Random.Range(-_aimDeflection, _aimDeflection);
        Vector3 target = new Vector3(_lastCursorPosition.x + xOffset, _cursor.transform.position.y, _lastCursorPosition.z + zOffset);
        return target;
    }

    private void MoveTo(Vector3 target)
    {
        _detail.MoveCompleted += OnMoveCompleted;
        _detail.MoveTowards(target);
    }

    private void OnMoveCompleted()
    {
        Drop();
    }

    private void Drop()
    {
        _cursor.Drop();
        _interTurnsBehaviour.StartBehaviour(_detail.CollisionEventsSenders);
    }
}

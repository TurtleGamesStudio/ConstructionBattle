using UnityEngine;

public class TurnsSequence : MonoBehaviour
{
    [SerializeField] private Turn[] _turns;

    private int _indexOfCurrentTurn;

    public Turn CurrentTurn => _turns[_indexOfCurrentTurn];

    public void GiveTurnToNextPlayer()
    {
        _indexOfCurrentTurn++;

        if (_indexOfCurrentTurn == _turns.Length)
        {
            _indexOfCurrentTurn = 0;
        }
    }

    public Turn GetNextTurn()
    {
        _indexOfCurrentTurn++;

        if (_indexOfCurrentTurn > _turns.Length)
        {
            _indexOfCurrentTurn = 0;
        }

        return _turns[_indexOfCurrentTurn];
    }
}

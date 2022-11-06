using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Levels", menuName = "Levels", order = 1)]
public class Levels : ScriptableObject
{
    [SerializeField] private string[] _names;

    public IReadOnlyList<string> Names => _names;
}

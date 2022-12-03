using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Cursor _cursor;

    public void UpdateText()
    {
        _text.text = "Max height = " + _cursor.Level.ToString() + " cubes!";
    }
}

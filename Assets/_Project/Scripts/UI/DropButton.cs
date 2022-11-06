using UnityEngine;
using UnityEngine.UI;
using System;

public class DropButton : MonoBehaviour
{
    [SerializeField] private MovablePanel _movablePanel;
    [SerializeField] private Button _button;
    [SerializeField] private Cursor _cursor;

    public event Action Clicked;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    public void Activate()
    {
        _movablePanel.ActivatePanel();
    }

    public void Deactivate()
    {
        _movablePanel.DeactivatePanel();
    }

    private void OnClick()
    {
        _cursor.Drop();
        Clicked?.Invoke();
    }
}

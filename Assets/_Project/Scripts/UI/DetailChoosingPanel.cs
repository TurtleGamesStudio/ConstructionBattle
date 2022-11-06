using UnityEngine;
using System;

public class DetailChoosingPanel : MonoBehaviour
{
    [SerializeField] private MovablePanel _movablePanel;
    [SerializeField] private DetailButton[] _detailButtons;
    [SerializeField] private Detail[] _details;

    public event Action<Detail> Clicked;

    private void OnEnable()
    {
        foreach (DetailButton button in _detailButtons)
        {
            button.Clicked += OnClicked;
        }
    }

    private void OnDisable()
    {
        foreach (DetailButton button in _detailButtons)
        {
            button.Clicked -= OnClicked;
        }
    }

    public void Init()
    {
        foreach (DetailButton button in _detailButtons)
        {
            SetDetail(button);
        }
    }

    public void ActivatePanel()
    {
        _movablePanel.ActivatePanel();
    }

    public void DeactivatePanel()
    {
        _movablePanel.DeactivatePanel();
    }

    private void OnClicked(DetailButton detailButton, Detail detail)
    {
        SetDetail(detailButton);
        DeactivatePanel();
        Clicked?.Invoke(detail);
    }

    private void SetDetail(DetailButton detailButton)
    {
        Detail detail = GetRandomTemplate();
        detailButton.SetDetail(detail);
    }

    private Detail GetRandomTemplate()
    {
        int index = UnityEngine.Random.Range(0, _details.Length);
        return _details[index];
    }
}

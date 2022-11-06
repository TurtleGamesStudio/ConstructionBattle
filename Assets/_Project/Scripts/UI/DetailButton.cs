using UnityEngine;
using UnityEngine.UI;
using System;

public class DetailButton : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;
    [SerializeField] private Cursor _cursor;

    private Detail _detail;

    public event Action<DetailButton, Detail> Clicked;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    public void SetDetail(Detail template)
    {
        _detail = Instantiate(template, _cursor.transform);
        _icon.sprite = _detail.Sprite;
    }

    public void Activate()
    {
        _button.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        _button.gameObject.SetActive(false);
    }

    private void OnClick()
    {
        _cursor.UpdateLevel();
        _cursor.Init(_detail);

        Clicked?.Invoke(this, _detail);
    }
}

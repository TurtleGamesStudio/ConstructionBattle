using UnityEngine;
using UnityEngine.UI;
using General;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(RestartLevel);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(RestartLevel);
    }

    private void RestartLevel()
    {
        LevelLoader.Instance.Reload();
    }
}

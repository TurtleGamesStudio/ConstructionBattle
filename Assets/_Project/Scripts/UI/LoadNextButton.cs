using UnityEngine;
using UnityEngine.UI;
using General;

public class LoadNextButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(LoadNextLevel);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(LoadNextLevel);
    }

    private void LoadNextLevel()
    {
        LevelLoader.Instance.LoadNext();
    }
}

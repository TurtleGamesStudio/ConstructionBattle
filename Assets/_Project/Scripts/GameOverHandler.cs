using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private EndScreen _winScreen;
    [SerializeField] private EndScreen _loseScreen;

    [SerializeField] private ParticleSystem[] _confetties;
    [SerializeField] private Image _tiredSmile;
    [SerializeField] private float _timeOfAppearence;

    private void OnEnable()
    {
        _game.Won += OnWon;
        _game.Lose += OnLose;
    }

    private void OnDisable()
    {
        _game.Won -= OnWon;
        _game.Lose -= OnLose;
    }

    private void OnWon()
    {
        _winScreen.Show();

        foreach (ParticleSystem particleSystem in _confetties)
        {
            particleSystem.Play();
        }
    }

    private void OnLose()
    {
        _loseScreen.Show();
        _tiredSmile.gameObject.SetActive(true);
        StartCoroutine(Appear());
    }

    private IEnumerator Appear()
    {
        float progress = 0;
        float time = 0;
        Color target = new Color(1, 1, 1, 1);

        while (progress < 1)
        {
            time += Time.deltaTime;
            progress = time / _timeOfAppearence;
            float alpha = Mathf.Lerp(0, 1, progress);
            _tiredSmile.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
    }
}

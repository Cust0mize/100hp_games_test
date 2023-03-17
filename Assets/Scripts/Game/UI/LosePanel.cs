using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePanel : AbstractPanel
{
    [SerializeField] private Button _restartButton;

    public override void Show(Action onComplete) {
        base.Show(onComplete);
        _restartButton.onClick.AddListener(RestartGame);
    }

    private void RestartGame() {
        SceneManager.LoadScene(0);
        PlayerPrefs.DeleteAll();
    }
}

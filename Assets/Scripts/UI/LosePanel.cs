using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePanel : AbstractPanel
{
    [SerializeField] private Button _restartButton;

    public override void Show(Action onComplete) {
        base.Show(onComplete);
        _restartButton.onClick.AddListener(NextLevel);
    }

    private void NextLevel() {
        SceneManager.LoadScene(0);
    }
}

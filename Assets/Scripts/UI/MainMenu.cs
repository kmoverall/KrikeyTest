using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button _StartButton;
    [SerializeField]
    private Button _QuitButton;
    [SerializeField]
    private Button _ContinueButton;

    [SerializeField]
    private TMP_Text _HighScoreText;
    [SerializeField]
    private TMP_Text _TitleText;
    [SerializeField]
    private TMP_Text _GameOverText;

    private void Awake()
    {
        _StartButton.onClick.AddListener(OnStartButton);
        _ContinueButton.onClick.AddListener(OnContinueButton);
        _QuitButton.onClick.AddListener(OnQuitButton);
    }

    public void Open(bool showContinue, bool showGameOver = false)
    {
        gameObject.SetActive(true);
        _ContinueButton.gameObject.SetActive(showContinue);
        _StartButton.gameObject.SetActive(!showContinue);

        _GameOverText.gameObject.SetActive(showGameOver);
        _TitleText.gameObject.SetActive(!showGameOver);

        _HighScoreText.text = CoreController.ScoreManager.HighScore.ToString();
        Time.timeScale = 0;
        CoreController.PlayerInput.SwitchCurrentActionMap("UI");
    }

    public void Close()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        CoreController.PlayerInput.SwitchCurrentActionMap("Player");
    }

    private void OnStartButton()
    {
        CoreController.GameManager.StartGame();
        Close();
    }

    private void OnContinueButton() => Close();

    private void OnQuitButton()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}

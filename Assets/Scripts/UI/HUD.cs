using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _ScoreText;
    [SerializeField]
    private TMP_Text _LevelText;
    [SerializeField]
    private TMP_Text _LivesText;
     
    private void Start()
    {
        _ScoreText.text = "";
        _LevelText.text = "";
        _LivesText.text = "";

        enabled = false;
    }

    public void StartUpdating() => enabled = true;
    public void StopUpdating() => enabled = false;

    private void Update()
    {
        _ScoreText.text = CoreController.ScoreManager.Score.ToString();
        _LevelText.text = CoreController.GameManager.Level.ToString();

        // Prevent -1 from showing after player loses their last life
        var lives = CoreController.Player.LifeCount;
        lives = Mathf.Clamp(lives, 0, int.MaxValue);
        _LivesText.text = lives.ToString();
    }
}

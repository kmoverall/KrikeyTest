using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int _StartingLives;
    [SerializeField]
    private float _TransitionPause;
    [SerializeField]
    private float _DeathPause;

    public int Level { get; private set; }

    private void Start()
    {
        CoreController.Player.OnDestroy += PlayerDeath;
        CoreController.EnemyManager.OnAllDestroyed += CompleteLevel;
        CoreController.WaveController.OnYLimitHit += InstantGameOver;

        StartGame();
    }

    public void StartGame()
    {
        CoreController.Player.LifeCount = _StartingLives;
        CoreController.ScoreManager.Reset();
        CoreController.UIManager.HUD.StartUpdating();
        StartLevel(1);
    }

    private void StartLevel(int level)
    {
        Level = level;
        CoreController.Player.Reset();
        CoreController.PlayerBulletPool.ReturnAll();
        CoreController.EnemyBulletPool.ReturnAll();
        CoreController.EnemyManager.ClearEnemies();
        CoreController.EnemyManager.StartRow = Level - 1;
        CoreController.EnemyManager.SpawnWave();
        Time.timeScale = 1f;
    }

    private void CompleteLevel() => StartCoroutine(LevelTransition());

    private IEnumerator LevelTransition()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(_TransitionPause);
        StartLevel(Level + 1);
    }

    private void PlayerDeath() => StartCoroutine(PlayerDeathRoutine());

    private void InstantGameOver() 
    {
        CoreController.Player.LifeCount = -1;
        StartCoroutine(PlayerDeathRoutine());
    }

    private IEnumerator PlayerDeathRoutine()
    {
        Time.timeScale = 0;
        CoreController.Player.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(_DeathPause);

        if (CoreController.Player.LifeCount < 0)
        {
            GameOver();
            yield break;
        }

        CoreController.Player.gameObject.SetActive(true);
        Time.timeScale = 1;
    }

    private void GameOver()
    {
        CoreController.UIManager.HUD.StopUpdating();
        CoreController.ScoreManager.UpdateHighScore();
    }
}

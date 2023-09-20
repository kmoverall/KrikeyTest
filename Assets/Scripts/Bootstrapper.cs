using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bootstrapper : MonoBehaviour
{
    public PlayerInput PlayerInput;
    public PlayerShip Player;

    public ObjectPool PlayerBulletPool;
    public ObjectPool EnemyBulletPool;

    public EnemyManager EnemyManager;
    public WaveController WaveController;

    public GameManager GameManager;

    // Set up static references for other scripts
    public void Awake()
    {
        CoreController.PlayerInput = PlayerInput;
        CoreController.PlayerBulletPool = PlayerBulletPool;
        CoreController.EnemyBulletPool = EnemyBulletPool;
        CoreController.EnemyManager = EnemyManager;
        CoreController.WaveController = WaveController;
        CoreController.GameManager = GameManager;
        CoreController.Player = Player;
    }
}

public static class CoreController
{
    public static PlayerInput PlayerInput;
    public static PlayerShip Player;

    public static ObjectPool PlayerBulletPool;
    public static ObjectPool EnemyBulletPool;

    public static EnemyManager EnemyManager;
    public static WaveController WaveController;

    public static GameManager GameManager;
}
